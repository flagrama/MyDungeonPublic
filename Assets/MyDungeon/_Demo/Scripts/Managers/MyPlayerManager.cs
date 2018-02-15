using System.Collections.Generic;
using MyDungeon.Items;
using MyDungeon.Managers;
using MyDungeon._Demo.Saving;

namespace MyDungeon._Demo.Managers
{
    public class MyPlayerManager : PlayerManager
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
            Inventory = new List<Item>();
        }

        public static void Load(MySaveData saveData)
        {
            InitPlayer(saveData.DisplayName, saveData.MaxHealth);
            Inventory = saveData.Inventory;
            Initialized = true;
        }

        public override void Reset()
        {
            base.Reset();
            Initialized = false;
        }
    }
}
