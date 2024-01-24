using System;
using Studio23.SS2.PuzzleSystem.Data;
using Studio23.SS2.PuzzleSystem.Interface;
using UnityEngine;

namespace Studio23.SS2.PuzzleSystem.Core
{
    public class CombinationPuzzle : IPuzzle
    { 
        private int _selectedDial { get; set; }

       public int SelectedDial {
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
           get { return _isPuzzleStarted;}
           set
           {
               if(_isPuzzleStarted != value)
               {
                   _isPuzzleStarted = value;
                   if(_isPuzzleStarted)
                   {
                       OnPuzzleStart?.Invoke();
                   }
                   else
                   {
                      OnPuzzleStop?.Invoke();
                   }
               }
           }
       }
        public IDial[] Dials { get; set; }
        public PuzzleInfo PuzzleInfo { get; set; }
        public event Action OnPuzzleStart;
        public event Action OnPuzzleStop;
        public event Action<int> OnSelectedDialChanged;
        public event Action<DialInfo> OnDialValueChanged;
        
        public Action OnPuzzleUnlocked;
        
        private int _count;
       
        public CombinationPuzzle(PuzzleInfo puzzleInfo)=>SetupPuzzle(puzzleInfo);

        public void SetupPuzzle(PuzzleInfo puzzleInfo)
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
                DialInfo dialInfo = new DialInfo(i, PuzzleInfo.CurrentValues[i], PuzzleInfo.MinValue, PuzzleInfo.MaxValue);
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

       

        public void ResetPuzzle() => StopPuzzle();

        public void StartPuzzle()
        {
            if(!IsPuzzleStarted)
            {
                IsPuzzleStarted = true; // Invoke OnPuzzleStart 
                SelectedDial = 0; // Fire OnSelectedDialChanged
                
                if (PuzzleInfo.IsPuzzleSolved)
                {
                    OnPuzzleUnlocked.Invoke();
                }
            }
            else
            {
                Debug.Log("Puzzle is already started");
            }
        }
       
        public void Move(Direction direction)
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
                   Debug.Log($"Move Error! : {direction}");
                    break;
            }

            if (PuzzleInfo.IsPuzzleSolved) 
                OnPuzzleUnlocked?.Invoke();

        }
      
        public void Move(Vector2 input)
        {
            if(input.y > 0)
            {
                Move(Direction.Up);
            }
            else if(input.y < 0)
            {
               Move(Direction.Down);
            }
            else if(input.x > 0)
            {
               Move( Direction.Right);
            }
            else if(input.x < 0)
            {
                Move( Direction.Left);
            }
        }
        
        public void ShowHint()
        {
            throw new NotImplementedException();
        }

        public void StopPuzzle()
        {
            if(!IsPuzzleStarted) return; 
           for (int i = 0; i < Dials.Length; i++)
           {
               Dials[i].DialInfo.OnValueChanged -= OnDialValueChanged;
           }
           Dials = null;
           PuzzleInfo = null;
           IsPuzzleStarted = false;  
           
        }
    }
}