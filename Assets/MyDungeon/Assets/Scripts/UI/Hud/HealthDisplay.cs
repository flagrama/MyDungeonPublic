namespace MyDungeon.UI.Hud
{
    using UnityEngine;

    /// <summary>
    /// HealthDisplay is the class that manages updating the current health display in the HUD
    /// </summary>
    public class HealthDisplay : MonoBehaviour
    {
        /// <summary>
        /// The UI Text object displaying the current floor
        /// </summary>
        protected UnityEngine.UI.Text HealthText;

        /// <summary>
        /// Finds the UI Text object that displays the current health in the HUD
        /// </summary>
        protected virtual void Start()
        {
            HealthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<UnityEngine.UI.Text>();
        }

        /// <summary>
        /// Updates the health displayed in the HUD and adds padding if the health is less than 3 digits
        /// </summary>
        /// <param name="curHealth">The current health value</param>
        /// <param name="maxHealth">The max health value</param>
        public virtual void UpdateHealth(int curHealth, int maxHealth)
        {
            HealthText.text = string.Format("HP:{0,3}/{1,3}", curHealth, maxHealth);
        }
    }
}
