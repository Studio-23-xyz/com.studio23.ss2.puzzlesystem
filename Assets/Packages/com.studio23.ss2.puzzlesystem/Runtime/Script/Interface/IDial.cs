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
        /// <param name="indexId"> current index position into the dials combination. unchangeable</param>
        /// <param name="currentValue"> current value. while rotated this value will be updated </param>
        public void Initialize(DialInfo dialInfo);
        
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
        /// Updates/Sets the dial to a new value while rotating or moving. Invokes the OnValueChanged event.
        /// </summary>
        /// <param name="value">New value for the dial.</param>
        public void AdjustValue(int value);

        /// <summary>
        /// Provides UI feedback on the event of the puzzle unlock.
        /// </summary>
        public void Unlock();

     
        
        /// <summary>
        /// Dial basic info.
        /// </summary>
        /// <param name="indexId"> current index position into the dials combination. unchangeable</param>
        /// <param name="currentValue"> current value. while rotated this value will be updated </param>
        /// <param name="MinValue">minimum value can set as current value</param>
        /// <param name="MaxValue">maximum value can set as current value</param>
      
        public DialInfo DialInfo { get; set; }
        
        

       

        /// <summary>
        /// Event triggered when the value of the dial changes, indicating player interaction or manipulation.
        /// </summary>
        public event Action<DialInfo> OnValueChanged;
    }
}