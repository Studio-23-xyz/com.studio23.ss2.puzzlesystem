using Studio23.SS2.PuzzleSystem.Data;
using Studio23.SS2.PuzzleSystem.Interface;

namespace Studio23.SS2.PuzzleSystem.Core
{
    public class DialController : IDial
    {
        public BaseDialInfo DialIndexInfo { get; set; }
        
        public DialController(BaseDialInfo dialIndexInfo)
        {
            // Initialize Dial Info
            DialIndexInfo = dialIndexInfo;
        }
        
        public void AdjustValue(int adjustAmount)
        {
            //  Call AdjustValue
            DialIndexInfo.AdjustValue(adjustAmount);
        }

        /// <inheritdoc />
        public void SetValue(int value)
        {
            //  Invoke OnValueChanged
            DialIndexInfo.SetValue(DialIndexInfo.AdjustValue(value));
        }
    }
}