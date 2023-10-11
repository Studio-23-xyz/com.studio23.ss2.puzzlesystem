using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio23.SS2.PuzzleSystem
{
    /// <summary>
    /// Represents a hint for the puzzle, containing a message and a symbol.
    /// </summary>
    public class PuzzleHints
    {
        /// <summary>
        /// Gets or sets the hint message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the symbol associated with the hint.
        /// </summary>
        public Sprite Symbol { get; set; }
    }

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
        /// Gets or sets the list of correct values for the puzzle.
        /// </summary>
        public List<int> ResultValues { get; set; }

        /// <summary>
        /// Gets or sets the list of current values of the puzzle dials.
        /// </summary>
        public List<int> CurrentValues { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the puzzle is solved.
        /// </summary>
        public bool IsPuzzleSolved { get; set; }

        /// <summary>
        /// Event invoked when the puzzle is unlocked.
        /// </summary>
        public Action OnPuzzleUnlocked;

        /// <summary>
        /// Gets or sets the list of hints for the puzzle.
        /// </summary>
        public List<PuzzleHints> PuzzleHints { get; set; }

        /// <summary>
        /// Gets or sets the time taken to solve the puzzle.
        /// </summary>
        public float PuzzleTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the PuzzleInfo class with the specified properties.
        /// </summary>
        public PuzzleInfo(string puzzleName, List<int> resultValues, List<int> currentValues, bool isPuzzleSolved, List<PuzzleHints> puzzleHints, float puzzleTime)
        {
            PuzzleName = puzzleName;
            ResultValues = resultValues;
            CurrentValues = currentValues;
            IsPuzzleSolved = isPuzzleSolved;
            PuzzleHints = puzzleHints;
            PuzzleTime = puzzleTime;
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
