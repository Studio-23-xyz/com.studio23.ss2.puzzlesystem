using System;
using System.Collections;
using System.Collections.Generic;
using Studio23.SS2.PuzzleSystem.Interface;
using UnityEngine;

namespace Studio23.SS2.PuzzleSystem
{
    public class PuzzleController : MonoBehaviour, IPuzzle
    {
        public IDial SelectedDial { get; set; }
        public IDial[] Dials { get; }
        public PuzzleInfo PuzzleInfo { get; }
        public event Action OnPuzzleUnlocked;
        public event Action OnPuzzleReset;
        
       public void SetupPuzzle()
        {
            throw new NotImplementedException();
        }

        public void ResetPuzzle()
        {
            throw new NotImplementedException();
        }

        public void StartPuzzle()
        {
            throw new NotImplementedException();
        }

        public bool CheckResult()
        {
            throw new NotImplementedException();
        }

        public void ShowHint()
        {
            throw new NotImplementedException();
        }

        public void ExitPuzzle()
        {
            throw new NotImplementedException();
        }

        public void SelectDial(int dialIndex)
        {
            throw new NotImplementedException();
        }

        public void UnlockPuzzle()
        {
            throw new NotImplementedException();
        }

       
    }

}