namespace MyDungeon.UI.Hud
{
    using UnityEngine;

    /// <summary>
    /// LevelDisplay is the class that manages updating the current level display in the HUD
    /// </summary>
    public class LevelDisplay : MonoBehaviour
    {
        /// <summary>
        /// The UI Text object displaying the current level
        /// </summary>
        protected UnityEngine.UI.Text LevelText;

        /// <summary>
        /// Finds the UI Text object that displays the current floor in the HUD
        /// </summary>
        protected virtual void Start()
        {
            LevelText = GameObject.FindGameObjectWithTag("LevelText").GetComponent<UnityEngine.UI.Text>();
        }

        /// <summary>
        /// Updates the level displayed in the HUD and adds padding if the level is less than 3 digits
        /// </summary>
        /// <param name="level">The new level value</param>
        public virtual void UpdateLevel(int level)
        {
            LevelText.text = string.Format("Lv{0,3}", level);
        }
    }
}
