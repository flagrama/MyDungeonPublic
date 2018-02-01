using System.Collections.Generic;
using UnityEngine;

namespace MyDungeon
{
    public struct Coord
    {
        public int X, Y;

        public Coord(int p1, int p2)
        {
            X = p1;
            Y = p2;
        }
    }

    public class Room
    {
        public List<Coord> Connections;
        public Direction EnteringCorridor; // The direction of the corridor that is entering this room.
        public int RoomHeight; // How many tiles high the room is.
        public int RoomWidth; // How many tiles wide the room is.
        public int XPos; // The x coordinate of the lower left tile of the room.
        public int YPos; // The y coordinate of the lower left tile of the room.


        // This is used for the first room.  It does not have a Corridor parameter since there are no corridors yet.
        public void SetupRoom(IntRange widthRange, IntRange heightRange, int col, int row)
        {
            Connections = new List<Coord>();

            // Set a random width and height.
            RoomWidth = widthRange.Random;
            RoomHeight = heightRange.Random;

            // Set the x and y coordinates so the room is roughly in the middle of the board.
            XPos = col;
            YPos = row;

            // Set connection tiles
            Connections.Add(new Coord(Random.Range(XPos, XPos + RoomWidth), YPos)); // Bottom connection
            Connections.Add(new Coord(XPos, Random.Range(YPos, YPos + RoomHeight - 1))); // Left connection
            Connections.Add(new Coord(Random.Range(XPos, XPos + RoomWidth), YPos + RoomHeight - 1)); // Top connection
            Connections.Add(new Coord(XPos + RoomWidth, Random.Range(YPos, YPos + RoomHeight - 1))); // Right connection
        }
    }
}