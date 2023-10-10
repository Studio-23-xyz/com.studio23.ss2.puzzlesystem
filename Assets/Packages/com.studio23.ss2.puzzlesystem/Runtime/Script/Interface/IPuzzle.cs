using System;
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
        /// Verifies whether the current combination of dial values matches the correct solution (ResultValues).
        /// </summary>
        /// <returns>True if the puzzle is solved, false otherwise.</returns>
        bool CheckResult();

        /// <summary>
        /// Provides a hint or clue to the player, aiding them in solving the puzzle.
        /// Useful for UI feedback.
        /// </summary>
        void ShowHint();

        /// <summary>
        /// Allows the player to exit the puzzle, ending their current session.
        /// Hides visual puzzle. Useful for UI action. Unsubscribes dials event.
        /// </summary>
        void ExitPuzzle();

        /// <summary>
        /// Useful to select dial as per input command.
        /// </summary>
        /// <param name="dialIndex">Index of the dial to be selected.</param>
        void SelectDial(int dialIndex);

        /// <summary>
        /// Unlocks and opens the puzzle, indicating that it has been successfully solved.
        /// Saves the puzzle state to unlock. Invokes OnPuzzleUnlocked event.
        /// </summary>
        void UnlockPuzzle();

        #endregion

        #region Properties

        /// <summary>
        /// Currently selected dial.
        /// </summary>
        IDial SelectedDial { get; set; }

        /// <summary>
        /// All dials information.
        /// </summary>
        IDial[] Dials { get; }

        /// <summary>
        /// Stores information about the puzzle's configuration, including its dials, hints, and solution.
        /// </summary>
        PuzzleInfo PuzzleInfo { get; }

        #endregion

        #region Events

        /// <summary>
        /// Triggered when the player successfully unlocks and solves the puzzle.
        /// </summary>
        event Action OnPuzzleUnlocked;

        /// <summary>
        /// Triggered when the puzzle is started
        /// </summary>
        event Action OnPuzzleStart;
        
        /// <summary>
        /// Triggered when the puzzle is reset to its initial state, either by player action or automatically.
        /// </summary>
        event Action OnPuzzleReset;

        #endregion
    }
}
