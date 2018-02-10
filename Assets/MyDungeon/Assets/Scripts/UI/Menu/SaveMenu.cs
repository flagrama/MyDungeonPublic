using UnityEngine;
using System.Collections;

namespace MyDungeon
{
    public class SaveMenu : MonoBehaviour
    {

        public virtual void SaveGame(SaveData saveData, string path)
        {
            gameObject.GetComponent<SaveLoad>().Save(saveData, path);
        }
    }
}
