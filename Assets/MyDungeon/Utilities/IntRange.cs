namespace MyDungeon.Utilities
{
    using System;

    /// <summary>
    /// Class to get a random value between two numbers (inclusive)
    /// </summary>
    [Serializable]
    public class IntRange
    {
        /// <summary>
        /// The maximum value for a range
        /// </summary>
        public int Max;
        /// <summary>
        /// The minimum value for a range
        /// </summary>
        public int Min;


        /// <summary>
        /// Set the Range values
        /// </summary>
        /// <param name="min">The minimum value in this range</param>
        /// <param name="max">The maximum value in this range</param>
        public IntRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Get a random value from the range.
        /// </summary>
        public int Random
        {
            get { return UnityEngine.Random.Range(Min, Max); }
        }
    }
}