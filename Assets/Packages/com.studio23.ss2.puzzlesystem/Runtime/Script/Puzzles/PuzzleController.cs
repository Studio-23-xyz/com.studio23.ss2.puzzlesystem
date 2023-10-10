using System;
using System.Collections;
using System.Collections.Generic;
using Studio23.SS2.PuzzleSystem.Interface;
using UnityEngine;

namespace Studio23.SS2.PuzzleSystem
{
    public class PuzzleController : MonoBehaviour, IPuzzle
    {
        public IDial SelectedDial { get; set; }
        public IDial[] Dials { get; set; }
        public PuzzleInfo PuzzleInfo { get; set; }
        public event Action OnPuzzleUnlocked;
        public event Action OnPuzzleStart;
        public event Action OnPuzzleReset;

        private void Awake()
        {
            SetupPuzzle();
        }

        public void SetupPuzzle()
        { 
            // todo: Initialize Puzzle Info
            int capacity = 4;
            var resultValue = new List<int>(capacity) {1, 2, 3, 4};
            var currentValue = new List<int>(capacity) {0, 0, 0, 0};
            var puzzleInfo = new PuzzleInfo(
                "DialCombinationPuzzle", 
                resultValue, 
                currentValue,
                false, 
                new List<PuzzleHints>(), 
                0);
            
            PuzzleInfo = puzzleInfo;
            // todo: Initialize/Setup each Dials
            Dials = new IDial[capacity];
            for (int i = 0; i < capacity; i++)
            {
                Dials[i] = new DialController();
                Dials[i].Initialize();
            }
        }

        public void ResetPuzzle()
        {
            // todo: Reset Dials to initial state
            // todo: Select dials 0 as SeletedDial Value
            // todo: Invoke OnPuzzleReset
        }

        public void StartPuzzle()
        {
           // todo: Show Puzzle Visuals
           // todo: Subscribe to Dials Event
           // todo: Select dials 0 as SeletedDial Value
           // todo: Invoke OnPuzzleStart 
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