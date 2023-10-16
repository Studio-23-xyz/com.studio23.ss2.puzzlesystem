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
        
        public DialController(DialInfo dialInfo)
        {
            // Initialize Dial Info
            DialInfo = dialInfo;
        }
        
        public void AdjustValue(int value)
        {
            //  Call AdjustValue
            //  Invoke OnValueChanged
            DialInfo.AdjustValue(value);
            
           
        }
    }
}