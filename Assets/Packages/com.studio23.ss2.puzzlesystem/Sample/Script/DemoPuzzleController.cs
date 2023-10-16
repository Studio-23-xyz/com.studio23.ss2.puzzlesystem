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

namespace Studio23.SS2.PuzzleDemo
{
    public class DemoPuzzleController : MonoBehaviour
    {
        // Serialized Fields
        [Header("Serialized Fields")]
        [SerializeField] private GameObject demoPuzzleGameObject;
        [SerializeField] private Transform dialsContainer;
        [SerializeField] private GameObject dialPrefab;
        [SerializeField] private Button openPuzzleButton;
        [SerializeField] private Button startPuzzleButton;
        [SerializeField] private Button exitPuzzleButton;
        [SerializeField] private GameObject puzzleNotification;
        [SerializeField] private PuzzleInputController puzzleInputController;

        [Header("Puzzle Info")]
        [SerializeField] private string puzzleName;
        [SerializeField] private int minValue;
        [SerializeField] private int maxValue;
        [SerializeField] private List<int> resultValues;
        [SerializeField] private List<int> currentValues;
        [SerializeField] private Color unselectedColor;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color solvedColor;

        private CombinationPuzzle combinationPuzzle;
        private bool isPuzzleStarted;

        // Called when the script starts
        private void Start()
        {
            puzzleInputController.OnOpenAction += Open;
        }

        // Opens the puzzle if requested and not already started
        private void Open(bool obj)
        {
            if (obj && !isPuzzleStarted)
            {
                isPuzzleStarted = true;
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
            var puzzleInfo = new PuzzleInfo(puzzleName, minValue, maxValue, resultValues, currentValues, false, new List<PuzzleHints>());
            combinationPuzzle = new CombinationPuzzle(puzzleInfo);

            combinationPuzzle.OnSelectedDialChanged += OnSelectedDialChanged;
            combinationPuzzle.OnDialValueChanged += OnDialValueChanged;
            combinationPuzzle.OnPuzzleUnlocked += OnPuzzleUnlocked;
            combinationPuzzle.OnPuzzleStart += OnPuzzleStart;
            combinationPuzzle.OnPuzzleStop += OnPuzzleStop;
        }

        // Sets the visibility of puzzle visuals
        private void SetupPuzzleVisual(bool status)
        {
            demoPuzzleGameObject.SetActive(status);
            openPuzzleButton.gameObject.SetActive(!status);
            startPuzzleButton.gameObject.SetActive(status);
            exitPuzzleButton.gameObject.SetActive(!status);
        }

        // Subscribes to input system events
        private void SubscribeInputSystem()
        {
            puzzleInputController.OnMoveAction += Move;
            puzzleInputController.OnStartAction += StartPuzzle;
            puzzleInputController.OnExitAction += StopPuzzle;
        }

        // Starts the puzzle if requested
        private void StartPuzzle(bool obj)
        {
            if (obj) combinationPuzzle.StartPuzzle();
        }

        // Moves the dials based on input
        private void Move(Vector2 obj)
        {
            if (obj.sqrMagnitude >= 1) combinationPuzzle.Move(obj);
        }

        // Stops the puzzle if requested
        private void StopPuzzle(bool obj)
        {
            if (obj) combinationPuzzle.StopPuzzle();
        }

        // Called when the puzzle starts
        private void OnPuzzleStart()
        {
            for (int i = 0; i < currentValues.Count; i++)
            {
                var dial = Instantiate(dialPrefab, dialsContainer);
                dial.GetComponentInChildren<TextMeshProUGUI>().text = currentValues[i].ToString();
                dial.GetComponent<Image>().color = unselectedColor;
            }
            exitPuzzleButton.gameObject.SetActive(true);
            startPuzzleButton.gameObject.SetActive(false);
            puzzleNotification.GetComponentInChildren<TextMeshProUGUI>().text = "Puzzle Started!";
        }

        // Called when the puzzle stops
        private void OnPuzzleStop()
        {
            foreach (Transform item in dialsContainer.transform)
            {
                Destroy(item.gameObject);
            }

            puzzleNotification.GetComponentInChildren<TextMeshProUGUI>().text = "Puzzle Stop!";
            SetupPuzzleVisual(false);
            UnsubscribeEvents();
            isPuzzleStarted = false;
        }

        // Unsubscribes from events
        private void UnsubscribeEvents()
        {
            combinationPuzzle.OnSelectedDialChanged -= OnSelectedDialChanged;
            combinationPuzzle.OnDialValueChanged -= OnDialValueChanged;
            combinationPuzzle.OnPuzzleUnlocked -= OnPuzzleUnlocked;
            combinationPuzzle.OnPuzzleStart -= OnPuzzleStart;
            combinationPuzzle.OnPuzzleStop -= OnPuzzleStop;

            puzzleInputController.OnMoveAction -= Move;
            puzzleInputController.OnStartAction -= StartPuzzle;
            puzzleInputController.OnExitAction -= StopPuzzle;
        }

        // Called when the selected dial changes
        private void OnSelectedDialChanged(int obj)
        {
            for (int i = 0; i < dialsContainer.childCount; i++)
            {
                dialsContainer.GetChild(i).GetComponent<Image>().color = i == obj ? selectedColor : unselectedColor;
            }
        }

        // Called when the value of a dial changes
        private void OnDialValueChanged(DialInfo obj)
        {
            dialsContainer.GetChild(obj.IndexID).GetComponentInChildren<TextMeshProUGUI>().text = obj.CurrentValue.ToString();
        }

        // Called when the puzzle is successfully unlocked
        private void OnPuzzleUnlocked()
        {
            puzzleNotification.GetComponentInChildren<TextMeshProUGUI>().text = "Puzzle Unlocked!";
            puzzleNotification.SetActive(true);
            for (int i = 0; i < dialsContainer.childCount; i++)
            {
                dialsContainer.GetChild(i).GetComponent<Image>().color = solvedColor;
            }
        }
    }
}
