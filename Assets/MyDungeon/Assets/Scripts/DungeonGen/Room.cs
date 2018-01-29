using System.Collections.Generic;
using UnityEngine;

public struct Coord
{
    public int x, y;

    public Coord(int p1, int p2)
    {
        x = p1;
        y = p2;
    }
}

public class Room
{
    public int xPos;                      // The x coordinate of the lower left tile of the room.
    public int yPos;                      // The y coordinate of the lower left tile of the room.
    public int roomWidth;                     // How many tiles wide the room is.
    public int roomHeight;                    // How many tiles high the room is.
    public Direction enteringCorridor;    // The direction of the corridor that is entering this room.
    public List<Coord> connections;


    // This is used for the first room.  It does not have a Corridor parameter since there are no corridors yet.
    public void SetupRoom(IntRange widthRange, IntRange heightRange, int col, int row)
    {
        connections = new List<Coord>();

        // Set a random width and height.
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        // Set the x and y coordinates so the room is roughly in the middle of the board.
        xPos = col;
        yPos = row;

        // Set connection tiles
        connections.Add(new Coord(Random.Range(xPos, xPos + roomWidth), yPos));                 // Bottom connection
        connections.Add(new Coord(xPos, Random.Range(yPos, yPos + roomHeight - 1)));                // Left connection
        connections.Add(new Coord(Random.Range(xPos, xPos + roomWidth), yPos + roomHeight - 1));    // Top connection
        connections.Add(new Coord(xPos + roomWidth, Random.Range(yPos, yPos + roomHeight - 1)));    // Right connection

    }
}
