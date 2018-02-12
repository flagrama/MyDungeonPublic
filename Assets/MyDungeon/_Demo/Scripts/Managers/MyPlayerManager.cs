using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MyDungeon.Demo
{
    public class MyPlayerManager : MyDungeon.PlayerManager
    {
        public static int CurrentHealth;
        public static bool Initialized;
        public static int Level = 1;
        public static int MaxHealth;
        public static string PlayerName;

        public static void InitPlayer(string playerName, int maxHealth)
        {
            PlayerName = playerName;
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
            GameManager.Inventory = new List<Item>();
        }

        public static void Load(MySaveData saveData)
        {
            InitPlayer(saveData.DisplayName, saveData.MaxHealth);
            GameManager.Inventory = saveData.Inventory;
            Initialized = true;
        }
    }
}
