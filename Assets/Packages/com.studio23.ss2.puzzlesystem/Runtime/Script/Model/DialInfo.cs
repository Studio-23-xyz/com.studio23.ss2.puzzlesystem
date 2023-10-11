using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio23.SS2.PuzzleSystem
{
    /// <summary>
    /// Represents information about a dial, including its unique identifier, current value, minimum value, and maximum value.
    /// </summary>
    public class DialInfo 
    {
        /// <summary>
        /// Initializes a new instance of the DialInfo class with the specified properties.
        /// </summary>
        /// <param name="indexID">The unique identifier of the dial. Unchangeable after setup.</param>
        /// <param name="currentValue">The current value of the dial.</param>
        /// <param name="minValue">The minimum value the dial can have.</param>
        /// <param name="maxValue">The maximum value the dial can have.</param>
        public DialInfo(int indexID, int currentValue, int minValue, int maxValue)
        {
            IndexID = indexID;
            CurrentValue = currentValue;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the dial. Unchangeable after set it up.
        /// </summary>
        public int IndexID { get; set; }

        /// <summary>
        /// Gets or sets the current value of the dial.
        /// </summary>
        public int CurrentValue { get; set; }

        /// <summary>
        /// Gets or sets the minimum value the dial can have.
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value the dial can have.
        /// </summary>
        public int MaxValue { get; set; }
        
        /// <summary>
        /// Event triggered when the value of the dial changes, indicating player interaction or manipulation.
        /// </summary>
        public event Action<DialInfo> OnValueChanged;
       
        /// <summary>
        /// Adjusts the value of the dial by the specified amount. This method fire OnValueChanged event.
        /// </summary>
        /// <param name="value"></param>
        public void AdjustValue(int value)
        {
          int newValue =  CurrentValue + value;
          if(newValue > MaxValue)
          {
              CurrentValue = MinValue;
          }
          else if(newValue < MinValue)
          {
              CurrentValue = MaxValue;
          }
          else
          {
              CurrentValue = newValue;
          }
          OnValueChanged?.Invoke(this);
        }
    }
}