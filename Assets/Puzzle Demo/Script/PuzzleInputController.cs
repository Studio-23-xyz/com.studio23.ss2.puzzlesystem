using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Studio23.SS2.PuzzleDemo
{
    public class PuzzleInputController : MonoBehaviour
    {
        public Action<Vector2> Moved;
        public Action<bool> IsEnter;
        public Action<bool> IsExit;
        
        private void Start()
        {
           Debug.Log("Started!");
        }
        public void OnMove(InputAction.CallbackContext context)
        {
              Moved?.Invoke(context.ReadValue<Vector2>().normalized);
        }

        public void OnEnter(InputAction.CallbackContext context)
        {
            IsEnter?.Invoke(context.ReadValue<bool>());
        }

        public void OnExit(InputAction.CallbackContext context)
        {
            IsExit?.Invoke(context.ReadValue<bool>());
        }


    }
}
