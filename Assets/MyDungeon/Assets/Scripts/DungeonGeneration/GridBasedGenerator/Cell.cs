namespace MyDungeon
{
    /// <summary>
    /// Class for setting up Cells in the Grid-Based dungeon map
    /// </summary>
    public class Cell
    {
        public bool Used;
        public int XPos;
        public int YPos;

        /// <summary>
        /// Sets up Cell location in map array
        /// </summary>
        /// <param name="column">Cell starting X position</param>
        /// <param name="row">Cell starting Y position</param>
        public void SetupCell(int column, int row)
        {
            XPos = column;
            YPos = row;
            Used = false;
        }
    }
}