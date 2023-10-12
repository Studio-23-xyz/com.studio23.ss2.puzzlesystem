using System;
using System.Collections.Generic;
using Studio23.SS2.PuzzleSystem.Interface;
using UnityEngine;

namespace Studio23.SS2.PuzzleSystem
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
            PuzzleInfo.OnPuzzleSolved += OnPuzzleSolved;
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

        private void OnPuzzleSolved()
        {
            OnPuzzleUnlocked?.Invoke();
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
                PuzzleInfo.CheckPuzzleStatus(); // check if puzzle is solved
            }
            else
            {
                Debug.Log("Puzzle is already started");
            }
        }
        public void Move(Vector2 input)
        {
           if(!IsPuzzleStarted) return;
           // Debug.Log($"input : {input}");
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
           
           PuzzleInfo.OnPuzzleSolved -= OnPuzzleSolved;
           PuzzleInfo = null;
           
           IsPuzzleStarted = false; // invoke OnPuzzleStop
           
        }
        
        
      
        
    }

}