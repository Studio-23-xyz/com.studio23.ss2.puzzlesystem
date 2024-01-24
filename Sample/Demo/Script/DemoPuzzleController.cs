/*
 * DemoPuzzleController Class:
 * This class manages the behavior and interaction of a demo puzzle in the game.
 */

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Studio23.SS2.PuzzleSystem.Core;
using Studio23.SS2.PuzzleSystem.Data;
using UnityEngine.Serialization;

namespace Studio23.SS2.Sample
{
    public class DemoPuzzleController : MonoBehaviour
    {
        // Serialized Fields

        [Header("Serialized Fields")] 
        [SerializeField] private GameObject _demoPuzzleGameObject;
        [SerializeField] private Transform _dialsContainer;
        [SerializeField] private GameObject _dialPrefab;
        [SerializeField] private Button _openPuzzleButton;
        [SerializeField] private Button _startPuzzleButton;
        [SerializeField] private Button _exitPuzzleButton;
        [SerializeField] private GameObject _puzzleNotification;
        [SerializeField] private PuzzleInputController _puzzleInputController;


        [Header("Puzzle Info")] 
        [SerializeField] private string _puzzleName;
        [SerializeField] private int _minValue;
        [SerializeField] private int _maxValue;
        [SerializeField] private List<int> _resultValues;
        [SerializeField] private List<int> _currentValues;
        [SerializeField] private Color _unselectedColor;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _solvedColor;

        private CombinationPuzzle _combinationPuzzle;
        private bool _isPuzzleStarted;
        
        private void Start()
        {
            _puzzleInputController.OnOpenAction += Open;
        }

        // Opens the puzzle if requested and not already started
        private void Open(bool obj)
        {
            if (obj && !_isPuzzleStarted)
            {
                _isPuzzleStarted = true;
                InitPuzzle();
            }
        }

        // Initializes the puzzle
        private void InitPuzzle()
        {
            SetupDemoPuzzle();
            SubscribeInputSystem();
            SetupPuzzleVisual(true);
        }

        // Sets up the demo puzzle with provided parameters
        private void SetupDemoPuzzle()
        {
            var puzzleInfo = new PuzzleInfo(_puzzleName, _minValue, _maxValue, _resultValues, _currentValues,
                new List<PuzzleHints>());
            _combinationPuzzle = new CombinationPuzzle(puzzleInfo);

            _combinationPuzzle.OnSelectedDialChanged += OnSelectedDialChanged;
            _combinationPuzzle.OnDialValueChanged += OnDialValueChanged;
            _combinationPuzzle.OnPuzzleUnlocked += OnPuzzleUnlocked;
            _combinationPuzzle.OnPuzzleStart += OnPuzzleStart;
            _combinationPuzzle.OnPuzzleStop += OnPuzzleStop;
        }

        // Sets the visibility of puzzle visuals
        private void SetupPuzzleVisual(bool status)
        {
            _demoPuzzleGameObject.SetActive(status);
            _openPuzzleButton.gameObject.SetActive(!status);
            _startPuzzleButton.gameObject.SetActive(status);
            _exitPuzzleButton.gameObject.SetActive(!status);
        }

        // Subscribes to input system events
        private void SubscribeInputSystem()
        {
            _puzzleInputController.OnMoveAction += Move;
            _puzzleInputController.OnStartAction += StartPuzzle;
            _puzzleInputController.OnExitAction += StopPuzzle;
        }

        // Starts the puzzle if requested
        private void StartPuzzle(bool isStarted)
        {
            if (isStarted) _combinationPuzzle.StartPuzzle();
        }

        // Moves the dials based on input
        private void Move(Vector2 input)
        {
            if (input.sqrMagnitude >= 1) _combinationPuzzle.AdjustDial(input.GetDirection());
        }

        // Stops the puzzle if requested
        private void StopPuzzle(bool isStopped)
        {
            if (isStopped) _combinationPuzzle.StopPuzzle();
        }

        // Called when the puzzle starts
        private void OnPuzzleStart()
        {
            for (int i = 0; i < _currentValues.Count; i++)
            {
                var dial = Instantiate(_dialPrefab, _dialsContainer);
                dial.GetComponentInChildren<TextMeshProUGUI>().text = _currentValues[i].ToString();
                dial.GetComponent<Image>().color = _unselectedColor;
            }

            _exitPuzzleButton.gameObject.SetActive(true);
            _startPuzzleButton.gameObject.SetActive(false);
            _puzzleNotification.GetComponentInChildren<TextMeshProUGUI>().text = "Puzzle Started!";
        }

        // Called when the puzzle stops
        private void OnPuzzleStop()
        {
            foreach (Transform item in _dialsContainer.transform)
            {
                Destroy(item.gameObject);
            }

            _puzzleNotification.GetComponentInChildren<TextMeshProUGUI>().text = "Puzzle Stop!";
            SetupPuzzleVisual(false);
            UnsubscribeEvents();
            _isPuzzleStarted = false;
        }

        // Called when the selected dial changes
        private void OnSelectedDialChanged(int obj)
        {
            for (int i = 0; i < _dialsContainer.childCount; i++)
            {
                _dialsContainer.GetChild(i).GetComponent<Image>().color = i == obj ? _selectedColor : _unselectedColor;
            }
        }

        // Called when the value of a dial changes
        private void OnDialValueChanged(DialInfo obj)
        {
            _dialsContainer.GetChild(obj.IndexID).GetComponentInChildren<TextMeshProUGUI>().text =
                obj.CurrentValue.ToString();
        }

        // Called when the puzzle is successfully unlocked
        private void OnPuzzleUnlocked()
        {
            _puzzleNotification.GetComponentInChildren<TextMeshProUGUI>().text = "Puzzle Unlocked!";
            _puzzleNotification.SetActive(true);
            for (int i = 0; i < _dialsContainer.childCount; i++)
            {
                _dialsContainer.GetChild(i).GetComponent<Image>().color = _solvedColor;
            }
        }

        // Unsubscribes from events
        private void UnsubscribeEvents()
        {
            _combinationPuzzle.OnSelectedDialChanged -= OnSelectedDialChanged;
            _combinationPuzzle.OnDialValueChanged -= OnDialValueChanged;
            _combinationPuzzle.OnPuzzleUnlocked -= OnPuzzleUnlocked;
            _combinationPuzzle.OnPuzzleStart -= OnPuzzleStart;
            _combinationPuzzle.OnPuzzleStop -= OnPuzzleStop;

            _puzzleInputController.OnMoveAction -= Move;
            _puzzleInputController.OnStartAction -= StartPuzzle;
            _puzzleInputController.OnExitAction -= StopPuzzle;
        }
    }
}