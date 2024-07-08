using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Studio23.SS2.PuzzleSystem.Data;
using Studio23.SS2.PuzzleSystem.Interface;
using UnityEngine;

[assembly: InternalsVisibleTo("editmode.tests")]

namespace Studio23.SS2.PuzzleSystem.Core
{
    public class CombinationDialPuzzle : IDialPuzzle
    {
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
        public event Action<DialInfo> OnDialValueChanged;

        public event Action  OnPuzzleSolved;
        public bool CheckPuzzleSolved() => PuzzleInfo.CheckPuzzleSolved();

        private int _count;

        public CombinationDialPuzzle(DialPuzzleInfo puzzleInfo) => SetupPuzzle(puzzleInfo);

        public void SetupPuzzle(DialPuzzleInfo puzzleInfo)
        {
            if (!puzzleInfo.Validate())
            {
                Debug.LogError("ERROR : Puzzle Info is not valid!");
                return;
            }

            PuzzleInfo = puzzleInfo;
            _count = PuzzleInfo.CurrentValues.Count;


            //  Initialize/Setup each Dials
            Dials = new IDial[_count];

            for (int i = 0; i < Dials.Length; i++)
            {
                DialInfo dialInfo = new DialInfo(i, PuzzleInfo.CurrentValues[i], PuzzleInfo.MinValue,
                    PuzzleInfo.MaxValue);
                var newDial = new DialController(dialInfo);
                Dials[i] = newDial;

                Dials[i].DialInfo.OnValueChanged += DialValueChanged;
            }
        }

        private void DialValueChanged(DialInfo dialInfo)
        {
            OnDialValueChanged?.Invoke(dialInfo);
            PuzzleInfo.SetCurrentValues(dialInfo.IndexID, dialInfo.CurrentValue); //_selectedDial = IndexID;
        }


        public DateTime StartTime => PuzzleInfo.StartTime;
        public void ResetPuzzle() => StopPuzzle();

        public void StartPuzzle()
        {
            if (!IsPuzzleStarted)
            {
                IsPuzzleStarted = true; // Invoke OnPuzzleStart 
                SelectedDial = 0; // Fire OnSelectedDialChanged

                if (PuzzleInfo.IsPuzzleSolved)
                {
                    OnPuzzleSolved?.Invoke();
                }
            }
            else
            {
                Debug.Log("Puzzle is already started");
            }
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
                Dials[i].DialInfo.OnValueChanged -= OnDialValueChanged;
            }

            Dials = null;
            PuzzleInfo = null;
            IsPuzzleStarted = false;
        }

        public void SetCurrentValuesInternal(int index, int newCurrentValue)
        {
            var wasPuzzleSolevd = PuzzleInfo.CheckPuzzleSolved();
            PuzzleInfo.SetCurrentValues(index, newCurrentValue);
            if (!wasPuzzleSolevd && PuzzleInfo.CheckPuzzleSolved())
            {
                OnPuzzleSolved?.Invoke();
            }
        }

        public int SelectedDialIndex => _selectedDial;  

        /// <summary>
        /// Updates the current values of the puzzle dials and checks if the puzzle is solved.
        /// </summary>
        /// <param name="newCurrentValues">The new values for the puzzle dials.</param>
        public void SetCurrentValues(List<int> newCurrentValues)
        {
            var wasPuzzleSolevd = PuzzleInfo.CheckPuzzleSolved();

            for (int i = 0; i < newCurrentValues.Count; i++)
            {
                PuzzleInfo.SetCurrentValues(i, newCurrentValues[i]);
            }

            if (!wasPuzzleSolevd && PuzzleInfo.CheckPuzzleSolved())
            {
                OnPuzzleSolved?.Invoke();
            }
        }
    }
}