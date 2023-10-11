using System.Collections.Generic;
using Studio23.SS2.PuzzleSystem;
using Studio23.SS2.PuzzleSystem.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Studio23.SS2.PuzzleDemo
{
    public class DemoPuzzleController : MonoBehaviour
    {
        [Header("Serialized Fields")]
        [SerializeField] private GameObject demoPuzzleGameObject;
        [SerializeField] private Transform dialsContainer;
        [SerializeField] private GameObject dialPrefab;
        [SerializeField] private Button startPuzzleButton;
        [SerializeField] private Button exitPuzzleButton;
        [SerializeField] private GameObject unlockGameObject;
        [SerializeField] private PuzzleInputController puzzleInputController;
            
        [Header("Puzzle Info")]
        CombinationPuzzle combinationPuzzle;
        [SerializeField] private string puzzleName;
        [SerializeField] private List<int> resultValue; //= new List<int>(capacity) {1, 2, 3, 4};
        [SerializeField] private List<int> currentValue; //= new List<int>(capacity) {0, 0, 0, 0};
        
        [Header("Color Info")]
        [SerializeField] private Color unselectedColor;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color solvedColor;
        private void Start()
        {
            SetupDemoPuzzle();
            SetupPuzzleInput();
            SetupPuzzleVisual();

        }

        private void SetupDemoPuzzle()
        {
            var puzzleInfo = new PuzzleInfo(
                puzzleName, 
                resultValue, 
                currentValue,
                false, 
                new List<PuzzleHints>(), 
                0);
            
            
            combinationPuzzle = new CombinationPuzzle(puzzleInfo);
           
            combinationPuzzle.OnSelectedDialChanged += OnSelectedDialChanged;
            combinationPuzzle.OnDialValueChanged += OnDialValueChanged;
            combinationPuzzle.PuzzleInfo.OnPuzzleUnlocked += OnPuzzleUnlocked;
           
            combinationPuzzle.OnPuzzleStart += OnPuzzleStart;
            combinationPuzzle.OnPuzzleStop += OnPuzzleStop;
        }
        private void SetupPuzzleVisual()
        {
            
            demoPuzzleGameObject.SetActive(true);
        }
        private void SetupPuzzleInput()
        {
            puzzleInputController.Moved += Move;
            puzzleInputController.IsEnter += StartPuzzle;
            puzzleInputController.IsExit += StopPuzzle;
        }

        private void Move(Vector2 obj)
        {
            if(obj.sqrMagnitude >= 1)combinationPuzzle.Move(obj);
        }

        private void StartPuzzle(bool obj)
        {
            if(obj)combinationPuzzle.StartPuzzle();
        }

        private void StopPuzzle(bool obj)
        {
            if(obj)combinationPuzzle.StopPuzzle();
        }

        private void OnPuzzleStart()
        {
            for (int i = 0; i < currentValue.Count; i++)
            {
                var dial = Instantiate(dialPrefab, dialsContainer);
                dial.GetComponentInChildren<TextMeshProUGUI>().text = currentValue[i].ToString();
                dial.GetComponent<Image>().color = unselectedColor;
            }
            exitPuzzleButton.gameObject.SetActive(true);
        }
        private void OnPuzzleStop()
        {
            throw new System.NotImplementedException();
        }
        private void OnSelectedDialChanged(int obj)
        {
            for (int i = 0; i < dialsContainer.childCount; i++)
            {
                dialsContainer.GetChild(i).GetComponent<Image>().color = unselectedColor;
            }
            dialsContainer.GetChild(obj).GetComponent<Image>().color = selectedColor;
        }
        private void OnDialValueChanged(DialInfo obj)
        {
            dialsContainer.GetChild(obj.IndexID).GetComponentInChildren<TextMeshProUGUI>().text = obj.CurrentValue.ToString();
        }

        private void OnPuzzleUnlocked()
        {
            unlockGameObject.SetActive(true);
            for (int i = 0; i < dialsContainer.childCount; i++)
            {
                dialsContainer.GetChild(i).GetComponent<Image>().color = solvedColor;
            }
        }

        

        
    }
}
