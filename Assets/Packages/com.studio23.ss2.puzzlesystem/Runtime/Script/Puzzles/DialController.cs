using Studio23.SS2.PuzzleSystem.Data;
using Studio23.SS2.PuzzleSystem.Interface;

namespace Studio23.SS2.PuzzleSystem.Core
{
    public class DialController : IDial
    {
        public DialInfo DialInfo { get; set; }
        
        public DialController(DialInfo dialInfo)
        {
            // Initialize Dial Info
            DialInfo = dialInfo;
        }
        
        public void AdjustValue(int adjustAmount)
        {
            //  Call AdjustValue
            //  Invoke OnValueChanged
            DialInfo.AdjustValue(adjustAmount);
        }

        /// <inheritdoc />
        public void SetValue(int value)
        {
            DialInfo.SetValue(value);
        }
    }
}