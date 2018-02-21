namespace MyDungeon.UI.Menu
{
    using Managers;
    using Utilities;
    using UnityEngine.EventSystems;
    using UnityEngine.SceneManagement;
#if UNITY_EDITOR
    using UnityEditor;
#endif
    using UnityEngine;

    /// <summary>
    /// MainMenu is the class that handles the menu that allows the player to start a new game, load a saved game, and quit the game
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// Prefab of the Main Menu
        /// </summary>
        public GameObject MainMenuPrefab;
        /// <summary>
        /// Scene that is loaded when New Game is clicked
        /// </summary>
        public SceneField NewGameScene;
        /// <summary>
        /// Scene that is loaded when Load Game is clicked
        /// </summary>
        public SceneField LoadGameScene;
        protected EventSystem EventSystem;
        protected GameObject LastSelected;

        /// <summary>
        /// Instantiates the menu prefab and sets up EventSystem selections
        /// </summary>
        protected virtual void Start()
        {
            Instantiate(MainMenuPrefab);
            EventSystem = EventSystem.current;
            EventSystem.firstSelectedGameObject = GameObject.FindGameObjectWithTag("UIFirstSelected");
            LastSelected = EventSystem.firstSelectedGameObject;
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
        /// Called when the New Game button is clicked
        /// </summary>
        public virtual void NewGame()
        {
            SceneManager.LoadScene(NewGameScene.SceneName);
        }

        /// <summary>
        /// Called when the Load Game button is clicked
        /// </summary>
        public virtual void LoadGame()
        {
            SceneManager.LoadScene(LoadGameScene.SceneName);
        }

        /// <summary>
        /// Called when the Quit Game button is clicked
        /// </summary>
        public virtual void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        Application.Quit();
#endif
        }
    }
}
