using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using NUnit.Framework;
using Studio23.SS2.PuzzleSystem.Core;
using Studio23.SS2.PuzzleSystem.Data;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;
 

namespace Tests.EditMode
{
    public class PuzzleSystemTests
    {
        [Test]
        public void DialInfo_AdjustValue_WithinRange_ShouldChangeValue()
        {
            DialInfo dialInfo = new DialInfo(0, 5, 0, 10);
            dialInfo.AdjustValue(2);
            Assert.AreEqual(7, dialInfo.CurrentValue);
        }

        [Test]
        public void DialInfo_AdjustValue_OutsideMaxRange_ShouldWrapAroundToMinValue()
        {
            DialInfo dialInfo = new DialInfo(0, 8, 0, 5);
            Assert.AreEqual(5, dialInfo.CurrentValue);
        }

        [Test]
        public void DialInfo_AdjustValue_OutsideMinRange_ShouldWrapAroundToMaxValue()
        {
            DialInfo dialInfo = new DialInfo(0, 2, 1, 4);
            dialInfo.AdjustValue(-3);
            Assert.AreEqual(4, dialInfo.CurrentValue);
        }

        [Test]
        public void PuzzleInfo_CheckPuzzleStatus_CorrectSolution_ShouldReturnTrue()
        {
            PuzzleInfo puzzleInfo = new PuzzleInfo("TestPuzzle", 1, 10, new System.Collections.Generic.List<int> { 1, 2, 3 }, new System.Collections.Generic.List<int> { 0,0,0 },  null);
             puzzleInfo.SetCurrentValues(0,1);
             puzzleInfo.SetCurrentValues(1,2);
             puzzleInfo.SetCurrentValues(2,3);
            Assert.IsTrue(puzzleInfo.IsPuzzleSolved);
        }

        [Test]
        public void PuzzleInfo_CheckPuzzleStatus_IncorrectSolution_ShouldReturnFalse()
        {
            PuzzleInfo puzzleInfo = new PuzzleInfo("TestPuzzle", 1, 10, new System.Collections.Generic.List<int> { 1, 2, 3 }, new System.Collections.Generic.List<int> { 1, 4, 3 },  null);
           
            Assert.IsFalse(puzzleInfo.IsPuzzleSolved);
        }

        [Test]
        public void CombinationPuzzle_StartPuzzle_ShouldSetIsPuzzleStartedToTrue()
        {
            PuzzleInfo puzzleInfo = new PuzzleInfo("TestPuzzle", 0, 9, new System.Collections.Generic.List<int> { 1, 2, 3 }, new System.Collections.Generic.List<int> { 0,0,0 },  null);

           // CombinationPuzzle combinationPuzzle = new CombinationPuzzle(puzzleInfo);
            
            Assert.IsTrue(puzzleInfo.Validate());
        }

        
        /*[Test]
        public void CombinationPuzzle_StopPuzzle_ShouldSetIsPuzzleStartedToFalse()
        {
            PuzzleInfo puzzleInfo = new PuzzleInfo("TestPuzzle", 1, 10, new System.Collections.Generic.List<int> { 1, 2, 3 }, new System.Collections.Generic.List<int> { 1, 2, 3 },  null);
            CombinationPuzzle puzzle = new CombinationPuzzle(puzzleInfo);
            puzzle.StartPuzzle();
            puzzle.StopPuzzle();
            Assert.IsFalse(puzzle.IsPuzzleStarted);
        }

        [Test]
        public void CombinationPuzzle_SetCurrentValues_ShouldUpdateCurrentValues()
        {
            PuzzleInfo puzzleInfo = new PuzzleInfo("TestPuzzle", 1, 10, new System.Collections.Generic.List<int> { 1, 2, 3 }, new System.Collections.Generic.List<int> { 1, 2, 3 },  null);
            CombinationPuzzle puzzle = new CombinationPuzzle(puzzleInfo);
            puzzle.StartPuzzle();
            puzzleInfo.SetCurrentValues(new System.Collections.Generic.List<int> { 4, 5, 6 });
            Assert.AreEqual(4, puzzle.PuzzleInfo.CurrentValues[0]);
            Assert.AreEqual(5, puzzle.PuzzleInfo.CurrentValues[1]);
            Assert.AreEqual(6, puzzle.PuzzleInfo.CurrentValues[2]);
        }*/
        
        
    }
}
