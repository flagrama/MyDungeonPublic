namespace MyDungeon
{
    /// <summary>
    /// Directional values for Corridor
    /// </summary>
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    /// <summary>
    /// Class for creating Corridors
    /// </summary>
    public class Corridor
    {
        /// <summary>
        /// How many units long the corridor is
        /// </summary>
        public int CorridorLength;
        /// <summary>
        /// Which direction the corridor is heading from it's room
        /// </summary>
        public Direction Direction;
        /// <summary>
        /// The x coordinate for the start of the corridor
        /// </summary>
        public int StartXPos;
        /// <summary>
        /// The y coordinate for the start of the corridor
        /// </summary>
        public int StartYPos;

        /// <summary>
        /// Sets up Corridor location in map array
        /// </summary>
        /// <param name="x">Starting X position</param>
        /// <param name="y">Starting Y position</param>
        /// <param name="length">Length of the corridor</param>
        /// <param name="direction">Direction of the corridor</param>
        public void SetupCorridor(int x, int y, int length, Direction direction)
        {
            StartXPos = x;
            StartYPos = y;
            CorridorLength = length;
            Direction = direction;
        }
    }
}