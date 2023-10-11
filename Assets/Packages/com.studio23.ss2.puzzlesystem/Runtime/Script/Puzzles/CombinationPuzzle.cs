using System;
using System.Collections;
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

       

       public bool IsPuzzleStarted { get; set; }
        public List<IDial> Dials { get; set; }
        public PuzzleInfo PuzzleInfo { get; set; }
        public event Action OnPuzzleStart;
        public event Action OnPuzzleStop;
        public event Action<int> OnSelectedDialChanged;
        public event Action<DialInfo> OnDialValueChanged;
       
        public void IPuzzle(PuzzleInfo puzzleInfo)
        {
            throw new NotImplementedException();
        }
        
        public CombinationPuzzle(PuzzleInfo puzzleInfo)
        {
            // todo: Initialize Puzzle Info
            if (puzzleInfo.ResultValues.Capacity == puzzleInfo.CurrentValues.Capacity)
            {
                PuzzleInfo = puzzleInfo;
                Dials = new List<IDial>(puzzleInfo.ResultValues.Capacity);
                SetupPuzzle();
            }
            else
            {
                Debug.LogError("ResultValues and CurrentValues Capacity are not same");
            }
        }

        public void SetupPuzzle()
        { 
            //  Initialize/Setup each Dials
            var capacity = PuzzleInfo.ResultValues.Count;
            Dials = new List<IDial>(capacity);
            
            for (int i = 0; i < capacity; i++)
            {
                DialInfo dialInfo = new DialInfo(i, PuzzleInfo.CurrentValues[i], 0, 9);
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
                IsPuzzleStarted = true;
                SelectedDial = 0; // Dials Index Id
                // Invoke OnPuzzleStart 
                OnPuzzleStart?.Invoke();
            }
            else
            {
                Debug.LogError("Puzzle is already started");
            }
        }
        public void Move(Vector2 input)
        {
           if(!IsPuzzleStarted) return;
           Debug.Log($"input : {input}");
           if(input.x > 0)
           {
               Dials[SelectedDial].AdjustValue(1);
           }
           else if(input.x < 0)
           {
               Dials[SelectedDial].AdjustValue(-1);
           }
           else if(input.y > 0)
           {
               SelectedDial++;
               if(SelectedDial >= Dials.Count) SelectedDial = 0;
               OnSelectedDialChanged?.Invoke(SelectedDial);
           }
           else if(input.y < 0)
           {
               SelectedDial--;
               if(SelectedDial < 0) SelectedDial = Dials.Count - 1;
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
           
           for (int i = 0; i < Dials.Count; i++)
           {
               Dials[i].DialInfo.OnValueChanged -= OnDialValueChanged;
           }
          
           Dials.Clear();
           PuzzleInfo = null;
           IsPuzzleStarted = false;
           OnPuzzleStop?.Invoke();
        }
    }

}