using System;
using UnityEngine;

namespace Studio23.SS2.PuzzleSystem.Data
{
    /// <summary>
    ///     Represents information about a dial, including its unique identifier, current value, minimum value, and maximum
    ///     value.
    /// </summary>
    public class DialIndexInfo :BaseDialInfo
    {
        /// <summary>
        ///     Initializes a new instance of the DialIndexInfo class with the specified properties.
        /// </summary>
        /// <param name="indexID">The unique identifier of the dial. Unchangeable after setup.</param>
        /// <param name="currentValue">The current value of the dial.</param>
        /// <param name="minValue">The minimum value the dial can have.</param>
        /// <param name="maxValue">The maximum value the dial can have.</param>
        public DialIndexInfo(int indexID, int currentValue, int minValue, int maxValue)
        {
            IndexID = indexID;
            MinValue = minValue;
            MaxValue = maxValue;
            var value = Mathf.Clamp(currentValue, minValue, maxValue);
            SetCurrentValue(value);
        }

        /// <summary>
        ///     Adjusts the value of the dial by the specified amount. This method fires the OnValueChanged event.
        /// </summary>
        /// <param name="value">The amount by which the dial value will be adjusted.</param>
        public override int AdjustValue(int value)
        {
            var newValue = CurrentValue + value;
            if (newValue > MaxValue)
                newValue -= (MaxValue + 1);
            else if(newValue < MinValue)
                newValue += (MaxValue + 1);

            SetCurrentValue(Mathf.Clamp(newValue, MinValue, MaxValue));
            return CurrentValue;
        }
        
        /// <summary>
        ///     Adjusts the value of the dial by the specified amount. This method fires the OnValueChanged event.
        /// </summary>
        /// <param name="value">The amount by which the dial value will be adjusted.</param>
        public override void SetValue(int value)
        {
            SetCurrentValue(Mathf.Clamp(value, MinValue, MaxValue)) ;
            base.SetValue(value);
        }
    }
}