using NUnit.Framework;
using Studio23.SS2.PuzzleSystem.Core;
using Studio23.SS2.PuzzleSystem.Data;

using System.Collections.Generic;
using UnityEngine;


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
        public void DialInfo_AdjustValue_OutsideMaxRange_ShouldWrapAroundTMaxValue()
        {
            DialInfo dialInfo = new DialInfo(0, 8, 0, 5);
            Assert.AreEqual(5, dialInfo.CurrentValue);
        }
        [Test]
        public void DialInfo_AdjustValue_OutsideMaxRange_ShouldWrapAroundToMinValue()
        {
            DialInfo dialInfo = new DialInfo(0, -20, 0, 5);
            Assert.AreEqual(0, dialInfo.CurrentValue);
        }
        [Test]
        public void DialInfo_AdjustValue_OutsideMinRange_ShouldWrapAroundToMaxValue()
        {
            DialInfo dialInfo = new DialInfo(0, 2, 1, 4);
            dialInfo.AdjustValue(-3);
            Assert.AreEqual(4, dialInfo.CurrentValue);
        }

        [Test]
        public void DialInfo_AdjustValue_OutsideMinRange_ShouldWrapAroundToMinValue()
        {
            DialInfo dialInfo = new DialInfo(0, 2, 1, 4);
            dialInfo.AdjustValue(8);
            Assert.AreEqual(1, dialInfo.CurrentValue);
        }
        
      
        
        [Test]
        public void PuzzleInfo_CheckPuzzleStatus_CorrectSolution_ShouldReturnTrue()
        {
            PuzzleInfo puzzleInfo = new PuzzleInfo("TestPuzzle", 1, 10, new List<int> { 1, 2, 3 }, new List<int> { 0,0,0 },  null);
             puzzleInfo.SetCurrentValues(0,1);
             puzzleInfo.SetCurrentValues(1,2);
             puzzleInfo.SetCurrentValues(2,3);
             // puzzleInfo.SetCurrentValues(new List<int> { 1, 2, 3 });
            Assert.IsTrue(puzzleInfo.IsPuzzleSolved);
        }
        [Test]
        public void PuzzleInfo_CheckPuzzleStatus_IncorrectSolution_ShouldReturnFalse()
        {
            PuzzleInfo puzzleInfo = new PuzzleInfo("TestPuzzle", 1, 10, new List<int> { 1, 2, 3 }, new List<int> { 1, 4, 3 },  null);
            Assert.IsFalse(puzzleInfo.IsPuzzleSolved);
        }
        [Test]
        public void PuzzleInfo_SetCurrentValues_OutsideRange_ShouldWrapInsideRange()
        {
            PuzzleInfo puzzleInfo = new PuzzleInfo("TestPuzzle", 0, 10, 
                new List<int> { 1, 2, 3 }, 
                new List<int> { 0,0,0 },  
                null);

            /*puzzleInfo.SetCurrentValues(0,-20);
            puzzleInfo.SetCurrentValues(1,20);
            puzzleInfo.SetCurrentValues(2,5);*/
            puzzleInfo.SetCurrentValues(new List<int> { -20,20,5 });
            Assert.AreEqual(10, puzzleInfo.CurrentValues[0]);
            Assert.AreEqual(0, puzzleInfo.CurrentValues[1]);
            Assert.AreEqual(5, puzzleInfo.CurrentValues[2]);
           
        }
        
      

        [Test]
        public void CombinationPuzzle_StartPuzzle_ShouldSetIsPuzzleStartedToTrue()
        {
            PuzzleInfo puzzleInfo = new PuzzleInfo("TestPuzzle", 0, 9, new List<int> { 1, 2, 3 }, new List<int> { 1,2,1 },  null);

            if(puzzleInfo.Validate()) {
                CombinationPuzzle puzzle = new CombinationPuzzle(puzzleInfo);
                
                
                bool onPuzzleStartInvoked = false;
                puzzle.OnPuzzleStart += () => onPuzzleStartInvoked = true;
                
                puzzle.StartPuzzle();
                
                Assert.IsTrue(onPuzzleStartInvoked);
                Assert.IsTrue(puzzle.IsPuzzleStarted);
            }
        }

        


        [Test]
        public void CombinationPuzzle_StopPuzzle_ShouldSetIsPuzzleStartedToFalse()
        {
            PuzzleInfo puzzleInfo = new PuzzleInfo("TestPuzzle", 0, 9, new List<int> { 1, 2, 3 }, new List<int> { 1,2,1 },  null);
            if (puzzleInfo.Validate())
            {
                CombinationPuzzle puzzle = new CombinationPuzzle(puzzleInfo);
                
                bool onPuzzleStartInvoked = false;
                puzzle.OnPuzzleStart += () => onPuzzleStartInvoked = true;
                
                bool onPuzzleStopInvoked = false;
                puzzle.OnPuzzleStop += () => onPuzzleStopInvoked = true;
                
                puzzle.StartPuzzle();
                Assert.IsTrue(onPuzzleStartInvoked);
                Assert.IsTrue(puzzle.IsPuzzleStarted);
                
                puzzle.StopPuzzle();
                Assert.IsTrue(onPuzzleStopInvoked);
                Assert.IsFalse(puzzle.IsPuzzleStarted);
            }

            
        }

        [Test]
        public void CombinationPuzzle_MovePuzzle_UsingDirections()
        {
            PuzzleInfo puzzleInfo = new PuzzleInfo(
                "TestPuzzle", 
                0, 
                3, 
                new List<int> { 1,0,0,3 }, 
                new List<int> { 0,1,3,0 },  
                null);
            
            if (puzzleInfo.Validate())
            {
                CombinationPuzzle puzzle = new CombinationPuzzle(puzzleInfo);
                puzzle.StartPuzzle();
                
                puzzle.AdjustDial(new Vector2(0,1));
                puzzle.AdjustDial(new Vector2(1,0));
                puzzle.AdjustDial(new Vector2(0,-1));
                puzzle.AdjustDial(new Vector2(1,0));
                puzzle.AdjustDial(new Vector2(0,1)); 
                puzzle.AdjustDial(new Vector2(1,0));
                puzzle.AdjustDial(new Vector2(0,-1)); 
                
                Assert.IsTrue(puzzleInfo.IsPuzzleSolved);
            }

        }
        [Test]
        public void CombinationPuzzle_MovePuzzle_UsingVector2()
        {
            PuzzleInfo puzzleInfo = new PuzzleInfo(
                "TestPuzzle", 
                0, 
                3, 
                new List<int> { 1,0,0,3 }, 
                new List<int> { 0,1,3,0 },  
                null);
            
            if (puzzleInfo.Validate())
            {
                CombinationPuzzle puzzle = new CombinationPuzzle(puzzleInfo);
                puzzle.StartPuzzle();
                
                puzzle.AdjustDial(new Vector2(0,1));
                puzzle.AdjustDial(new Vector2(1,0));
                puzzle.AdjustDial(new Vector2(0,-1));
                puzzle.AdjustDial(new Vector2(1,0));
                puzzle.AdjustDial(new Vector2(0,1)); 
                puzzle.AdjustDial(new Vector2(1,0));
                puzzle.AdjustDial(new Vector2(0,-1)); 
                
                Assert.IsTrue(puzzleInfo.IsPuzzleSolved);
            }

        }
        
    }
}
