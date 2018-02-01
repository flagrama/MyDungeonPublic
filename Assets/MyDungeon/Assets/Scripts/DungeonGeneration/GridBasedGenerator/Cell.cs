namespace MyDungeon
{
    public class Cell
    {
        public int XPos;
        public int YPos;
        public bool Used;

        public void SetupCell(int column, int row)
        {
            XPos = column;
            YPos = row;
            Used = false;
        }
    }
}
