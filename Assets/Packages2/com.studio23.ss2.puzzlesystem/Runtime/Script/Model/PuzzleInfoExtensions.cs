namespace Studio23.SS2.PuzzleSystem
{
    public static class PuzzleInfoExtensions
    {
        /// <summary>
        /// Validates the puzzle information.
        /// </summary>
        /// <param name="puzzleInfo">The puzzle information to validate.</param>
        /// <returns>True if the puzzle information is valid, otherwise false.</returns>
        public static bool Validate(this PuzzleInfo puzzleInfo)
        {
            // 1. MaxValue should be greater than MinValue
            if (puzzleInfo.MaxValue <= puzzleInfo.MinValue)
            {
                return false;
            }

            // 2. CurrentValues and ResultValues should be within the range of MinValue and MaxValue
            foreach (var value in puzzleInfo.CurrentValues)
            {
                if (value < puzzleInfo.MinValue || value > puzzleInfo.MaxValue)
                {
                    return false;
                }
            }

            foreach (var value in puzzleInfo.ResultValues)
            {
                if (value < puzzleInfo.MinValue || value > puzzleInfo.MaxValue)
                {
                    return false;
                }
            }

            // 3. CurrentValues and ResultValues capacity should be equal
            if (puzzleInfo.CurrentValues.Count != puzzleInfo.ResultValues.Count)
            {
                return false;
            }

            return true;
        }
    }
}