using System;
using System.Collections;
using System.Collections.Generic;
using Studio23.SS2.PuzzleSystem.Interface;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class DialController : MonoBehaviour, IDial
{
    public bool IsSelected { get; set; }
    public float CurrentValue { get; }
    public event Action<float> OnValueChanged;
    
    public void Rotated(Vector2 input)
    {
        throw new NotImplementedException();
    }

    public void Move(Vector2 input)
    {
        throw new NotImplementedException();
    }

    public void Select()
    {
        throw new NotImplementedException();
    }

    public void Deselect()
    {
        throw new NotImplementedException();
    }

    public void SetValue(float value)
    {
        throw new NotImplementedException();
    }

    public void Unlock()
    {
        throw new NotImplementedException();
    }

   
}
