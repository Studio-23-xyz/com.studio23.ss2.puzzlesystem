using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio23.SS2.PuzzleSystem.Interface
{
    /// <summary>
    /// Interface for puzzle interactions.
    /// </summary>
    public interface IPuzzle 
    {
        #region Actions

        /// <summary>
        /// Initializes the puzzle with its initial configuration.
        /// Sets up all dials and parameters.
        /// </summary>
        void SetupPuzzle();

        /// <summary>
        /// Resets the puzzle to its initial state, clearing any progress made by the player.
        /// Invokes OnPuzzleReset event.
        /// </summary>
        void ResetPuzzle();

        /// <summary>
        /// Initiates the puzzle, allowing the player to interact with it and attempt to solve it.
        /// Shows puzzle visuals. Subscribes to the dials event.
        /// </summary>
        void StartPuzzle();

        
        /// <summary>
        /// Provides a hint or clue to the player, aiding them in solving the puzzle.
        /// Useful for UI feedback.
        /// </summary>
        void ShowHint();

        /// <summary>
        /// Allows the player to exit the puzzle, ending their current session.
        /// Hides visual puzzle. Useful for UI action. Unsubscribes dials event.
        /// </summary>
        void StopPuzzle();

        /// <summary>
        /// Handles player input to move the dial.
        /// </summary>
        /// <param name="input">Input vector representing player movement.</param>
        public void Move(Vector2 input);
        
        #endregion

        #region Properties
        /// <summary>
        /// Puzzle current status. stop user to start a puzzle if it is already started.
        /// </summary>
        bool IsPuzzleStarted { get; set; }
        
        /// <summary>
        /// Currently selected dial.
        /// </summary>
        int SelectedDial { get; set; }

        /// <summary>
        /// Puzzle all dials information.
        /// </summary>
        List<IDial> Dials { get; set; }

        /// <summary>
        /// Stores information about the puzzle's configuration, including its dials, hints, and solution.
        /// </summary>
        PuzzleInfo PuzzleInfo { get; }

        #endregion

        #region Events

        /// <summary>
        /// Triggered when the puzzle is started
        /// </summary>
        event Action OnPuzzleStart;

        /// <summary>
        /// Triggered when the puzzle is stop
        /// </summary>
        event Action OnPuzzleStop;
        
        /// <summary>
        /// Event triggered when dial selection changed.
        /// </summary>
        public event Action<int> OnSelectedDialChanged;
        
        #endregion
    }
}
