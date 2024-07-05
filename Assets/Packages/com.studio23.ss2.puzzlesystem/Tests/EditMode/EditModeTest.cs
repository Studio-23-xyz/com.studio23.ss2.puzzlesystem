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
            DialPuzzleInfo dialPuzzleInfo = new DialPuzzleInfo("TestPuzzle", 1, 10, new List<int> { 1, 2, 3 }, new List<int> { 0,0,0 },  null);
             dialPuzzleInfo.SetCurrentValues(0,1);
             dialPuzzleInfo.SetCurrentValues(1,2);
             dialPuzzleInfo.SetCurrentValues(2,3);
             // puzzleInfo.SetCurrentValues(new List<int> { 1, 2, 3 });
            Assert.IsTrue(dialPuzzleInfo.IsPuzzleSolved);
        }
        [Test]
        public void PuzzleInfo_CheckPuzzleStatus_IncorrectSolution_ShouldReturnFalse()
        {
            DialPuzzleInfo dialPuzzleInfo = new DialPuzzleInfo("TestPuzzle", 1, 10, new List<int> { 1, 2, 3 }, new List<int> { 1, 4, 3 },  null);
            Assert.IsFalse(dialPuzzleInfo.IsPuzzleSolved);
        }
        [Test]
        public void PuzzleInfo_SetCurrentValues_OutsideRange_ShouldWrapInsideRange()
        {
            DialPuzzleInfo dialPuzzleInfo = new DialPuzzleInfo("TestPuzzle", 0, 10, 
                new List<int> { 1, 2, 3 }, 
                new List<int> { 0,0,0 },  
                null);

            /*puzzleInfo.SetCurrentValues(0,-20);
            puzzleInfo.SetCurrentValues(1,20);
            puzzleInfo.SetCurrentValues(2,5);*/
            dialPuzzleInfo.SetCurrentValues(new List<int> { -20,20,5 });
            Assert.AreEqual(10, dialPuzzleInfo.CurrentValues[0]);
            Assert.AreEqual(0, dialPuzzleInfo.CurrentValues[1]);
            Assert.AreEqual(5, dialPuzzleInfo.CurrentValues[2]);
        }
        
      

        [Test]
        public void CombinationPuzzle_StartPuzzle_ShouldSetIsPuzzleStartedToTrue()
        {
            DialPuzzleInfo dialPuzzleInfo = new DialPuzzleInfo("TestPuzzle", 0, 9, new List<int> { 1, 2, 3 }, new List<int> { 1,2,1 },  null);

            if(dialPuzzleInfo.Validate()) {
                var dialPuzzleBase = new CombinationDialPuzzle(dialPuzzleInfo);
                
                
                bool onPuzzleStartInvoked = false;
                dialPuzzleBase.OnPuzzleStart += () => onPuzzleStartInvoked = true;
                
                dialPuzzleBase.StartPuzzle();
                
                Assert.IsTrue(onPuzzleStartInvoked);
                Assert.IsTrue(dialPuzzleBase.IsPuzzleStarted);
            }
        }

        


        [Test]
        public void CombinationPuzzle_StopPuzzle_ShouldSetIsPuzzleStartedToFalse()
        {
            DialPuzzleInfo dialPuzzleInfo = new DialPuzzleInfo("TestPuzzle", 0, 9, new List<int> { 1, 2, 3 }, new List<int> { 1,2,1 },  null);
            if (dialPuzzleInfo.Validate())
            {
                CombinationDialPuzzle dialPuzzleBase = new CombinationDialPuzzle(dialPuzzleInfo);
                
                bool onPuzzleStartInvoked = false;
                dialPuzzleBase.OnPuzzleStart += () => onPuzzleStartInvoked = true;
                
                bool onPuzzleStopInvoked = false;
                dialPuzzleBase.OnPuzzleStop += () => onPuzzleStopInvoked = true;
                
                dialPuzzleBase.StartPuzzle();
                Assert.IsTrue(onPuzzleStartInvoked);
                Assert.IsTrue(dialPuzzleBase.IsPuzzleStarted);
                
                dialPuzzleBase.StopPuzzle();
                Assert.IsTrue(onPuzzleStopInvoked);
                Assert.IsFalse(dialPuzzleBase.IsPuzzleStarted);
            }

            
        }

        [Test]
        public void CombinationPuzzle_MovePuzzle_UsingDirections()
        {
            DialPuzzleInfo dialPuzzleInfo = new DialPuzzleInfo(
                "TestPuzzle", 
                0, 
                3, 
                new List<int> { 1,0,0,3 }, 
                new List<int> { 0,1,3,0 },  
                null);
            
            if (dialPuzzleInfo.Validate())
            {
                CombinationDialPuzzle dialPuzzleBase = new CombinationDialPuzzle(dialPuzzleInfo);
                dialPuzzleBase.StartPuzzle();
                
                dialPuzzleBase.AdjustDial(new Vector2(0,1));
                dialPuzzleBase.AdjustDial(new Vector2(1,0));
                dialPuzzleBase.AdjustDial(new Vector2(0,-1));
                dialPuzzleBase.AdjustDial(new Vector2(1,0));
                dialPuzzleBase.AdjustDial(new Vector2(0,1)); 
                dialPuzzleBase.AdjustDial(new Vector2(1,0));
                dialPuzzleBase.AdjustDial(new Vector2(0,-1)); 
                
                Assert.IsTrue(dialPuzzleInfo.IsPuzzleSolved);
            }

        }
        [Test]
        public void CombinationPuzzle_MovePuzzle_UsingVector2()
        {
            DialPuzzleInfo dialPuzzleInfo = new DialPuzzleInfo(
                "TestPuzzle", 
                0, 
                3, 
                new List<int> { 1,0,0,3 }, 
                new List<int> { 0,1,3,0 },  
                null);
            
            if (dialPuzzleInfo.Validate())
            {
                CombinationDialPuzzle dialPuzzleBase = new CombinationDialPuzzle(dialPuzzleInfo);
                dialPuzzleBase.StartPuzzle();
                
                dialPuzzleBase.AdjustDial(new Vector2(0,1));
                dialPuzzleBase.AdjustDial(new Vector2(1,0));
                dialPuzzleBase.AdjustDial(new Vector2(0,-1));
                dialPuzzleBase.AdjustDial(new Vector2(1,0));
                dialPuzzleBase.AdjustDial(new Vector2(0,1)); 
                dialPuzzleBase.AdjustDial(new Vector2(1,0));
                dialPuzzleBase.AdjustDial(new Vector2(0,-1)); 
                
                Assert.IsTrue(dialPuzzleInfo.IsPuzzleSolved);
            }

        }
        
    }
}
