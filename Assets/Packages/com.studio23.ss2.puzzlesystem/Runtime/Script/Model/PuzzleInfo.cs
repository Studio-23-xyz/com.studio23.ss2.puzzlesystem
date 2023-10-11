using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio23.SS2.PuzzleSystem
{
 

    public class PuzzleHints
    {
        public string Message { get; set; }
        public Sprite Symbol { get; set; }
    }

    public class PuzzleInfo
    {
        public string PuzzleName { get; set; }
        public List<int> ResultValues { get; set; }
        public List<int> CurrentValues { get; set; }
       
        public bool IsPuzzleSolved { get; set; }
         
        public Action OnPuzzleUnlocked;
        
        public List<PuzzleHints> PuzzleHints { get; set; }
        public float PuzzleTime { get; set; }

        public PuzzleInfo(string puzzleName, List<int> resultValues, List<int> currentValues, bool IsPuzzleSolved, List<PuzzleHints> puzzleHints, float puzzleTime)
        {
            PuzzleName = puzzleName;
            ResultValues = resultValues;
            CurrentValues = currentValues;
            IsPuzzleSolved = IsPuzzleSolved;
            PuzzleHints = puzzleHints;
            PuzzleTime = puzzleTime;
        }

        // Method to update puzzle result values and check if the puzzle is solved
        /*public void SetPuzzleResult(List<int> newResultValues)
        {
            ResultValues = newResultValues;
            CheckPuzzleStatus();
        }*/

        // Method to update current values of the dials and check if the puzzle is solved
        public void SetCurrentValues(List<int> newCurrentValues)
        {
            CurrentValues = newCurrentValues;
            CheckPuzzleStatus();
        }
       
        public void SetCurrentValues(int index, int newCurrentValue)
        {
            if ( index < CurrentValues.Count )
            {
                CurrentValues[index] = newCurrentValue;
                CheckPuzzleStatus();
            }else
            {
                Debug.LogError("Index out of range");
            }
           
        }
        /// <summary>
        /// Verifies whether the current combination of dial values matches the correct solution (ResultValues).
        /// </summary>
        private void CheckPuzzleStatus()
        {
            bool isPuzzleSolved = true;
            for (int i = 0; i < ResultValues.Count; i++)
            {
                if (ResultValues[i] != CurrentValues[i])
                {
                    isPuzzleSolved = false;
                    break;
                }
            }

            IsPuzzleSolved = isPuzzleSolved;
            
            // If the puzzle is solved, invoke the OnPuzzleSolved event
            if (IsPuzzleSolved)
            {
                OnPuzzleUnlocked.Invoke();
            }
        }
    }

}