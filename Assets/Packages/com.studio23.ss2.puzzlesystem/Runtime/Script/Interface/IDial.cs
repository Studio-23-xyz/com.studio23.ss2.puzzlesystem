using System;
using System.Numerics;

namespace Studio23.SS2.PuzzleSystem.Interface
{
    /// <summary>
    /// Interface for a dial that can be rotated, moved, selected, and deselected by the player.
    /// </summary>
    public interface IDial
    {
        
        /// <summary>
        /// HSetup basic info on start or restart.
        /// </summary>
        public void Initialize();
        
        /// <summary>
        /// Handles player input to rotate the dial.
        /// </summary>
        /// <param name="input">Input vector representing player movement.</param>
        public void Rotated(Vector2 input);

        /// <summary>
        /// Handles player input to move the dial.
        /// </summary>
        /// <param name="input">Input vector representing player movement.</param>
        public void Move(Vector2 input);

        /// <summary>
        /// Selects the dial for interaction, enabling the player to manipulate it.
        /// </summary>
        public void Select();

         

        /// <summary>
        /// Updates/Sets the dial to a new value while rotating or moving. Invokes the OnValueChanged event.
        /// </summary>
        /// <param name="value">New value for the dial.</param>
        public void SetValue(float value);

        /// <summary>
        /// Provides UI feedback on the event of the puzzle unlock.
        /// </summary>
        public void Unlock();

        /// <summary>
        /// Gets or sets a value indicating whether this dial is currently selected.
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Stores the current value displayed by the dial. It may be different from the
        /// display value in the case of symbolic puzzles.
        /// </summary>
        public float CurrentValue { get; }

        /// <summary>
        /// Event triggered when the value of the dial changes, indicating player interaction or manipulation.
        /// </summary>
        public event Action<float> OnValueChanged;
    }
}