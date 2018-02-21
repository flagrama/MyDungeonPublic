namespace MyDungeon.UI.Menu
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// ExitMenu is the class that handles the menu that allows the player to leave the dungeon or continue to the next floor
    /// </summary>
    public class ExitMenu : MonoBehaviour
    {
        /// <summary>
        /// Prefab of the Exit Menu
        /// </summary>
        public GameObject ExitMenuPrefab;
        /// <summary>
        /// Scene that is loaded when Exit is clicked
        /// </summary>
        public Utilities.SceneField ExitScene;
        /// <summary>
        /// Scene that is loaded when Continue is clicked
        /// </summary>
        public Utilities.SceneField ContinueScene;
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
            int horizontal = Mathf.RoundToInt(Managers.ControlManager.MenuHorizontal);
            int vertical = Mathf.RoundToInt(Managers.ControlManager.MenuVertical);
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
        /// Instantiates the menu prefab and sets up EventSystem selections
        /// </summary>
        protected virtual void ShowMenu()
        {
            Instantiate(ExitMenuPrefab);
            GameObject firstSelected = GameObject.FindGameObjectWithTag("UIFirstSelected");
            EventSystem = EventSystem.current;
            EventSystem.firstSelectedGameObject = firstSelected;
            EventSystem.SetSelectedGameObject(firstSelected);
            LastSelected = EventSystem.firstSelectedGameObject;
        }

        /// <summary>
        /// Called when the Continue button is clicked
        /// </summary>
        public virtual void Continue()
        {
            SceneManager.LoadSceneAsync(ContinueScene.SceneName, LoadSceneMode.Single);
        }

        /// <summary>
        /// Called when the Exit button is clicked
        /// </summary>
        public virtual void Exit()
        {
            SceneManager.LoadScene(ExitScene.SceneName);
        }
    }
}
