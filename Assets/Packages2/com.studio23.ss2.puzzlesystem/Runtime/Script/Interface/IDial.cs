using System;

namespace Studio23.SS2.PuzzleSystem.Interface
{
    /// <summary>
    /// Interface for a dial that can be rotated, moved, selected, and deselected by the player.
    /// </summary>
    public interface IDial
    {
        
        /// <summary>
        /// Represents information about a dial, including its unique identifier, current value, minimum value, and maximum value.
        /// </summary>
        public DialInfo DialInfo { get; set; }
        
        /// <summary>
        /// Updates/Sets the dial to a new value while rotating or moving. Invokes the OnValueChanged event.
        /// </summary>
        /// <param name="value">New value for the dial.</param>
        public void AdjustValue(int value);

    }
}