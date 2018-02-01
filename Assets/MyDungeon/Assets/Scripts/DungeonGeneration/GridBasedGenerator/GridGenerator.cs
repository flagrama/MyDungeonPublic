using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyDungeon
{
    public class GridGenerator : MonoBehaviour
    {
        public enum TileType
        {
            Exit,
            Item,
            Floor,
            Wall,
            Creature,
            Player
        }

        private TileType[,] _board;
        private GameObject _boardHolder; // GameObject that acts as a container for all other tiles.
        private Cell[] _cells;
        private List<Corridor> _corridors; // All the corridors that connect the rooms.
        private int _creatureCount;
        private int _itemCount;
        private IntRange _roomHeight; // The range of heights rooms can have.
        private Room[] _rooms; // All the rooms that are created for this board.
        private IntRange _roomWidth; // The range of widths rooms can have.
        public int CellSizeH = 10;
        public int CellSizeW = 10;
        public int Columns = 100; // The number of columns on the board (how wide it will be).
        public IntRange CorridorLength = new IntRange(6, 10); // The range of lengths corridors between rooms can have.
        public IntRange CreatureLevelMaxCount = new IntRange(6, 10);
        public GameObject[] Creatures;
        public GameObject Exit;
        public GameObject[] FloorTiles; // An array of floor tile prefabs.
        public IntRange ItemLevelMaxCount = new IntRange(6, 10);
        public GameObject[] Items;
        public IntRange NumRooms = new IntRange(15, 20); // The range of the number of rooms there can be.
        public GameObject[] OuterWallTiles; // An array of outer wall tile prefabs.
        public GameObject Player;
        public int RoomMinHeight = 3;
        public int RoomMinWidth = 3;
        public int Rows = 100; // The number of rows on the board (how tall it will be).
        public int Seed;
        public GameObject[] WallTiles; // An array of wall tile prefabs.

        public TileType[,] GenerateBoard()
        {
#if UNITY_EDITOR
            Seed = (int) DateTime.Now.Ticks;
            //seed = 0;
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


        public TileType[,] SetupTilesArray()
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
                }
            }
        }


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