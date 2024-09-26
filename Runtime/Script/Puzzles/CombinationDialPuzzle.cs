using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using Studio23.SS2.PuzzleSystem.Data;
using Studio23.SS2.PuzzleSystem.Interface;
using UnityEngine;

[assembly: InternalsVisibleTo("editmode.tests")]

namespace Studio23.SS2.PuzzleSystem.Core
{
    public class CombinationDialPuzzle : IDialPuzzle
    {
        [SerializeField] private bool _dialSelectionHorizontally;

        private int _selectedDial { get; set; }

        public int SelectedDial
        {
            get => _selectedDial;
            set
            {
                if (Dials.Length == 0)
                {
                    Debug.LogError($"Dials is empty!");
                    return;
                }
                if (value >= Dials.Length) _selectedDial = 0;
                else if (value < 0) _selectedDial = Dials.Length - 1;
                else _selectedDial = value;
                OnSelectedDialChanged?.Invoke(_selectedDial);
            }
        }

        private bool _isPuzzleStarted;

        public bool IsPuzzleStarted
        {
            get => _isPuzzleStarted;
            set
            {
                if (_isPuzzleStarted == value) return;
                _isPuzzleStarted = value;
                if (_isPuzzleStarted)
                {
                    OnPuzzleStart?.Invoke();
                }
                else
                {
                    OnPuzzleStop?.Invoke();
                }
            }
        }

        public List<PuzzleHints> PuzzleHints
        {
            get => PuzzleInfo.PuzzleHints;
            set => PuzzleInfo.PuzzleHints = value;
        }

        public IDial[] Dials { get; set; }
        public DialPuzzleInfo PuzzleInfo { get; set; }
        public event Action OnPuzzleStart;
        public event Action OnPuzzleStop;
        public event Action<int> OnSelectedDialChanged;
        public event Action<BaseDialInfo> OnDialUpdated; 

        public event Action  OnPuzzleSolved;
        public bool CheckPuzzleSolved() => PuzzleInfo.IsPuzzleSolved;

