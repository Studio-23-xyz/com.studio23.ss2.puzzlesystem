
using System;


namespace Studio23.SS2.PuzzleSystem.Data
{
    /// <summary>
    ///     Represents information about a dial, including its unique identifier, current value, minimum value, and maximum
    ///     value.
    /// </summary>
    public class BaseDialInfo
    {
        /// <summary>
        ///     Gets or sets the unique identifier of the dial. Unchangeable after set it up.
        /// </summary>
        public int IndexID { get; set; }

        /// <summary>
        ///     Gets or sets the current value of the dial.
        /// </summary>
        public int CurrentValue { get; private set; }



        /// <summary>
        ///     Gets or sets the minimum value the dial can have.
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        ///     Gets or sets the maximum value the dial can have.
        /// </summary>
        public int MaxValue { get; set; }

        /// <summary>
        ///     Event triggered when the value of the dial changes, indicating player interaction or manipulation.
        /// </summary>
        public event Action<BaseDialInfo> OnValueChanged;

        /// <summary>
        ///     Adjusts the value of the dial by the specified amount. This method fires the OnValueChanged event.
        /// </summary>
        /// <param name="value">The amount by which the dial value will be adjusted.</param>
        public virtual int AdjustValue(int value)
        {
            return 0;
        }

        /// <summary>
        ///     Adjusts the value of the dial by the specified amount. This method fires the OnValueChanged event.
        /// </summary>
        /// <param name="value">The amount by which the dial value will be adjusted.</param>
        public virtual void SetValue(int value)
        {
            OnValueChanged?.Invoke(this);
        }

        protected void SetCurrentValue(int currentVal)
        {
            CurrentValue = currentVal;
        }
    }
}

