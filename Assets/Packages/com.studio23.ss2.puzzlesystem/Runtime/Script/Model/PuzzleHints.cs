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
}