namespace MyDungeon
{
    /// <summary>
    /// Class for setting up Cells in the Grid-Based dungeon map
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Indicates if a Cell has been used to prevent overlapping rooms
        /// </summary>
        public bool Used;
        /// <summary>
        /// The starting X position of a Cell
        /// </summary>
        public int XPos;
        /// <summary>
        /// The starting Y position of a Cell
        /// </summary>
        public int YPos;

        /// <summary>
        /// Sets up Cell location in map array
        /// </summary>
        /// <param name="column">Cell starting X position</param>
        /// <param name="row">Cell starting Y position</param>
        public virtual void SetupCell(int column, int row)
        {
            XPos = column;
            YPos = row;
            Used = false;
        }
    }
}