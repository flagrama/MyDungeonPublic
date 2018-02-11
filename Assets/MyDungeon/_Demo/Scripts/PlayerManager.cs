using System.Collections.Generic;
using UnityEngine;

namespace MyDungeon.Demo
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        public int BaseXp = 100;
        public int CurHealth;
        public int CurXp;
        public bool Initialized = false;
        public int Level = 1;
        public float LevelFactor = 2;
        public int MaxHealth;
        public int NextXp;
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
            NextXp = Instance.CalculateNextXp();
            GameManager.Inventory = new List<Item>();
        }

        private void LevelUp()
        {
            Level++;
            UpdateLevel();
            GameObject.FindGameObjectWithTag("HudManager").GetComponent<MessageLogDisplay>().AddMessage(PlayerName + " has reached level " + Level);
        }

        private int CalculateNextXp()
        {
            int xp = BaseXp * Mathf.RoundToInt(Mathf.Pow(Level, LevelFactor));

            return xp;
        }

        private void UpdateLevel()
        {
            GameObject.FindGameObjectWithTag("HudManager").GetComponent<LevelDisplay>().UpdateLevel(Level);
        }

        public void GainXp(int xp)
        {
            CurXp += xp;
            if (CurXp >= NextXp)
            {
                LevelUp();
                CurXp -= NextXp;
                NextXp = CalculateNextXp();
            }

            GameObject.FindGameObjectWithTag("HudManager").GetComponent<MessageLogDisplay>().AddMessage(PlayerName + " gained " + xp + " experience points");
        }

        public void Load(MySaveData saveData)
        {
            Instance.InitPlayer(saveData.DisplayName, saveData.MaxHealth);
            GameManager.Inventory = saveData.Inventory;
            Instance.Initialized = true;
        }
    }
}