using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio23.SS2.PuzzleSystem
{
    /// <summary>
    /// Represents information about a puzzle, including its name, result values, current values, solved status, hints, and solving time.
    /// </summary>
    public class PuzzleInfo
    {
        /// <summary>
        /// Gets or sets the name of the puzzle.
        /// </summary>
        public string PuzzleName { get; set; }

        /// <summary>
        /// Gets or sets the minimum values of the puzzle dials.
        /// </summary>
        public int MinValue { get; set; }
        
        /// <summary>
        /// Gets or sets the maximum values of the puzzle dials.
        /// </summary>
        public int MaxValue { get; set; }
        
        /// <summary>
        /// Gets or sets the list of current values of the puzzle dials.
        /// </summary>
        public List<int> CurrentValues { get; set; }
        
        /// <summary>
        /// Gets or sets the list of correct values for the puzzle.
        /// </summary>
        public List<int> ResultValues { get; set; }
        
        
        

        /// <summary>
        /// Gets or sets a value indicating whether the puzzle is solved.
        /// </summary>
        public bool IsPuzzleSolved { get; set; }

        /// <summary>
        /// Event invoked when the puzzle is unlocked.
        /// </summary>
        public Action OnPuzzleSolved;
        
        /// <summary>
        /// Gets or sets the list of hints for the puzzle.
        /// </summary>
        public List<PuzzleHints> PuzzleHints { get; set; }

        /// <summary>
        /// Gets the time puzzle has been elapsed in seconds.
        /// </summary>
        public float PuzzleTime
        {
            get
            {
                // Calculate the elapsed time since the puzzle was initialized
                TimeSpan elapsed = DateTime.Now - StartTime;
                return (float)elapsed.TotalSeconds;
            }
        }
        /// <summary>
        /// Gets or sets the start time when the puzzle is initialized.
        /// </summary>
        public DateTime StartTime { get; private set; }
        
        /// <summary>
        /// Initializes a new instance of the PuzzleInfo class with the specified properties.
        /// </summary>
        public PuzzleInfo(string puzzleName, int minValue, int maxValue, List<int> resultValues, List<int> currentValues, bool isPuzzleSolved, List<PuzzleHints> puzzleHints)
        {
            PuzzleName = puzzleName;
            MinValue = minValue;
            MaxValue = maxValue;
            ResultValues = resultValues;
            CurrentValues = currentValues;
            IsPuzzleSolved = isPuzzleSolved;
            PuzzleHints = puzzleHints;
            
            // Save the current time as the start time
            StartTime = DateTime.Now;
        }

        /// <summary>
        /// Updates the current values of the puzzle dials and checks if the puzzle is solved.
        /// </summary>
        /// <param name="newCurrentValues">The new values for the puzzle dials.</param>
        public void SetCurrentValues(List<int> newCurrentValues)
        {
            CurrentValues = newCurrentValues;
            CheckPuzzleStatus();
        }

        /// <summary>
        /// Updates the value of a specific dial at the given index and checks if the puzzle is solved.
        /// </summary>
        /// <param name="index"> This index positioned value will be updated</param>
        /// <param name="newCurrentValue">The new value for the currentValue item</param>
        public void SetCurrentValues(int index, int newCurrentValue)
        {
            if (index >= 0 && index < CurrentValues.Count)
            {
                CurrentValues[index] = newCurrentValue;
                CheckPuzzleStatus();
            }
            else
            {
                Debug.LogError("Index out of range");
            }
        }

        /// <summary>
        /// Verifies whether the current combination of dial values matches the correct solution (ResultValues).
        /// </summary>
        public void CheckPuzzleStatus()
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
                OnPuzzleSolved.Invoke();
                
            }
        }
    }
}
