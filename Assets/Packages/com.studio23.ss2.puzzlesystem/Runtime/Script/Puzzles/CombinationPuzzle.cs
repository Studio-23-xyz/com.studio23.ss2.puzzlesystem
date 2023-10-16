using System;
using Studio23.SS2.PuzzleSystem.Data;
using Studio23.SS2.PuzzleSystem.Interface;
using UnityEngine;

namespace Studio23.SS2.PuzzleSystem.Core
{
    public class CombinationPuzzle : IPuzzle
    { 
        private int selectedDial { get; set; }

       public int SelectedDial {
            get => selectedDial;
            set
            {
                selectedDial = value;
                OnSelectedDialChanged?.Invoke(selectedDial);
            }
        }

       private bool isPuzzleStarted;
       public bool IsPuzzleStarted
       {
           get { return isPuzzleStarted;}
           set
           {
               if(isPuzzleStarted != value)
               {
                   isPuzzleStarted = value;
                   if(isPuzzleStarted)
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
        
        private int capacity;
       
        public CombinationPuzzle(PuzzleInfo puzzleInfo)=>SetupPuzzle(puzzleInfo);

        public void SetupPuzzle(PuzzleInfo puzzleInfo)
        {
            if (!puzzleInfo.Validate())
            {
                Debug.LogError("ERROR : Puzzle Info is not valid!");
                return;
            }
            
            PuzzleInfo = puzzleInfo;
            capacity = PuzzleInfo.ResultValues.Capacity;
             
            
            //  Initialize/Setup each Dials
            Dials = new IDial[capacity];
            
            for (int i = 0; i < capacity; i++)
            {
                DialInfo dialInfo = new DialInfo(i, PuzzleInfo.CurrentValues[i], PuzzleInfo.MinValue, PuzzleInfo.MaxValue);
                var newDial = new DialController(dialInfo);
                Dials[i] = newDial;
               
                Dials[i].DialInfo.OnValueChanged += DialValueChanged;
            }
        }

        

        private void DialValueChanged(DialInfo obj)
        {
            OnDialValueChanged?.Invoke(obj);
            PuzzleInfo.SetCurrentValues(obj.IndexID, obj.CurrentValue); //selectedDial = IndexID;
        }

       

        public void ResetPuzzle()
        {
            throw new NotImplementedException();
        }

        public void StartPuzzle()
        {
            if(!IsPuzzleStarted)
            {
                IsPuzzleStarted = true;// Invoke OnPuzzleStart 
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

            // Debug.Log($"direction: {direction}");
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
                    if (SelectedDial >= Dials.Length) SelectedDial = 0;
                    OnSelectedDialChanged?.Invoke(SelectedDial);
                    break;
                case Direction.Left:
                    SelectedDial--;
                    if (SelectedDial < 0) SelectedDial = Dials.Length - 1;
                    OnSelectedDialChanged?.Invoke(SelectedDial);
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
            if(!IsPuzzleStarted) 
                return;
            if(PuzzleInfo.IsPuzzleSolved) 
                return;
            
            if(input.y > 0)
            {
                Dials[SelectedDial].AdjustValue(1);
            }
            else if(input.y < 0)
            {
                Dials[SelectedDial].AdjustValue(-1);
            }
          
            if(input.x > 0)
            {
                SelectedDial++;
                if(SelectedDial >= Dials.Length) SelectedDial = 0;
                OnSelectedDialChanged?.Invoke(SelectedDial);
            }
            else if(input.x < 0)
            {
                SelectedDial--;
                if(SelectedDial < 0) SelectedDial = Dials.Length - 1;
                OnSelectedDialChanged?.Invoke(SelectedDial);
            }
            if (PuzzleInfo.IsPuzzleSolved) 
                OnPuzzleUnlocked?.Invoke();
        }
        public void ShowHint()
        {
            throw new NotImplementedException();
        }

        public void StopPuzzle()
        {
            // todo: Hide Puzzle Visuals
            // todo: Unsubscribe to Dials Event
            
            if(!IsPuzzleStarted) return; 
            
            
           for (int i = 0; i < Dials.Length; i++)
           {
               Dials[i].DialInfo.OnValueChanged -= OnDialValueChanged;
           }
          
           Dials = null;
           
          // PuzzleInfo.OnPuzzleSolved -= OnPuzzleSolved;
           PuzzleInfo = null;
           
           IsPuzzleStarted = false; // invoke OnPuzzleStop
           
        }

        
        
    }

}