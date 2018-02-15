using System.IO;
using UnityEngine;

namespace MyDungeon
{
    /// <summary>
    /// SaveLoad contains the Save and Load methods
    /// </summary>
    public class SaveLoad : MonoBehaviour
    {
        /// <summary>
        /// Creates or overwrites a JSON file containing game save data
        /// </summary>
        /// <param name="save">A serializable class that represents data you wish to save</param>
        /// <param name="path">The path to a save file</param>
        public virtual void Save<T>(T save, string path)
        {
            string json = JsonUtility.ToJson(save);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Reads a JSON file containing game save data
        /// </summary>
        /// <typeparam name="T">A SaveData class that inherits from MyDungeon.SaveData</typeparam>
        /// <param name="path">The path to a save file</param>
        /// <returns>A SaveData object of type T deserialized from the JSON file located at the provided path if found else returns null</returns>
        public virtual T Load<T>(string path)
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