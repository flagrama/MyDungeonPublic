using System;

namespace MyDungeon
{
    // Serializable so it will show up in the inspector.
    [Serializable]
    public class IntRange
    {
        public int Max; // The maximum value in this range.
        public int Min; // The minimum value in this range.


        // Constructor to set the values.
        public IntRange(int min, int max)
        {
            Min = min;
            Max = max;
        }


        // Get a random value from the range.
        public int Random
        {
            get { return UnityEngine.Random.Range(Min, Max); }
        }
    }
}