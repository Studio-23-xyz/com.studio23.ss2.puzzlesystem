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
            // todo: Initialize Dial Info
            DialInfo = dialInfo;
        }
        
        public void AdjustValue(int value)
        {
            // todo: call AdjustValue
            // todo: Invoke OnValueChanged
            DialInfo.AdjustValue(value);
            
           
        }
    }
}