        /// <inheritdoc />
        public async UniTask ForceSolvePuzzle(bool instant)
        {
            SetCurrentValues(PuzzleInfo.ResultValues);

            if (!instant)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(.25f));
            }
        }

        private int _count;

        public CombinationDialPuzzle(DialPuzzleInfo puzzleInfo) => SetupPuzzle(puzzleInfo);

        public void SetupPuzzle(DialPuzzleInfo puzzleInfo)
        {
            if (!puzzleInfo.Validate())
            {
                Debug.LogError("ERROR : Puzzle Info is not valid!");
                return;
            }

            SetUpDialSelectionOrientation();

            PuzzleInfo = puzzleInfo;
            _count = PuzzleInfo.CurrentValues.Count;


            //  Initialize/Setup each Dials
            Dials = new IDial[_count];

        }

        public void PopulateDial(IDial[] dials)
        {
            for (int i = 0; i < dials.Length; i++)
            {
                Dials[i] = dials[i];
                Dials[i].DialIndexInfo.OnValueChanged += DialIndexValueChanged;
            }
        }

        protected void DialIndexValueChanged(BaseDialInfo dialIndexInfo)
        {
            PuzzleInfo.SetCurrentValues(dialIndexInfo.IndexID, dialIndexInfo.CurrentValue); //_selectedDial = IndexID;
            OnDialUpdated?.Invoke(dialIndexInfo);
        }


        public DateTime StartTime => PuzzleInfo.StartTime;
        public void ResetPuzzle() => StopPuzzle();

        public void StartPuzzle()
        {
            if (!IsPuzzleStarted)
            {
                IsPuzzleStarted = true; // Invoke OnPuzzleStart 
                SelectedDial = 0; // Fire OnSelectedDialChanged

                CheckConditionalSolvePuzzle();
            }
            else
            {
                Debug.Log("Puzzle is already started");
            }
        }

        public void SetUpDialSelectionOrientation(bool isHorizontalOrientation = true)
        {
            _dialSelectionHorizontally = isHorizontalOrientation;
        }

        public void AdjustDial(Direction direction)
        {
            if (!IsPuzzleStarted)
                return;
            if (PuzzleInfo.IsPuzzleSolved)
                return;

            switch (direction)
            {
                case Direction.Up:
                    Dials[SelectedDial].AdjustValue(1);
                    break;
                case Direction.Down:
                    Dials[SelectedDial].AdjustValue(-1);
                    break;
                case Direction.Right:
                    SelectedDial++;

                    break;
                case Direction.Left:
                    SelectedDial--;

                    break;
                default:
                    Debug.Log($"AdjustDial Error! : {direction}");
                    break;
            }

            if (PuzzleInfo.IsPuzzleSolved)
                OnPuzzleSolved?.Invoke();
        }

        public void AdjustDialWithValue(Direction direction, int newValue = 0)
        {
            if (!IsPuzzleStarted)
                return;
            if (PuzzleInfo.IsPuzzleSolved)
                return;

            switch (direction)
            {
                case Direction.Up:
                    if (_dialSelectionHorizontally)
                        Dials[SelectedDial].SetValue(newValue);
                    else
                        SelectedDial++;
                    break;
                case Direction.Down:
                    if (_dialSelectionHorizontally)
                        Dials[SelectedDial].SetValue(-newValue);
                    else
                        SelectedDial--;
                    break;
                case Direction.Right:
                    if (_dialSelectionHorizontally)
                        SelectedDial++;
                    else
                        Dials[SelectedDial].SetValue(newValue);
                    break;
                case Direction.Left:
                    if (_dialSelectionHorizontally)
                        SelectedDial--;
                    else
                        Dials[SelectedDial].SetValue(-newValue);
                    break;
                default:
                    Debug.Log($"AdjustDial Error! : {direction}");
                    break;
            }

            CheckConditionalSolvePuzzle();
        }
        public void AdjustDial(Vector2 input)
        {
            if (input.y > 0)
            {
                AdjustDial(Direction.Up);
            }
            else if (input.y < 0)
            {
                AdjustDial(Direction.Down);
            }
            else if (input.x > 0)
            {
                AdjustDial(Direction.Right);
            }
            else if (input.x < 0)
            {
                AdjustDial(Direction.Left);
            }
        }

        public void ShowHint()
        {
            throw new NotImplementedException();
        }

        public void StopPuzzle()
        {
            if (!IsPuzzleStarted) return;
            for (int i = 0; i < Dials.Length; i++)
            {
                Dials[i].DialIndexInfo.OnValueChanged -= DialIndexValueChanged;
            }

            Dials = null;
            PuzzleInfo = null;
            IsPuzzleStarted = false;
        }

        public void SetCurrentValues(int index, int newCurrentValue)
        {
            var wasPuzzleSolevd = PuzzleInfo.IsPuzzleSolved;
            Dials[index].AdjustValue(newCurrentValue);
            PuzzleInfo.SetCurrentValues(index, Dials[index].DialIndexInfo.CurrentValue);
            if (!wasPuzzleSolevd)
            {
                CheckConditionalSolvePuzzle();
            }
        }

        public int SelectedDialIndex => _selectedDial;  

        /// <summary>
        /// Updates the current values of the puzzle dials and checks if the puzzle is solved.
        /// </summary>
        /// <param name="newCurrentValues">The new values for the puzzle dials.</param>
        public void SetCurrentValues(List<int> newCurrentValues)
        {
            var wasPuzzleSolevd = PuzzleInfo.IsPuzzleSolved;

            for (int i = 0; i < newCurrentValues.Count; i++)
            {
                Dials[i].AdjustValue(newCurrentValues[i]);
                PuzzleInfo.SetCurrentValues(i, Dials[i].DialIndexInfo.CurrentValue);
            }

            if (!wasPuzzleSolevd)
            {
                CheckConditionalSolvePuzzle();
            }
        }


        public void CheckConditionalSolvePuzzle(bool pressedKey = false)
        {
            if (!PuzzleInfo.NeedKeyPress || pressedKey)
            {
                if (PuzzleInfo.IsPuzzleSolved)
                    OnPuzzleSolved?.Invoke();
            }
        }
    }
}