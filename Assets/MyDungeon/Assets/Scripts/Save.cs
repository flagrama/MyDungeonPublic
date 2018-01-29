using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public List<Item> inventory = new List<Item>();
    public string displayName = "";
    public int maxHealth = -1;
}
