namespace MyDungeon
{
    public class Cell
    {
        public bool Used;
        public int XPos;
        public int YPos;

        public void SetupCell(int column, int row)
        {
            XPos = column;
            YPos = row;
            Used = false;
        }
    }
}