using MyDungeon.Saving;
using UnityEngine;

namespace MyDungeon.UI.Menu
{
    public class SaveMenu : MonoBehaviour
    {

        public virtual void SaveGame(SaveData saveData, string path)
        {
            gameObject.GetComponent<SaveLoad>().Save(saveData, path);
        }
    }
}
