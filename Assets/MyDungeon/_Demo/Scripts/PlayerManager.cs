using System.Collections.Generic;
using UnityEngine;

namespace MyDungeon.Demo
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;
        
        public int CurHealth;
        public bool Initialized;
        public int Level = 1;
        public int MaxHealth;
        public string PlayerName;

        // Use this for initialization
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void InitPlayer(string initPlayerName, int maxHp)
        {
            PlayerName = initPlayerName;
            MaxHealth = maxHp;
            CurHealth = MaxHealth;
            GameManager.Inventory = new List<Item>();
        }

        public void Load(MySaveData saveData)
        {
            Instance.InitPlayer(saveData.DisplayName, saveData.MaxHealth);
            GameManager.Inventory = saveData.Inventory;
            Instance.Initialized = true;
        }
    }
}