namespace MyDungeon.UI.Hud
{
    using UnityEngine;

    /// <summary>
    /// HealthBarDisplay is the class that manages updating the current health bar display in the HUD
    /// </summary>
    public class HealthBarDisplay : MonoBehaviour
    {
        protected const int MaxHealth = 1000;
        protected const int BaseHealth = 50;
        /// <summary>
        /// The RectTransform holding the foreground and background elements of the health bar
        /// </summary>
        protected RectTransform HealthBar;
        /// <summary>
        /// The RectTransform displaying the background image of the health bar
        /// </summary>
        protected RectTransform HealthBarBackground;
        /// <summary>
        /// The RectTransform displaying the foreground image of the health bar
        /// </summary>
        protected RectTransform HealthBarForeground;

        /// <summary>
        /// Finds and stores the health bar, it's foreground, and it's background objects in the HUD
        /// </summary>
        protected virtual void Start()
        {
            HealthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<RectTransform>();
            HealthBarBackground = GameObject.FindGameObjectWithTag("HealthBarBackground").GetComponent<RectTransform>();
            HealthBarForeground = GameObject.FindGameObjectWithTag("HealthBarForeground").GetComponent<RectTransform>();
        }

        /// <summary>
        /// Updates the health bar in the HUD
        /// </summary>
        /// <param name="curHealth">The current health value</param>
        /// <param name="maxHealth">The max health value</param>
        public virtual void UpdateHealthBar(int curHealth, int maxHealth)
        {
            HealthBarForeground.sizeDelta =
                new Vector2(
                    curHealth / (float)maxHealth * Mathf.RoundToInt(
                        BaseHealth + HealthBar.rect.width * Mathf.Sin(maxHealth / (float)MaxHealth) / 2),
                    HealthBarForeground.sizeDelta.y);
            HealthBarBackground.sizeDelta = new Vector2(
                Mathf.RoundToInt(BaseHealth + HealthBar.rect.width * Mathf.Sin(maxHealth / (float)MaxHealth) / 2),
                HealthBarForeground.sizeDelta.y);
        }
    }
}
