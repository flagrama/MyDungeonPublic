using System.Collections.Generic;
using UnityEngine;

namespace MyDungeon.Demo
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        public int BaseXp = 100;
        [HideInInspector] public int CurHealth;
        [HideInInspector] public int CurXp;
        [HideInInspector] public bool Initialized = false;
        [HideInInspector] public List<Item> Inventory;
        [HideInInspector] public int Level = 1;
        public float LevelFactor = 2;
        [HideInInspector] public int MaxHealth;
        [HideInInspector] public int NextXp;
        [HideInInspector] public string PlayerName;

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
            Invoke("UpdateLevel", GameManager.Instance.LevelStartDelay);
            Invoke("UpdateXp", GameManager.Instance.LevelStartDelay);
            Inventory = new List<Item>();
        }

        private void LevelUp()
        {
            Level++;
            UpdateLevel();
            HudManager.Instance.AddMessage(PlayerName + " has reached level " + Level);
        }

        private int CalculateNextXp()
        {
            int xp = BaseXp * Mathf.RoundToInt(Mathf.Pow(Level, LevelFactor));

            return xp;
        }

        private void UpdateLevel()
        {
            HudManager.Instance.UpdateLevel(Level);
        }

        private void UpdateXp()
        {
#if UNITY_EDITOR
            HudManager.Instance.UpdateXp(CurXp, NextXp);
#endif
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

            UpdateXp();
            HudManager.Instance.AddMessage(PlayerName + " gained " + xp + " experience points");
        }

        public void AddItem(Item item)
        {
            Inventory.Add(item);
        }

        public void RemoveItem(Item item)
        {
            Inventory.Remove(item);
        }
    }
}