using UnityEngine;

// Enum to specify the direction is heading.
public enum Direction
{
    North, East, South, West,
}

public class Corridor
{
    public int startXPos;         // The x coordinate for the start of the corridor.
    public int startYPos;         // The y coordinate for the start of the corridor.
    public int corridorLength;            // How many units long the corridor is.
    public Direction direction;   // Which direction the corridor is heading from it's room.

    public void SetupCorridor(int x, int y, int length, Direction direction)
    {
        startXPos = x;
        startYPos = y;
        corridorLength = length;
        this.direction = direction;
    }
}
