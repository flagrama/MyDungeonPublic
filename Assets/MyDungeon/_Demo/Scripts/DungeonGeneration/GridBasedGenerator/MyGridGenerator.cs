using UnityEngine;

namespace MyDungeon.Demo
{
    public class MyGridGenerator : GridGenerator
    {
        public GameObject[] Creatures;
        public GameObject[] Items;
        public GameObject Exit;
        public GameObject Player;
        public Utilities.IntRange CreatureMaxCount = new Utilities.IntRange(6, 10);
        public Utilities.IntRange ItemMaxCount = new Utilities.IntRange(6, 10);

        private int _creatureCount;
        private int _itemCount;

        protected override void InstantiateTiles()
        {
            base.InstantiateTiles();

            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i, j] == TileType.Player)
                    {
                        Vector3 pos = new Vector3(i, j, 0);
                        Instantiate(Player, pos, Quaternion.identity);
                    }

                    if (Board[i, j] == TileType.Exit)
                    {
                        Vector3 pos = new Vector3(i, j, 0);
                        Instantiate(Exit, pos, Quaternion.identity);
                    }

                    if (Board[i, j] == TileType.Creature)
                    {
                        InstantiateFromArray(Creatures, i, j);
                    }

                    if (Board[i, j] == TileType.Item)
                    {
                        InstantiateFromArray(Items, i, j);
                    }
                }
            }
        }

        protected override void SetTilesValuesForRooms()
        {
            int exitRoom = Mathf.RoundToInt(Random.Range(Rooms.Length / 2, Rooms.Length));
            int playerRoom = Mathf.RoundToInt(Random.Range(0, Rooms.Length / 2));
            int playerX = 0;
            int playerY = 0;
            int exitX = 0;
            int exitY = 0;

            _creatureCount = CreatureMaxCount.Random;
            _itemCount = ItemMaxCount.Random;

            base.SetTilesValuesForRooms();
            
            for (int i = 0; i < Rooms.Length; i++)
            {
                Room currentRoom = Rooms[i];

                if (_itemCount > 0)
                {
                    int r = Random.Range(0, 100);
                    int itemPosX;
                    int itemPosY;

                    if (r < 30)
                    {
                        itemPosX = Random.Range(currentRoom.XPos, currentRoom.XPos + currentRoom.RoomWidth);
                        itemPosY = Random.Range(currentRoom.YPos, currentRoom.YPos + currentRoom.RoomHeight);
                        Board[itemPosX, itemPosY] = TileType.Item;
                        _itemCount--;
                    }
                    else if (r >= 30 && r < 60)
                    {
                        itemPosX = Random.Range(currentRoom.XPos, currentRoom.XPos + currentRoom.RoomWidth);
                        itemPosY = Random.Range(currentRoom.YPos, currentRoom.YPos + currentRoom.RoomHeight);
                        Board[itemPosX, itemPosY] = TileType.Item;
                        _creatureCount--;

                        itemPosX = Random.Range(currentRoom.XPos, currentRoom.XPos + currentRoom.RoomWidth);
                        itemPosY = Random.Range(currentRoom.YPos, currentRoom.YPos + currentRoom.RoomHeight);
                        Board[itemPosX, itemPosY] = TileType.Item;
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
                        Board[creaturePosX, creaturePosY] = TileType.Creature;
                        _creatureCount--;
                    }
                    else if (r >= 40 && r < 50)
                    {
                        creaturePosX = Random.Range(currentRoom.XPos, currentRoom.XPos + currentRoom.RoomWidth);
                        creaturePosY = Random.Range(currentRoom.YPos, currentRoom.YPos + currentRoom.RoomHeight);
                        Board[creaturePosX, creaturePosY] = TileType.Creature;
                        _creatureCount--;

                        creaturePosX = Random.Range(currentRoom.XPos, currentRoom.XPos + currentRoom.RoomWidth);
                        creaturePosY = Random.Range(currentRoom.YPos, currentRoom.YPos + currentRoom.RoomHeight);
                        Board[creaturePosX, creaturePosY] = TileType.Creature;
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

            Board[exitX, exitY] = TileType.Exit;
            Board[playerX, playerY] = TileType.Player;
        }
    }
}
