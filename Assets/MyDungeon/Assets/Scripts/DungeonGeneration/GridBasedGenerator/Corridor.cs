namespace MyDungeon
{
    // Enum to specify the direction is heading.
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public class Corridor
    {
        public int CorridorLength; // How many units long the corridor is.
        public Direction Direction; // Which direction the corridor is heading from it's room.
        public int StartXPos; // The x coordinate for the start of the corridor.
        public int StartYPos; // The y coordinate for the start of the corridor.

        public void SetupCorridor(int x, int y, int length, Direction direction)
        {
            StartXPos = x;
            StartYPos = y;
            CorridorLength = length;
            Direction = direction;
        }
    }
}