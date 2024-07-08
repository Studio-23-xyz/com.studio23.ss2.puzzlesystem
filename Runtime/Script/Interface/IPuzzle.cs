using System;
using System.Collections.Generic;
using Studio23.SS2.PuzzleSystem.Data;

namespace Studio23.SS2.PuzzleSystem.Interface
{
    public interface IPuzzle
    {
        /// <summary>
        /// Gets or sets the start time when the puzzle is initialized.
        /// </summary>
        public DateTime StartTime { get; }
        
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
        /// Puzzle current status. stop user to start a puzzle if it is already started.
        /// </summary>
        bool IsPuzzleStarted { get; set; }
        /// <summary>
        /// Gets or sets the list of hints for the puzzle.
        /// </summary>
        public List<PuzzleHints> PuzzleHints { get; set; }
        
        /// <summary>
        /// Triggered when the puzzle is started
        /// </summary>
        event Action OnPuzzleStart;

        /// <summary>
        /// Triggered when the puzzle is stop
        /// </summary>
        event Action OnPuzzleStop;
        event Action OnPuzzleSolved;
        public bool CheckPuzzleSolved();
    }
}