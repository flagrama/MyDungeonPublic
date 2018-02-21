namespace MyDungeon.UI.Menu
{
    using Managers;
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// PauseMenu is the class that handles the menu that pauses the game
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        /// <summary>
        /// Prefab of the Pause Menu
        /// </summary>
        public GameObject PauseMenuPrefab;
        protected EventSystem EventSystem;
        protected GameObject LastSelected;

        /// <summary>
        /// Grabs reference to the current EventSystem
        /// </summary>
        protected virtual void Start()
        {
            EventSystem = EventSystem.current;
        }

        /// <summary>
        /// Places selection back on the menu if movement controls are pressed after losing menu focus
        /// </summary>
        protected virtual void Update()
        {
            int horizontal = Mathf.RoundToInt(ControlManager.MenuHorizontal);
            int vertical = Mathf.RoundToInt(ControlManager.MenuVertical);
            if (horizontal != 0 || vertical != 0)
            {
                if (EventSystem.currentSelectedGameObject != LastSelected)
                {
                    if (EventSystem.currentSelectedGameObject == null)
                        EventSystem.SetSelectedGameObject(LastSelected);
                    else
                        LastSelected = EventSystem.currentSelectedGameObject;
                }
            }
        }


        /// <summary>
        /// Called when the Pause control is pressed
        /// </summary>
        protected virtual void Pause()
        {
            GameManager.Paused = !GameManager.Paused;

            Time.timeScale = Mathf.Approximately(Time.timeScale, 0f) ? 1 : 0;
        }
    }
}
