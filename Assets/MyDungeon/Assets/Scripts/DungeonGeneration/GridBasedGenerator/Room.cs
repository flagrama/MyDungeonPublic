namespace MyDungeon
{
    using System.Collections.Generic;
    using Utilities;
    using UnityEngine;

    /// <summary>
    /// Data structure for Room starting coordinates
    /// </summary>
    public struct Coord
    {
        /// <summary>
        /// The starting X coordinate of a room
        /// </summary>
        public int X;
        /// <summary>
        /// The starting Y coordinate of a room
        /// </summary>
        public int Y;

        /// <summary>
        /// Starting coordinates for Rooms
        /// </summary>
        /// <param name="p1">Room starting X position</param>
        /// <param name="p2">Room starting Y position</param>
        public Coord(int p1, int p2)
        {
            X = p1;
            Y = p2;
        }
    }

    /// <summary>
    /// Class for creating dungeon rooms
    /// </summary>
    public class Room
    {
        /// <summary>
        /// The tiles of the room walls that are connected to corridors
        /// </summary>
        public List<Coord> Connections;
        /// <summary>
        /// The direction of the corridor that is entering this room
        /// </summary>
        public Direction EnteringCorridor;
        /// <summary>
        /// How many tiles high the room is
        /// </summary>
        public int RoomHeight;
        /// <summary>
        /// How many tiles wide the room is
        /// </summary>
        public int RoomWidth;
        /// <summary>
        /// The x coordinate of the lower left tile of the room
        /// </summary>
        public int XPos;
        /// <summary>
        /// The y coordinate of the lower left tile of the room
        /// </summary>
        public int YPos;

        /// <summary>
        /// Sets up Rooms in map array
        /// </summary>
        /// <param name="widthRange">Int Range for Room width</param>
        /// <param name="heightRange">Int Range for Room height</param>
        /// <param name="col">Room starting X position</param>
        /// <param name="row">Room starting Y position</param>
        public virtual void SetupRoom(IntRange widthRange, IntRange heightRange, int col, int row)
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