using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Studio23.SS2.PuzzleDemo
{
    public class PuzzleInputController : MonoBehaviour
    {
        
        public bool IsOpen;
        public Vector2 Move;
        public bool IsStart;
        public bool IsExit;
        
        public event Action<bool> OnOpenAction;
        public event Action<Vector2> OnMoveAction;
        public event Action<bool> OnStartAction;
        public event Action<bool> OnExitAction;
      

        private void Start()
        {
            Debug.Log("Input controller started!");
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                Move = context.ReadValue<Vector2>().normalized;
                OnMoveAction?.Invoke(Move);
            //    Debug.Log( "Move InputActionPhase.Performed : " + Move);
            }else if (context.phase == InputActionPhase.Canceled)
            {
                Move = Vector2.zero;
            //    Debug.Log( "Move InputActionPhase.Canceled: " + Move);
            }
        }

        public void OnStart(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                IsStart = context.ReadValueAsButton();
                OnStartAction?.Invoke(IsStart);
                Debug.Log( "Start: " + IsStart);
            }else if (context.phase == InputActionPhase.Canceled)
            {
                IsStart = false;
            }
            
        }

        public void OnExit(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                IsExit = context.ReadValueAsButton();
                OnExitAction?.Invoke(IsExit);
                Debug.Log( "Exit: " + IsExit);
            }else if (context.phase == InputActionPhase.Canceled)
            {
                IsExit = false;
            }
            
        }
        
        
        public void OnOpen(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                IsOpen = context.ReadValueAsButton();
                OnOpenAction?.Invoke(IsOpen);
                Debug.Log( "IsOpen: " + IsOpen);
            }else if (context.phase == InputActionPhase.Canceled)
            {
                IsOpen = false;
            }
            
        }
    }
}