using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio23.SS2.PuzzleSystem.Data
{
    /// <summary>
    /// Represents information about a puzzle, including its name, result values, current values, solved status, hints, and solving time.
    /// </summary>
    public class DialPuzzleInfo
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
        public bool IsPuzzleSolved => CheckPuzzleSolved();

        public bool NeedKeyPress = false;

        private bool CheckPuzzleSolved()
        {
            var isPuzzleUnlock = true;
            for (int i = 0; i < ResultValues.Count; i++)
            {
                if (ResultValues[i] != CurrentValues[i])
                {
                    isPuzzleUnlock = false;
                    break;
                }
            }

            return isPuzzleUnlock;
        }
        
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
        public DialPuzzleInfo(string puzzleName, int minValue, int maxValue, List<int> resultValues, List<int> currentValues, List<PuzzleHints> puzzleHints, bool needKeyPress)
        {
            PuzzleName = puzzleName;
            MinValue = minValue;
            MaxValue = maxValue;
            ResultValues = resultValues;
            CurrentValues = currentValues;
            PuzzleHints = puzzleHints;
            NeedKeyPress = needKeyPress;
            // Save the current time as the start time
            StartTime = DateTime.Now;
        }

        /// <summary>
        /// Updates the current values of the puzzle dials and checks if the puzzle is solved.
        /// </summary>
        /// <param name="newCurrentValues">The new values for the puzzle dials.</param>
        public void SetCurrentValues(List<int> newCurrentValues)
        {
            for (int i = 0; i < newCurrentValues.Count; i++)
            {
                SetCurrentValues(i, newCurrentValues[i]);
            }
        }

        /// <summary>
        /// Updates the value of a specific dial at the given index and checks if the puzzle is solved.
        /// </summary>
        /// <param name="index"> This index positioned value will be updated</param>
        /// <param name="newCurrentValue">The new value for the currentValue item</param>
        public void SetCurrentValues(int index, int newCurrentValue)
        {
            // Modified the newCurrentValue to be in range of MinValue and MaxValue
            newCurrentValue = (newCurrentValue > MaxValue) ? MinValue : (newCurrentValue < MinValue) ? MaxValue : newCurrentValue;
            
            if (index >= 0 && index < CurrentValues.Count)
            {
                CurrentValues[index] = newCurrentValue;
            }
            else
            {
                Debug.LogError($"{index} Index out of range");
            }
        }
        /// <summary>
        /// Validates the puzzle information.
        /// </summary>
        /// <param name="puzzleInfo">The puzzle information to validate.</param>
        /// <returns>True if the puzzle information is valid, otherwise false.</returns>
        public bool Validate()
        {
            // 1. MaxValue should be greater than MinValue
            if (MaxValue <= MinValue)
            {
                return false;
            }

            // 2. CurrentValues and ResultValues should be within the range of MinValue and MaxValue
            foreach (var value in CurrentValues)
            {
                if (value < MinValue || value > MaxValue)
                {
                    return false;
                }
            }

            foreach (var value in ResultValues)
            {
                if (value < MinValue || value > MaxValue)
                {
                    return false;
                }
            }

            // 3. CurrentValues and ResultValues capacity should be equal
            if (CurrentValues.Count != ResultValues.Count)
            {
                return false;
            }

            return true;
        }
    }
}
