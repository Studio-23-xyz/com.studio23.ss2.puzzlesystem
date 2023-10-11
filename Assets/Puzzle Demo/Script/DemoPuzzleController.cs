using System;
using System.Collections;
using System.Collections.Generic;
using Studio23.SS2.PuzzleSystem;
using UnityEngine;


namespace Studio23.SS2.PuzzleDemo
{
    public class DemoPuzzleController : MonoBehaviour
    {
        private void Start()
        {
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
            
            CombinationPuzzle combinationPuzzle = new CombinationPuzzle(puzzleInfo);
           
            combinationPuzzle.OnSelectedDialChanged += OnSelectedDialChanged;
            combinationPuzzle.OnDialValueChanged += OnDialValueChanged;
            combinationPuzzle.PuzzleInfo.OnPuzzleUnlocked += OnPuzzleUnlocked;
        }
        private void OnSelectedDialChanged(int obj)
        {
            Debug.Log("Currently selected dial : " + obj);
        }
        private void OnDialValueChanged(DialInfo obj)
        {
            Debug.Log("Dial Value Changed : " + obj.CurrentValue);
        }

        private void OnPuzzleUnlocked()
        {
            Debug.Log("Puzzle Unlocked");
        }

        

        
    }
}
