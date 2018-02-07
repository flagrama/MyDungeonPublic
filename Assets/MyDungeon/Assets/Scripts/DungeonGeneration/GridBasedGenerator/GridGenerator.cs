using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyDungeon
{
    /// <summary>
    /// Class for generating a pseudo-random grid-based dungeon
    /// </summary>
    public class GridGenerator : MonoBehaviour
    {
        /// <summary>
        /// Values of tiles that can be placed in dungeon grid
        /// </summary>
        public enum TileType
        {
            Exit,
            Item,
            Floor,
            Wall,
            Creature,
            Player
        }

        /// <summary>
        /// Array representing the map
        /// </summary>
        private TileType[,] _board;
        /// <summary>
        /// GameObject that acts as a container for all other tiles
        /// </summary>
        private GameObject _boardHolder;
        /// <summary>
        /// All the cells available in map
        /// </summary>
        private Cell[] _cells;
        /// <summary>
        /// All the corridors that connect the rooms
        /// </summary>
        private List<Corridor> _corridors;
        /// <summary>
        /// Total number of creatures to spawn in map
        /// </summary>
        private int _creatureCount;
        /// <summary>
        /// Total number of items to spawn in map
        /// </summary>
        private int _itemCount;
        /// <summary>
        /// The range of heights rooms can have
        /// </summary>
        private IntRange _roomHeight;
        /// <summary>
        /// All the rooms that are created for this board
        /// </summary>
        private Room[] _rooms;
        /// <summary>
        /// The range of widths rooms can have
        /// </summary>
        private IntRange _roomWidth;
        /// <summary>
        /// The number of rows in a cell
        /// </summary>
        public int CellSizeH = 10;
        /// <summary>
        /// The number of columns in a cell
        /// </summary>
        public int CellSizeW = 10;
        /// <summary>
        /// The number of columns on the board (how wide it will be)
        /// </summary>
        public int Columns = 100;
        /// <summary>
        /// The range of lengths corridors between rooms can have
        /// </summary>
        public IntRange CorridorLength = new IntRange(6, 10);
        /// <summary>
        /// The range of number of creatures that can appear in the board
        /// </summary>
        public IntRange CreatureLevelMaxCount = new IntRange(6, 10);
        /// <summary>
        /// Array creature prefabs
        /// </summary>
        public GameObject[] Creatures;
        /// <summary>
        /// Exit or staircase prefab
        /// </summary>
        public GameObject Exit;
        /// <summary>
        /// An array of floor tile prefabs
        /// </summary>
        public GameObject[] FloorTiles;
        /// <summary>
        /// The range of number of items that can appear in the board
        /// </summary>
        public IntRange ItemLevelMaxCount = new IntRange(6, 10);
        /// <summary>
        /// An array of item prefabs
        /// </summary>
        public GameObject[] Items;
        /// <summary>
        /// The range of the number of rooms there can be
        /// </summary>
        public IntRange NumRooms = new IntRange(15, 20);
        /// <summary>
        /// An array of outer wall tile prefabs
        /// </summary>
        public GameObject[] OuterWallTiles;
        /// <summary>
        /// Player prefab to spawn into map
        /// </summary>
        public GameObject Player;
        /// <summary>
        /// The smallest a room's height can be
        /// </summary>
        public int RoomMinHeight = 3;
        /// <summary>
        /// The smallest a room's width can be
        /// </summary>
        public int RoomMinWidth = 3;
        /// <summary>
        /// The number of rows on the board (how tall it will be)
        /// </summary>
        public int Rows = 100;
        /// <summary>
        /// The seed used to generate the board
        /// </summary>
        public int Seed;
        /// <summary>
        /// An array of wall tile prefabs
        /// </summary>
        public GameObject[] WallTiles;

        /// <summary>
        /// Generate a new grid-based dungeon map
        /// </summary>
        /// <returns>An 2D array representing the dungeon map</returns>
        public TileType[,] GenerateBoard()
        {
#if UNITY_EDITOR
            Seed = (int) DateTime.Now.Ticks;
            Random.InitState(Seed);
#endif
            // Create the board holder.
            _boardHolder = new GameObject("BoardHolder");

            _board = SetupTilesArray();
            SetupCells();

            CreateRooms();
            CreateCorridors();

            SetTilesValuesForCorridors();
            SetTilesValuesForRooms();

            InstantiateTiles();
            InstantiateOuterWalls();

            return _board;
        }

        /// <summary>
        /// Creates the initial map array filled with impassible terrain
        /// </summary>
        /// <returns>Map array filled with TileType.Wall</returns>
        private TileType[,] SetupTilesArray()
        {
            // Set the tiles jagged array to the correct width.
            _board = new TileType[Columns, Rows];

            // Go through all the tile arrays...
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    _board[i, j] = TileType.Wall;
                }
            }

            return _board;
        }

        /// <summary>
        /// Sets the number of cells contained on the map
        /// </summary>
        private void SetupCells()
        {
            int numCellsHorizontal = Columns / CellSizeW;
            int numCellsVertical = Rows / CellSizeH;
            _cells = new Cell[numCellsHorizontal * numCellsVertical];
            int numcells = 0;

            for (int i = 0; i < numCellsHorizontal; i++)
            {
                for (int j = 0; j < numCellsVertical; j++)
                {
                    _cells[numcells] = new Cell();
                    _cells[numcells].SetupCell(CellSizeW * i, CellSizeH * j);
                    numcells++;
                }
            }
        }

        /// <summary>
        /// Creates the rooms in the map
        /// </summary>
        private void CreateRooms()
        {
            _rooms = new Room[NumRooms.Random];
            int attempts = 3;

            for (int i = 0; i < _rooms.Length; i++)
            {
                int cell = Random.Range(0, _cells.Length - 1);
                _roomWidth = new IntRange(RoomMinWidth, CellSizeW - 1);
                _roomHeight = new IntRange(RoomMinHeight, CellSizeH - 1);

                if (_cells[cell].Used && attempts > 0)
                {
                    attempts--;
                    i--;
                    continue;
                }

                _rooms[i] = new Room();
                _rooms[i].SetupRoom(_roomWidth, _roomHeight, _cells[cell].YPos, _cells[cell].XPos);
                _cells[cell].Used = true;
                attempts = 3;
            }
        }

        /// <summary>
        /// Creates two connected corridors to connect rooms on the map
        /// </summary>
        private void CreateCorridors()
        {
            _corridors = new List<Corridor>();

            for (int i = 0; i < _rooms.Length - 1; i++)
            {
                int randomConnection = Random.Range(0, 3);
                int x1 = _rooms[i].Connections[randomConnection].X;
                int y1 = _rooms[i].Connections[randomConnection].Y;

                randomConnection = Random.Range(0, 3);
                int x2 = _rooms[i + 1].Connections[randomConnection].X;
                int y2 = _rooms[i + 1].Connections[randomConnection].Y;

                Direction direction;
                if (Random.Range(0, 100) < 50) // Horizontal
                {
                    if (x1 < x2)
                    {
                        direction = Direction.East;
                        Corridor currentCorridor = new Corridor();
                        currentCorridor.SetupCorridor(x1, y1, x2 - x1, direction);
                        _corridors.Add(currentCorridor);

                        x1 = x1 + (x2 - x1);

                        if (y1 < y2)
                        {
                            direction = Direction.North;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, y2 - y1, direction);
                            _corridors.Add(currentCorridor);
                        }
                        else
                        {
                            direction = Direction.South;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, y1 - y2, direction);
                            _corridors.Add(currentCorridor);
                        }
                    }
                    else
                    {
                        direction = Direction.West;
                        Corridor currentCorridor = new Corridor();
                        currentCorridor.SetupCorridor(x1, y1, x1 - x2, direction);
                        _corridors.Add(currentCorridor);

                        x1 = x1 - (x1 - x2);

                        if (y1 < y2)
                        {
                            direction = Direction.North;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, y2 - y1, direction);
                            _corridors.Add(currentCorridor);
                        }
                        else
                        {
                            direction = Direction.South;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, y1 - y2, direction);
                            _corridors.Add(currentCorridor);
                        }
                    }
                }
                else // Vertical
                {
                    if (y1 < y2)
                    {
                        direction = Direction.North;
                        Corridor currentCorridor = new Corridor();
                        currentCorridor.SetupCorridor(x1, y1, y2 - y1, direction);
                        _corridors.Add(currentCorridor);

                        y1 = y1 + (y2 - y1);

                        if (x1 < x2)
                        {
                            direction = Direction.East;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, x2 - x1, direction);
                            _corridors.Add(currentCorridor);
                        }
                        else
                        {
                            direction = Direction.West;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, x1 - x2, direction);
                            _corridors.Add(currentCorridor);
                        }
                    }
                    else
                    {
                        direction = Direction.South;
                        Corridor currentCorridor = new Corridor();
                        currentCorridor.SetupCorridor(x1, y1, y1 - y2, direction);
                        _corridors.Add(currentCorridor);

                        y1 = y1 - (y1 - y2);

                        if (x1 < x2)
                        {
                            direction = Direction.East;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, x2 - x1, direction);
                            _corridors.Add(currentCorridor);
                        }
                        else
                        {
                            direction = Direction.West;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, x1 - x2, direction);
                            _corridors.Add(currentCorridor);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// <para>Sets values within rooms in map array to create spawn locations for various objects</para>
        /// <para>Objects include Player, Exit, Monsters and Items</para>
        /// </summary>
        private void SetTilesValuesForRooms()
        {
            int exitRoom = Mathf.RoundToInt(Random.Range(_rooms.Length / 2, _rooms.Length));
            int playerRoom = Mathf.RoundToInt(Random.Range(0, _rooms.Length / 2));
            int playerX = 0;
            int playerY = 0;
            int exitX = 0;
            int exitY = 0;

            _creatureCount = CreatureLevelMaxCount.Random;
            _itemCount = ItemLevelMaxCount.Random;

            // Go through all the rooms...
            for (int i = 0; i < _rooms.Length; i++)
            {
                Room currentRoom = _rooms[i];

                // ... and for each room go through it's width.
                for (int j = 0; j < currentRoom.RoomWidth; j++)
                {
                    int xCoord = currentRoom.XPos + j;

                    // For each horizontal tile, go up vertically through the room's height.
                    for (int k = 0; k < currentRoom.RoomHeight; k++)
                    {
                        int yCoord = currentRoom.YPos + k;

                        // The coordinates in the jagged array are based on the room's position and it's width and height.
                        _board[xCoord, yCoord] = TileType.Floor;
                    }
                }

                if (_itemCount > 0)
                {
                    int r = Random.Range(0, 100);
                    int itemPosX;
                    int itemPosY;

                    if (r < 30)
                    {
                        itemPosX = Random.Range(currentRoom.XPos, currentRoom.XPos + currentRoom.RoomWidth);
                        itemPosY = Random.Range(currentRoom.YPos, currentRoom.YPos + currentRoom.RoomHeight);
                        _board[itemPosX, itemPosY] = TileType.Item;
                        _itemCount--;
                    }
                    else if (r >= 30 && r < 60)
                    {
                        itemPosX = Random.Range(currentRoom.XPos, currentRoom.XPos + currentRoom.RoomWidth);
                        itemPosY = Random.Range(currentRoom.YPos, currentRoom.YPos + currentRoom.RoomHeight);
                        _board[itemPosX, itemPosY] = TileType.Item;
                        _creatureCount--;

                        itemPosX = Random.Range(currentRoom.XPos, currentRoom.XPos + currentRoom.RoomWidth);
                        itemPosY = Random.Range(currentRoom.YPos, currentRoom.YPos + currentRoom.RoomHeight);
                        _board[itemPosX, itemPosY] = TileType.Item;
                        _itemCount--;
                    }
                }

                if (_creatureCount > 0)
                {
                    int r = Random.Range(0, 100);
                    int creaturePosX;
                    int creaturePosY;

                    if (r < 40)
                    {
                        creaturePosX = Random.Range(currentRoom.XPos, currentRoom.XPos + currentRoom.RoomWidth);
                        creaturePosY = Random.Range(currentRoom.YPos, currentRoom.YPos + currentRoom.RoomHeight);
                        _board[creaturePosX, creaturePosY] = TileType.Creature;
                        _creatureCount--;
                    }
                    else if (r >= 40 && r < 50)
                    {
                        creaturePosX = Random.Range(currentRoom.XPos, currentRoom.XPos + currentRoom.RoomWidth);
                        creaturePosY = Random.Range(currentRoom.YPos, currentRoom.YPos + currentRoom.RoomHeight);
                        _board[creaturePosX, creaturePosY] = TileType.Creature;
                        _creatureCount--;

                        creaturePosX = Random.Range(currentRoom.XPos, currentRoom.XPos + currentRoom.RoomWidth);
                        creaturePosY = Random.Range(currentRoom.YPos, currentRoom.YPos + currentRoom.RoomHeight);
                        _board[creaturePosX, creaturePosY] = TileType.Creature;
                        _creatureCount--;
                    }
                }

                if (i == exitRoom)
                {
                    exitX = Mathf.RoundToInt(currentRoom.XPos + currentRoom.RoomWidth / 2);
                    exitY = Mathf.RoundToInt(currentRoom.YPos + currentRoom.RoomHeight / 2);
                }

                if (i == playerRoom)
                {
                    playerX = currentRoom.XPos;
                    playerY = currentRoom.YPos;
                }
            }

            _board[exitX, exitY] = TileType.Exit;
            _board[playerX, playerY] = TileType.Player;
        }

        /// <summary>
        /// Adds corridors to the map array
        /// </summary>
        private void SetTilesValuesForCorridors()
        {
            // Go through every corridor...
            foreach (Corridor currentCorridor in _corridors)
            {
                // and go through it's length.
                for (int j = 0; j < currentCorridor.CorridorLength; j++)
                {
                    // Start the coordinates at the start of the corridor.
                    int xCoord = currentCorridor.StartXPos;
                    int yCoord = currentCorridor.StartYPos;

                    // Depending on the direction, add or subtract from the appropriate
                    // coordinate based on how far through the length the loop is.
                    switch (currentCorridor.Direction)
                    {
                        case Direction.North:
                            yCoord += j;
                            break;
                        case Direction.East:
                            xCoord += j;
                            break;
                        case Direction.South:
                            yCoord -= j;
                            break;
                        case Direction.West:
                            xCoord -= j;
                            break;
                    }

                    // Set the tile at these coordinates to Floor.
                    _board[xCoord, yCoord] = TileType.Floor;
                }
            }
        }

        /// <summary>
        /// Instantiate all of the objects based on the value in the map array
        /// </summary>
        private void InstantiateTiles()
        {
            // Go through all the tiles in the jagged array...
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    // ... and instantiate a floor tile for it.
                    InstantiateFromArray(FloorTiles, i, j);

                    // If the tile type is Wall...
                    if (_board[i, j] == TileType.Wall)
                    {
                        // ... instantiate a wall over the top.
                        InstantiateFromArray(WallTiles, i, j);
                    }

                    // TODO: Move to separate file
                    #region Entity Spawning

                    if (_board[i, j] == TileType.Player)
                    {
                        Vector3 pos = new Vector3(i, j, 0);
                        Instantiate(Player, pos, Quaternion.identity);
                    }

                    if (_board[i, j] == TileType.Exit)
                    {
                        Vector3 pos = new Vector3(i, j, 0);
                        Instantiate(Exit, pos, Quaternion.identity);
                    }

                    if (_board[i, j] == TileType.Creature)
                    {
                        InstantiateFromArray(Creatures, i, j);
                    }

                    if (_board[i, j] == TileType.Item)
                    {
                        InstantiateFromArray(Items, i, j);
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// Instantiate the outer wall of the map
        /// </summary>
        private void InstantiateOuterWalls()
        {
            // The outer walls are one unit left, right, up and down from the board.
            float leftEdgeX = -1f;
            float rightEdgeX = Columns + 0f;
            float bottomEdgeY = -1f;
            float topEdgeY = Rows + 0f;

            // Instantiate both vertical walls (one on each side).
            InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
            InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

            // Instantiate both horizontal walls, these are one in left and right from the outer walls.
            InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
            InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
        }

        /// <summary>
        /// Instantiate the vertical edges of the outer wall
        /// </summary>
        /// <param name="xCoord">The X position of the outer wall tile</param>
        /// <param name="startingY">The starting Y coordinate for the outer wall tiles</param>
        /// <param name="endingY">The ending Y coordinate for the outer wall tiles</param>
        private void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
        {
            // Start the loop at the starting value for Y.
            float currentY = startingY;

            // While the value for Y is less than the end value...
            while (currentY <= endingY)
            {
                // ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
                InstantiateFromArray(OuterWallTiles, xCoord, currentY);

                currentY++;
            }
        }

        /// <summary>
        /// Instantiate the horizontal edges of the outer wall
        /// </summary>
        /// <param name="startingX">The starting X coordinate for the outer wall tiles</param>
        /// <param name="endingX">The ending X coordinate for the outer wall tiles</param>
        /// <param name="yCoord">The Y position of the outer wall tile<</param>
        private void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
        {
            // Start the loop at the starting value for X.
            float currentX = startingX;

            // While the value for X is less than the end value...
            while (currentX <= endingX)
            {
                // ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
                InstantiateFromArray(OuterWallTiles, currentX, yCoord);

                currentX++;
            }
        }

        /// <summary>
        /// Instantiate random prefab from an array of prefabs
        /// </summary>
        /// <param name="prefabs">Array of possible prefabs to instantiate</param>
        /// <param name="xCoord">X position of instantiated prefab</param>
        /// <param name="yCoord">Y position of instantiated prefab</param>
        private void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
        {
            // Create a random index for the array.
            int randomIndex = Random.Range(0, prefabs.Length);

            // The position to be instantiated at is based on the coordinates.
            Vector3 position = new Vector3(xCoord, yCoord, 0f);

            // Create an instance of the prefab from the random index of the array.
            GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

            // Set the tile's parent to the board holder.
            if (tileInstance != null) tileInstance.transform.parent = _boardHolder.transform;
        }
    }
}