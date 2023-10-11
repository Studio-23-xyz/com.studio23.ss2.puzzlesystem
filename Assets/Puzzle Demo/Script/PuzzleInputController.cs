using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Studio23.SS2.PuzzleDemo
{
    public class PuzzleInputController : MonoBehaviour
    {
        public Vector2 Move;
        public bool IsEnter;
        public bool IsExit;

        public event Action<Vector2> OnMoveAction;
        public event Action<bool> OnEnterAction;
        public event Action<bool> OnExitAction;

        private void Start()
        {
            Debug.Log("Input controller started!");
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>().normalized;
            OnMoveAction?.Invoke(Move);
        }

        public void OnEnter(InputAction.CallbackContext context)
        {
            IsEnter = context.ReadValueAsButton();
            OnEnterAction?.Invoke(IsEnter);
            Debug.Log( "Enter: " + IsEnter);
        }

        public void OnExit(InputAction.CallbackContext context)
        {
            IsExit = context.ReadValueAsButton();
            OnExitAction?.Invoke(IsExit);
            Debug.Log( "Exit: " + IsExit);
        }
    }
}