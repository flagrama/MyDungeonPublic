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

            EventSystem = EventSystem.current;
            InitMenus();
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

        // Use this for initialization
        public void InitMenus()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            InMainMenu = currentScene.name == "Main Menu";

            _mainMenu = (GameObject) Instantiate(Resources.Load("MainMenuCanvas"));
            _pauseMenu = (GameObject) Instantiate(Resources.Load("PauseMenuCanvas"));
            _exitMenu = (GameObject) Instantiate(Resources.Load("ExitCanvas"));

            if (InMainMenu)
            {
                InMenu = true;
                _pauseMenu.SetActive(false);
                _exitMenu.SetActive(false);
                _mainMenu.SetActive(true);
#if UNITY_WEBGL
            GameObject.Find("QuitButton").SetActive(false);
#endif
                EventSystem.firstSelectedGameObject = GameObject.Find("StartGameButton");
                _lastSelected = EventSystem.firstSelectedGameObject;
            }
            else
            {
                _pauseMenu.SetActive(true);
                _mainMenu.SetActive(false);
                _exitMenu.SetActive(true);
                _pauseMenu.GetComponent<Canvas>().enabled = false;
                _exitMenu.GetComponent<Canvas>().enabled = false;
                InMenu = false;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
            int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
            if (InMenu && horizontal != 0 || vertical != 0)
            {
                if (EventSystem.currentSelectedGameObject != _lastSelected)
                {
                    if (EventSystem.currentSelectedGameObject == null)
                        EventSystem.SetSelectedGameObject(_lastSelected);
                    else
                        _lastSelected = EventSystem.currentSelectedGameObject;
                }
            }
            if (Input.GetButtonDown("Cancel") && !InMainMenu && GameManager.Instance.PlayersTurn)
            {
                Pause();
            }
        }

        public void Pause()
        {
            InMenu = !InMenu;
            Time.timeScale = Mathf.Approximately(Time.timeScale, 0f) ? 1 : 0;
            GameManager.Instance.Paused = !GameManager.Instance.Paused;
            GameObject.Find("Player(Clone)").GetComponent<Player>().enabled =
                !GameObject.Find("Player(Clone)").GetComponent<Player>().enabled;
            if (SceneManager.GetActiveScene().name != "Town")
            {
                _pauseMenu.GetComponent<Canvas>().enabled = !_pauseMenu.GetComponent<Canvas>().enabled;
                if (InMenu)
                {
                    ScrollView sv = GameObject.Find("ScrollView").GetComponent<ScrollView>();
                    EventSystem.SetSelectedGameObject(GameObject.Find("ExitButton"));
                    sv.Populate();
                }
                else
                {
                    ScrollView sv = GameObject.Find("ScrollView").GetComponent<ScrollView>();
                    sv.Depopulate();
                }
            }
        }

        public void ContinueOrExitMenu()
        {
            InMenu = !InMenu;
            _exitMenu.GetComponent<Canvas>().enabled = !_exitMenu.GetComponent<Canvas>().enabled;
            EventSystem.SetSelectedGameObject(GameObject.Find("ContinueButton"));
            _lastSelected = EventSystem.currentSelectedGameObject;
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