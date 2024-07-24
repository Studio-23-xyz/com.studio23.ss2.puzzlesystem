using System;
using System.Collections.Generic;
using Studio23.SS2.PuzzleSystem.Data;
using UnityEngine;

namespace Studio23.SS2.PuzzleSystem.Interface
{
    /// <summary>
    /// Interface for puzzle interactions.
    /// </summary>
    public interface IDialPuzzle : IPuzzle
    {
        DialPuzzleInfo PuzzleInfo { get; }
        #region Actions

        /// <summary>
        /// Initializes the puzzle with puzzle information. you can use constructor instead of this method.
        /// </summary>
        /// <param name="dialPuzzleInfo">Represents information about a puzzle, including its name, min-max value, result values, current values, solved status, hints, and solving time.</param>
        void SetupPuzzle(DialPuzzleInfo dialPuzzleInfo);


        /// <summary>
        /// Handles player input to move the dial.
        /// </summary>
        /// <param name="input">Input vector representing player movement.</param>
        public void AdjustDial(Vector2 input);
       
        /// <summary>
        /// Handles player input to move the dial.
        /// </summary>
        /// <param name="input">Input vector representing player movement.</param>
        public void AdjustDial(Direction input);

        /// <summary>
        /// Updates the current values of the puzzle dials and checks if the puzzle is solved.
        /// </summary>
        /// <param name="newCurrentValues">The new values for the puzzle dials.</param>
        public void SetCurrentValues(List<int> newCurrentValues);

        /// <summary>
        /// Updates the value of a specific dial at the given index and checks if the puzzle is solved.
        /// </summary>
        /// <param name="index"> This index positioned value will be updated</param>
        /// <param name="newCurrentValue">The new value for the currentValue item</param>
        public void SetCurrentValues(int index, int newCurrentValue);
        #endregion

        #region Properties

        /// <summary>
        /// Currently selected dial.
        /// </summary>
        int SelectedDialIndex { get; }

        /// <summary>
        /// Puzzle all dials information.
        /// </summary>
        IDial[] Dials { get; set; }
        #endregion

        #region Events

        /// <summary>
        /// Event triggered when dial selection changed.
        /// </summary>
        public event Action<int> OnSelectedDialChanged;
        /// <summary>
        /// Fired when Puzzle is Solved first time
        /// </summary>
        public event Action OnPuzzleSolved;
        public event Action<DialInfo> OnDialValueChanged;

        #endregion
    }
}
