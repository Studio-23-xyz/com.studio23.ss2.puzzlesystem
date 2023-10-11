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
            get
            {
                return selectedDial;
            }
            set
            {
                selectedDial = value;
                OnSelectedDialChanged?.Invoke(selectedDial);
            }
            
        }
        public List<IDial> Dials { get; set; }
        public PuzzleInfo PuzzleInfo { get; set; }
        public event Action OnPuzzleUnlocked;
        public event Action OnPuzzleStart;
        public event Action OnPuzzleReset;
        public event Action<int> OnSelectedDialChanged;
        public event Action<DialInfo> OnDialValueChanged;
        // Constructor for PuzzleController class.
        public CombinationPuzzle(PuzzleInfo puzzleInfo)
        {
            // todo: Initialize Puzzle Info
            if(puzzleInfo.ResultValues.Capacity == puzzleInfo.CurrentValues.Capacity)
                PuzzleInfo = puzzleInfo;
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
                Dials[i] = new DialController();
                DialInfo dialInfo = new DialInfo(i, PuzzleInfo.CurrentValues[i], 0, 9);
                Dials[i].Initialize(dialInfo);  
                Dials[i].OnValueChanged += OnDialValueChanged;
            }
            
            OnDialValueChanged += DialValueChanged;
        }

        private void DialValueChanged(DialInfo obj)
        {
            //PuzzleInfo.SetCurrentValues(selectedDial, obj.CurrentValue);
            PuzzleInfo.SetCurrentValues(obj.IndexID, obj.CurrentValue);
        }

        public void ResetPuzzle()
        {
            // todo :  Reset Dials to initial state
            // todo: Invoke OnPuzzleReset
        }

        public void StartPuzzle()
        {
            // Select dials 0 as SeletedDial Value
            // Invoke OnSelectedDialChanged 
           
            SelectedDial = 0; // Dials Index Id
             
           
            // todo: Show Puzzle Visuals
            // todo: Subscribe to Dials Event
            
            
            // Invoke OnPuzzleStart 
            OnPuzzleStart?.Invoke();
        }

        public bool CheckResult()
        {
           // todo: Check if all dials are unlocked
           // todo: Check if all dials are in correct position
           return false;
        }

        public void ShowHint()
        {
            throw new NotImplementedException();
        }

        public void ExitPuzzle()
        {
           // todo: Hide Puzzle Visuals
           // todo: Unsubscribe to Dials Event
        }

        public void SelectDial(int dialIndex)
        {
            // todo: Select a Dial as per input command
        }

        public void UnlockPuzzle()
        {
            // todo: Unlock Puzzle
            // todo: Invoke OnPuzzleUnlocked
            // todo: Save Puzzle State
            // todo: call Exit Puzzle
        }

       
    }

}