using System;
using System.Collections;
using System.Collections.Generic;
using Studio23.SS2.PuzzleSystem.Interface;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Studio23.SS2.PuzzleSystem
{
    public class DialController : IDial
    {
        public DialInfo DialInfo { get; set; }
        public event Action<DialInfo> OnValueChanged;
        
        public void Initialize(DialInfo dialInfo)
        {
            // todo: Initialize Dial Info
            DialInfo = dialInfo;
        }

        public void Rotated(Vector2 input)
        {
            // todo: Rotate Dial as per input command
            // todo: call AdjustValue
        }

        public void Move(Vector2 input)
        {
            // todo: Move Dial as per input command
        }
        public void AdjustValue(int value)
        {
            // todo: call AdjustValue
            DialInfo.CurrentValue += value;
            OnValueChanged?.Invoke(DialInfo);
            // todo: Invoke OnValueChanged
        }

        public void Unlock()
        {
            // todo: Unlock Dial will show UI feedback when puzzle is unlocked
        }
    }
}