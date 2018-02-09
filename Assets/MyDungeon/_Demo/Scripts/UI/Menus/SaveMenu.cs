using UnityEngine;
using System.Collections;

namespace MyDungeon.Demo
{
    public class SaveMenu : MonoBehaviour
    {

        public void SaveGame(SaveData saveData)
        {
            gameObject.GetComponent<SaveLoad>().Save(saveData, Application.persistentDataPath + "/save.sav");
        }
    }
}
