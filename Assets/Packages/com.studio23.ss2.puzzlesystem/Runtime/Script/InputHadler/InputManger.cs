using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Studio23.SS2.PuzzleSystem
{
    public enum GameActionMap
    {
        PuzzleMap,
        MenuMap
    }
    public enum GameControlSchema
    {
        KeyboardMouse,
        Gamepad
    }
    public class InputManger: MonoBehaviour
    {
        
        public Vector3 RawInputMovement;
        public Vector3 RawInputLook;
        public bool  IsSprint;
        public Action<bool> ToggleCamera;
        [SerializeField] private PlayerInput playerInput;

        
        // Accessible Properties 
        public GameActionMap CurrentActionMap;
        public GameControlSchema CurrentControlSchema;
        // Accessible Properties 
        private bool _isPaused;
        private string _currentControlScheme;  // store current control schema say keyboard or gamepad
        
        private void Awake()
        {
            _currentControlScheme = playerInput.currentControlScheme;// KeyboardMouse or Gamepad
            CurrentControlSchema = (GameControlSchema)Enum.Parse(typeof(GameControlSchema), _currentControlScheme);
        }
        public void OnMovement(InputAction.CallbackContext callbackContext)
        {
            var readValue = callbackContext.ReadValue<Vector2>(); // vector 2 while press
            RawInputMovement = new Vector3(readValue.x, 0, readValue.y);
           // Debug.Log($" RawInputMovement {RawInputMovement}");
        }
        public void OnLook(InputAction.CallbackContext callbackContext)
        {
            var readValue = callbackContext.ReadValue<Vector2>(); // vector 2 Mouse pointer delta or joystick L
            RawInputLook = new Vector3(readValue.x, 0, readValue.y);
        }
        public void OnSprint(InputAction.CallbackContext callbackContext)
        {
            var readValue = callbackContext.ReadValueAsButton(); // true until pressed
            IsSprint = readValue;
            // Debug.Log($" OnSprint {readValue}");
        }
        public void OnTogglePause(InputAction.CallbackContext callbackContext)
        {
            var readValue = callbackContext.ReadValueAsButton(); // button down true, release false
            Debug.Log($" OnTogglePause {readValue}");
            if(readValue) ControlChanged();
        }

        public void OnToggleCamera(InputAction.CallbackContext callbackContext)
        {
            var readValue = callbackContext.ReadValueAsButton();
            ToggleCamera.Invoke(readValue);
        }
        public void OnControlChanged( )
        {
            if (_currentControlScheme != playerInput.currentControlScheme)
            {
                _currentControlScheme = playerInput.currentControlScheme;
                CurrentControlSchema = (GameControlSchema)Enum.Parse(typeof(GameControlSchema), _currentControlScheme);
                RemoveAllBindingOverrides();
            }
        }
        private void RemoveAllBindingOverrides()
        {
            InputActionRebindingExtensions.RemoveAllBindingOverrides(playerInput.currentActionMap);
        }
        public void OnDeviceLost()
        {
        }


        public void OnDeviceRegained()
        {
        }
     
      
        private void ControlChanged()
        {
            switch(CurrentActionMap)
            {
                case GameActionMap.PuzzleMap:

                    CurrentActionMap = GameActionMap.MenuMap;
                    playerInput.SwitchCurrentActionMap(CurrentActionMap.ToString());  
                    break;
                case GameActionMap.MenuMap:
                    CurrentActionMap = GameActionMap.PuzzleMap;
                    playerInput.SwitchCurrentActionMap(CurrentActionMap.ToString());  
                    break;
                default:
                    Debug.LogError("Action Map Error");
                    break;
            }
        }
    }
}