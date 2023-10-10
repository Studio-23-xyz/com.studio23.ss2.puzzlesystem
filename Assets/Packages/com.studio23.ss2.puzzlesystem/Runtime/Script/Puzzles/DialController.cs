using System;
using System.Collections;
using System.Collections.Generic;
using Studio23.SS2.PuzzleSystem.Interface;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class DialController : MonoBehaviour, IDial
{
    public bool IsSelected { get; set; }
    public float CurrentValue { get; set; }
    public event Action<float> OnValueChanged;


    public void Initialize()
    {
        // todo: Initialize Dial Info
    }

    public void Rotated(Vector2 input)
    {
        // todo: Rotate Dial as per input command
        // todo: call SetValue
          
    }

    public void Move(Vector2 input)
    {
        // todo: Move Dial as per input command
       
    }

    public void Select()
    {
        // todo: Select Dial as per input command
        // todo: toggle IsSelected
    }

  

    public void SetValue(float value)
    {
        // todo: call SetValue
        // todo: Invoke OnValueChanged
    }

    public void Unlock()
    {
        // todo: Unlock Dial will show UI feedback when puzzle is unlocked
    }

   
}
