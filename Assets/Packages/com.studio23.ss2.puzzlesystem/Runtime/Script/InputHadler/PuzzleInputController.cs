using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Studio23.SS2.PuzzleSystem
{
    public class PuzzleInputController : MonoBehaviour
    {
        public Vector2 Move;
        public float MoveM;
        public bool IsEnter;
        public bool IsExit;

        private void Start()
        {
           Debug.Log("Started!");
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            
            Move = context.ReadValue<Vector2>().normalized; // vector 2 while press
            MoveM = Move.sqrMagnitude;
           
        }

        public void OnEnter(InputAction.CallbackContext context)
        {
            IsEnter = context.ReadValue<bool>();
        }

        public void OnExit(InputAction.CallbackContext context)
        {
            IsExit = context.ReadValue<bool>();
        }


    }
}
