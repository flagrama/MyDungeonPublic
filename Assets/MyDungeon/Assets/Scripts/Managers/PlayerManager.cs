using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance = null;

    public int baseXp = 100;
    public float levelFactor = 2;
    
    [HideInInspector] public bool initialized = false;
    [HideInInspector] public string playerName;
    [HideInInspector] public int maxHealth = 0;
    [HideInInspector] public int curHealth = 0;
    [HideInInspector] public int level = 1;
    [HideInInspector] public int curXp;
    [HideInInspector] public int nextXp;
    [HideInInspector] public List<Item> inventory;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void InitPlayer(string name, int maxHp)
    {
        playerName = name;
        maxHealth = maxHp;
        curHealth = maxHealth;
        nextXp = instance.CalculateNextXp();
        Invoke("UpdateLevel", GameManager.instance.levelStartDelay);
        Invoke("UpdateXp", GameManager.instance.levelStartDelay);
        inventory = new List<Item>();
    }

    void LevelUp()
    {
        level++;
        UpdateLevel();
        HudManager.instance.AddMessage(playerName + " has reached level " + level);
    }

    int CalculateNextXp()
    {
        int xp = baseXp * Mathf.RoundToInt(Mathf.Pow(level, levelFactor));

        return xp;
    }

    void UpdateLevel()
    {
        HudManager.instance.UpdateLevel(level);
    }

    void UpdateXp()
    {
#if UNITY_EDITOR
        HudManager.instance.UpdateXp(curXp, nextXp);
#endif
    }

    public void GainXp(int xp)
    {
        curXp += xp;
        if (curXp >= nextXp)
        {
            LevelUp();
            curXp -= nextXp;
            nextXp = CalculateNextXp();
        }

        UpdateXp();
        HudManager.instance.AddMessage(playerName + " gained " + xp + " experience points");
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
    }
}
