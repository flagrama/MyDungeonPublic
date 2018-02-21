namespace MyDungeon.UI.Hud
{
    using UnityEngine;

    /// <summary>
    /// FloorDisplay is the class that manages updating the current floor display in the HUD
    /// </summary>
    public class FloorDisplay : MonoBehaviour
    {
        /// <summary>
        /// The UI Text object displaying the current floor
        /// </summary>
        protected UnityEngine.UI.Text FloorText;

        /// <summary>
        /// Finds the UI Text object that displays the current floor in the HUD
        /// </summary>
        protected virtual void Start()
        {
            FloorText = GameObject.FindGameObjectWithTag("FloorText").GetComponent<UnityEngine.UI.Text>();
        }

        /// <summary>
        /// Updates the floor displayed in the HUD and adds padding if the floor is less than 3 digits
        /// </summary>
        /// <param name="floor">The new floor value</param>
        public virtual void UpdateFloor(int floor)
        {
            FloorText.text = string.Format("{0,3}F", floor);
        }
    }
}
