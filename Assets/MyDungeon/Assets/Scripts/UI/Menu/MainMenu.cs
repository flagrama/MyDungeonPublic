#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using SceneField = MyDungeon.Utilities.SceneField;
using EventSystem = UnityEngine.EventSystems.EventSystem;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace MyDungeon
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject MainMenuPrefab;
        public SceneField NewGameScene;
        public SceneField LoadGameScene;
        protected EventSystem EventSystem;
        protected GameObject LastSelected;

        // Use this for initialization
        protected virtual void Start()
        {
            Instantiate(MainMenuPrefab);
            EventSystem = EventSystem.current;
            EventSystem.firstSelectedGameObject = GameObject.FindGameObjectWithTag("UIFirstSelected");
            LastSelected = EventSystem.firstSelectedGameObject;
        }

        // Update is called once per frame
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

        public virtual void NewGame()
        {
            SceneManager.LoadScene(NewGameScene.SceneName);
        }

        public virtual void LoadGame()
        {
            SceneManager.LoadScene(LoadGameScene.SceneName);
        }

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
