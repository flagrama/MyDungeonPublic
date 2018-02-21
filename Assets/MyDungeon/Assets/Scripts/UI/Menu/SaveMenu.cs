namespace MyDungeon.UI.Menu
{
    using Saving;
    using UnityEngine;

    /// <summary>
    /// SaveMenu is the class that handles the menu that allows the player to save their game
    /// </summary>
    public class SaveMenu : MonoBehaviour
    {
        /// <summary>
        /// Calls the Save method of a sibling SaveLoad component
        /// </summary>
        /// <param name="saveData">The serialized data to be saved to a JSON save file</param>
        /// <param name="path">The path of the save file (eg: C:\Game\Game1.sav)</param>
        public virtual void SaveGame(SaveData saveData, string path)
        {
            try
            {
                gameObject.GetComponent<SaveLoad>().Save(saveData, path);
            }
            catch
            {
                Utilities.MyDungeonErrors.SaveLoadComponentNotFound(name);
            }
        }
    }
}
