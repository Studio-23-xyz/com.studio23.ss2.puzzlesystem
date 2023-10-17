using Studio23.SS2.PuzzleSystem.Data;
using UnityEngine;
using System;

namespace Studio23.SS2.PuzzleSystem.Core
{
    public static class Vector2Extensions
    {
        public static Direction GetDirection(this Vector2 input)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                // Horizontal movement is dominant
                return (input.x > 0) ? Direction.Right : Direction.Left;
            }
            else
            {
                // Vertical movement is dominant
                return (input.y > 0) ? Direction.Up : Direction.Down;
            }
        }
    }
}