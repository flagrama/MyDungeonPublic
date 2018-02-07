#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace MyDungeon.Demo
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        private GameObject _exitMenu;
        private GameObject _lastSelected;
        private GameObject _mainMenu;
        private GameObject _pauseMenu;
        public EventSystem EventSystem;
        public bool InMainMenu;
        public bool InMenu;

        // Use this for initialization
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void CallbackInitialization()
        {
            //register the callback to be called everytime the scene is loaded
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (Instance.EventSystem == null)
                Instance.EventSystem = EventSystem.current;
        }

        private void InitExitMenu()
        {
            _exitMenu = (GameObject)Instantiate(Resources.Load("ExitCanvas"));
            EventSystem.firstSelectedGameObject = GameObject.Find("ContinueButton");
            EventSystem.SetSelectedGameObject(EventSystem.firstSelectedGameObject);
            _lastSelected = EventSystem.firstSelectedGameObject;
        }

        private void DestroyExitMenu()
        {
            Destroy(_exitMenu);
        }

        public void ContinueOrExitMenu()
        {
            InMenu = true;
            InitExitMenu();
        }

        public void Continue()
        {
            Instance.InMenu = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Continue();
            DestroyExitMenu();
        }

        public void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        Application.Quit();
#endif
        }

        public void NewGame()
        {
            InMainMenu = false;
            InMenu = false;
            SceneManager.LoadScene("Town");
        }
    }
}