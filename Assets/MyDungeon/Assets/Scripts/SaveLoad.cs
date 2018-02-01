using System.IO;
using UnityEngine;

namespace MyDungeon
{
    public class SaveLoad : MonoBehaviour
    {
        public void Save(SaveData save, string path)
        {
            string json = JsonUtility.ToJson(save);
            File.WriteAllText(path, json);
        }

        public T Load<T>(string path)
        {
            if (File.Exists(path))
            {
                T save = JsonUtility.FromJson<T>(File.ReadAllText(path));
                return save;
            }

            return default(T);
        }
    }
}
