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
            Player,
        }

        public int seed = 0;
        public int columns = 100; // The number of columns on the board (how wide it will be).
        public int rows = 100; // The number of rows on the board (how tall it will be).
        public int cellSizeW = 10;
        public int cellSizeH = 10;
        public IntRange numRooms = new IntRange(15, 20); // The range of the number of rooms there can be.
        public int roomMinWidth = 3;
        public int roomMinHeight = 3;
        private IntRange roomWidth; // The range of widths rooms can have.
        private IntRange roomHeight; // The range of heights rooms can have.
        public IntRange corridorLength = new IntRange(6, 10); // The range of lengths corridors between rooms can have.
        public IntRange creatureLevelMaxCount = new IntRange(6, 10);
        public IntRange itemLevelMaxCount = new IntRange(6, 10);
        public GameObject[] floorTiles; // An array of floor tile prefabs.
        public GameObject[] wallTiles; // An array of wall tile prefabs.
        public GameObject[] outerWallTiles; // An array of outer wall tile prefabs.
        public GameObject[] creatures;
        public GameObject[] items;
        public GameObject player;
        public GameObject exit;

        private TileType[,] board;
        private Room[] rooms; // All the rooms that are created for this board.
        private Cell[] cells;
        private List<Corridor> corridors; // All the corridors that connect the rooms.
        private GameObject boardHolder; // GameObject that acts as a container for all other tiles.
        private int creatureCount;
        private int itemCount;

        public TileType[,] GenerateBoard()
        {
#if UNITY_EDITOR
            seed = (int) DateTime.Now.Ticks;
            //seed = 0;
            Random.InitState(seed);
#endif
            // Create the board holder.
            boardHolder = new GameObject("BoardHolder");

            board = SetupTilesArray();
            SetupCells();

            CreateRooms();
            CreateCorridors();

            SetTilesValuesForCorridors();
            SetTilesValuesForRooms();

            InstantiateTiles();
            InstantiateOuterWalls();

            return board;
        }


        public TileType[,] SetupTilesArray()
        {
            // Set the tiles jagged array to the correct width.
            board = new TileType[columns, rows];

            // Go through all the tile arrays...
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    board[i, j] = TileType.Wall;
                }
            }

            //HudManager.instance.UpdateFloor(GameManager.instance.floor);
            return board;
        }

        void SetupCells()
        {
            int numCellsHorizontal = columns / cellSizeW;
            int numCellsVertical = rows / cellSizeH;
            cells = new Cell[numCellsHorizontal * numCellsVertical];
            int numcells = 0;

            for (int i = 0; i < numCellsHorizontal; i++)
            {
                for (int j = 0; j < numCellsVertical; j++)
                {
                    cells[numcells] = new Cell();
                    cells[numcells].SetupCell(cellSizeW * i, cellSizeH * j);
                    numcells++;
                }
            }
        }

        void CreateRooms()
        {
            rooms = new Room[numRooms.Random];
            int attempts = 3;

            for (int i = 0; i < rooms.Length; i++)
            {
                int cell = Random.Range(0, cells.Length - 1);
                roomWidth = new IntRange(roomMinWidth, cellSizeW - 1);
                roomHeight = new IntRange(roomMinHeight, cellSizeH - 1);

                if (cells[cell].Used && attempts > 0)
                {
                    attempts--;
                    i--;
                    continue;
                }

                rooms[i] = new Room();
                rooms[i].SetupRoom(roomWidth, roomHeight, cells[cell].YPos, cells[cell].XPos);
                cells[cell].Used = true;
                attempts = 3;
            }
        }

        void CreateCorridors()
        {
            corridors = new List<Corridor>();

            for (int i = 0; i < rooms.Length - 1; i++)
            {
                int randomConnection = Random.Range(0, 3);
                int x1 = rooms[i].connections[randomConnection].x;
                int y1 = rooms[i].connections[randomConnection].y;

                randomConnection = Random.Range(0, 3);
                int x2 = rooms[i + 1].connections[randomConnection].x;
                int y2 = rooms[i + 1].connections[randomConnection].y;

                Direction direction;
                if (Random.Range(0, 100) < 50) // Horizontal
                {
                    if (x1 < x2)
                    {
                        direction = Direction.East;
                        Corridor currentCorridor = new Corridor();
                        currentCorridor.SetupCorridor(x1, y1, x2 - x1, direction);
                        corridors.Add(currentCorridor);

                        x1 = x1 + (x2 - x1);

                        if (y1 < y2)
                        {
                            direction = Direction.North;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, y2 - y1, direction);
                            corridors.Add(currentCorridor);
                        }
                        else
                        {
                            direction = Direction.South;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, y1 - y2, direction);
                            corridors.Add(currentCorridor);
                        }
                    }
                    else
                    {
                        direction = Direction.West;
                        Corridor currentCorridor = new Corridor();
                        currentCorridor.SetupCorridor(x1, y1, x1 - x2, direction);
                        corridors.Add(currentCorridor);

                        x1 = x1 - (x1 - x2);

                        if (y1 < y2)
                        {
                            direction = Direction.North;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, y2 - y1, direction);
                            corridors.Add(currentCorridor);
                        }
                        else
                        {
                            direction = Direction.South;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, y1 - y2, direction);
                            corridors.Add(currentCorridor);
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
                        corridors.Add(currentCorridor);

                        y1 = y1 + (y2 - y1);

                        if (x1 < x2)
                        {
                            direction = Direction.East;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, x2 - x1, direction);
                            corridors.Add(currentCorridor);
                        }
                        else
                        {
                            direction = Direction.West;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, x1 - x2, direction);
                            corridors.Add(currentCorridor);
                        }
                    }
                    else
                    {
                        direction = Direction.South;
                        Corridor currentCorridor = new Corridor();
                        currentCorridor.SetupCorridor(x1, y1, y1 - y2, direction);
                        corridors.Add(currentCorridor);

                        y1 = y1 - (y1 - y2);

                        if (x1 < x2)
                        {
                            direction = Direction.East;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, x2 - x1, direction);
                            corridors.Add(currentCorridor);
                        }
                        else
                        {
                            direction = Direction.West;
                            currentCorridor = new Corridor();
                            currentCorridor.SetupCorridor(x1, y1, x1 - x2, direction);
                            corridors.Add(currentCorridor);
                        }
                    }
                }
            }
        }

        void SetTilesValuesForRooms()
        {
            int exitRoom = Random.Range(Mathf.RoundToInt(rooms.Length / 2), rooms.Length);
            int playerRoom = Random.Range(0, Mathf.RoundToInt(rooms.Length / 2));
            int playerX = 0;
            int playerY = 0;
            int exitX = 0;
            int exitY = 0;

            creatureCount = creatureLevelMaxCount.Random;
            itemCount = itemLevelMaxCount.Random;

            // Go through all the rooms...
            for (int i = 0; i < rooms.Length; i++)
            {
                Room currentRoom = rooms[i];

                // ... and for each room go through it's width.
                for (int j = 0; j < currentRoom.roomWidth; j++)
                {
                    int xCoord = currentRoom.xPos + j;

                    // For each horizontal tile, go up vertically through the room's height.
                    for (int k = 0; k < currentRoom.roomHeight; k++)
                    {
                        int yCoord = currentRoom.yPos + k;

                        // The coordinates in the jagged array are based on the room's position and it's width and height.
                        board[xCoord, yCoord] = TileType.Floor;
                    }
                }

                if (itemCount > 0)
                {
                    int r = Random.Range(0, 100);
                    int itemPosX;
                    int itemPosY;

                    if (r < 30)
                    {
                        itemPosX = Random.Range(currentRoom.xPos, currentRoom.xPos + currentRoom.roomWidth);
                        itemPosY = Random.Range(currentRoom.yPos, currentRoom.yPos + currentRoom.roomHeight);
                        board[itemPosX, itemPosY] = TileType.Item;
                        itemCount--;
                    }
                    else if (r >= 30 && r < 60)
                    {
                        itemPosX = Random.Range(currentRoom.xPos, currentRoom.xPos + currentRoom.roomWidth);
                        itemPosY = Random.Range(currentRoom.yPos, currentRoom.yPos + currentRoom.roomHeight);
                        board[itemPosX, itemPosY] = TileType.Item;
                        creatureCount--;

                        itemPosX = Random.Range(currentRoom.xPos, currentRoom.xPos + currentRoom.roomWidth);
                        itemPosY = Random.Range(currentRoom.yPos, currentRoom.yPos + currentRoom.roomHeight);
                        board[itemPosX, itemPosY] = TileType.Item;
                        itemCount--;
                    }
                }

                if (creatureCount > 0)
                {
                    int r = Random.Range(0, 100);
                    int creaturePosX;
                    int creaturePosY;

                    if (r < 40)
                    {
                        creaturePosX = Random.Range(currentRoom.xPos, currentRoom.xPos + currentRoom.roomWidth);
                        creaturePosY = Random.Range(currentRoom.yPos, currentRoom.yPos + currentRoom.roomHeight);
                        board[creaturePosX, creaturePosY] = TileType.Creature;
                        creatureCount--;
                    }
                    else if (r >= 40 && r < 50)
                    {
                        creaturePosX = Random.Range(currentRoom.xPos, currentRoom.xPos + currentRoom.roomWidth);
                        creaturePosY = Random.Range(currentRoom.yPos, currentRoom.yPos + currentRoom.roomHeight);
                        board[creaturePosX, creaturePosY] = TileType.Creature;
                        creatureCount--;

                        creaturePosX = Random.Range(currentRoom.xPos, currentRoom.xPos + currentRoom.roomWidth);
                        creaturePosY = Random.Range(currentRoom.yPos, currentRoom.yPos + currentRoom.roomHeight);
                        board[creaturePosX, creaturePosY] = TileType.Creature;
                        creatureCount--;
                    }
                }

                if (i == exitRoom)
                {
                    exitX = currentRoom.xPos + Mathf.RoundToInt(currentRoom.roomWidth / 2);
                    exitY = currentRoom.yPos + Mathf.RoundToInt(currentRoom.roomHeight / 2);
                }

                if (i == playerRoom)
                {
                    playerX = currentRoom.xPos;
                    playerY = currentRoom.yPos;
                }
            }

            board[exitX, exitY] = TileType.Exit;
            board[playerX, playerY] = TileType.Player;
        }


        void SetTilesValuesForCorridors()
        {
            // Go through every corridor...
            for (int i = 0; i < corridors.Count; i++)
            {
                Corridor currentCorridor = corridors[i];

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
                    board[xCoord, yCoord] = TileType.Floor;
                }
            }
        }


        void InstantiateTiles()
        {
            // Go through all the tiles in the jagged array...
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    // ... and instantiate a floor tile for it.
                    InstantiateFromArray(floorTiles, i, j);

                    // If the tile type is Wall...
                    if (board[i, j] == TileType.Wall)
                    {
                        // ... instantiate a wall over the top.
                        InstantiateFromArray(wallTiles, i, j);
                    }

                    if (board[i, j] == TileType.Player)
                    {
                        Vector3 pos = new Vector3(i, j, 0);
                        Instantiate(player, pos, Quaternion.identity);
                    }

                    if (board[i, j] == TileType.Exit)
                    {
                        Vector3 pos = new Vector3(i, j, 0);
                        Instantiate(exit, pos, Quaternion.identity);
                    }

                    if (board[i, j] == TileType.Creature)
                    {
                        InstantiateFromArray(creatures, i, j);
                    }

                    if (board[i, j] == TileType.Item)
                    {
                        InstantiateFromArray(items, i, j);
                    }
                }
            }
        }


        void InstantiateOuterWalls()
        {
            // The outer walls are one unit left, right, up and down from the board.
            float leftEdgeX = -1f;
            float rightEdgeX = columns + 0f;
            float bottomEdgeY = -1f;
            float topEdgeY = rows + 0f;

            // Instantiate both vertical walls (one on each side).
            InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
            InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

            // Instantiate both horizontal walls, these are one in left and right from the outer walls.
            InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
            InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
        }


        void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
        {
            // Start the loop at the starting value for Y.
            float currentY = startingY;

            // While the value for Y is less than the end value...
            while (currentY <= endingY)
            {
                // ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
                InstantiateFromArray(outerWallTiles, xCoord, currentY);

                currentY++;
            }
        }


        void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
        {
            // Start the loop at the starting value for X.
            float currentX = startingX;

            // While the value for X is less than the end value...
            while (currentX <= endingX)
            {
                // ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
                InstantiateFromArray(outerWallTiles, currentX, yCoord);

                currentX++;
            }
        }


        void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
        {
            // Create a random index for the array.
            int randomIndex = Random.Range(0, prefabs.Length);

            // The position to be instantiated at is based on the coordinates.
            Vector3 position = new Vector3(xCoord, yCoord, 0f);

            // Create an instance of the prefab from the random index of the array.
            GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

            // Set the tile's parent to the board holder.
            tileInstance.transform.parent = boardHolder.transform;
        }
    }
}
