using System;
using Studio23.SS2.PuzzleSystem.Data;
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
        /// Initializes the puzzle with puzzle information. you can use constructor instead of this method.
        /// </summary>
        /// <param name="puzzleInfo">Represents information about a puzzle, including its name, min-max value, result values, current values, solved status, hints, and solving time.</param>
        void SetupPuzzle(PuzzleInfo puzzleInfo);

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
        public void AdjustDial(Vector2 input);
       
        /// <summary>
        /// Handles player input to move the dial.
        /// </summary>
        /// <param name="input">Input vector representing player movement.</param>
        public void AdjustDial(Direction input);
        
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
        IDial[] Dials { get; set; }

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
