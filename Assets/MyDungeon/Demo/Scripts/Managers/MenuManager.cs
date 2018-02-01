using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR

#endif

namespace MyDungeon.Demo
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance = null;
        public bool inMainMenu = false;
        public bool inMenu;
        public EventSystem eventSystem;
        private GameObject pauseMenu;
        private GameObject mainMenu;
        private GameObject exitMenu;
        private GameObject lastSelected;

        // Use this for initialization
        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            eventSystem = EventSystem.current;
            InitMenus();
        }

        // Use this for initialization
        public void InitMenus()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == "Main Menu")
                inMainMenu = true;
            else
                inMainMenu = false;

            mainMenu = (GameObject) Instantiate(Resources.Load("BottomCanvas"));
            pauseMenu = (GameObject) Instantiate(Resources.Load("PauseMenuCanvas"));
            exitMenu = (GameObject) Instantiate(Resources.Load("ExitCanvas"));

            if (inMainMenu)
            {
                inMenu = true;
                pauseMenu.SetActive(false);
                exitMenu.SetActive(false);
                mainMenu.SetActive(true);
#if UNITY_WEBGL
            GameObject.Find("QuitButton").SetActive(false);
#endif
                eventSystem.firstSelectedGameObject = GameObject.Find("StartGameButton");
                lastSelected = eventSystem.firstSelectedGameObject;
            }
            else
            {
                pauseMenu.SetActive(true);
                mainMenu.SetActive(false);
                exitMenu.SetActive(true);
                pauseMenu.GetComponent<Canvas>().enabled = false;
                exitMenu.GetComponent<Canvas>().enabled = false;
                inMenu = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
            int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
            if (inMenu && horizontal != 0 || vertical != 0)
            {
                if (eventSystem.currentSelectedGameObject != lastSelected)
                {
                    if (eventSystem.currentSelectedGameObject == null)
                        eventSystem.SetSelectedGameObject(lastSelected);
                    else
                        lastSelected = eventSystem.currentSelectedGameObject;
                }
            }
            if (Input.GetButtonDown("Cancel") && !inMainMenu && GameManager.instance.playersTurn)
            {
                Pause();
            }
        }

        public void Pause()
        {
            inMenu = !inMenu;
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            GameManager.instance.paused = !GameManager.instance.paused;
            GameObject.Find("Player(Clone)").GetComponent<Player>().enabled =
                !GameObject.Find("Player(Clone)").GetComponent<Player>().enabled;
            if (SceneManager.GetActiveScene().name != "Town")
            {
                pauseMenu.GetComponent<Canvas>().enabled = !pauseMenu.GetComponent<Canvas>().enabled;
                if (inMenu)
                {
                    ScrollView sv = GameObject.Find("ScrollView").GetComponent<ScrollView>();
                    eventSystem.SetSelectedGameObject(GameObject.Find("ExitButton"));
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
            inMenu = !inMenu;
            exitMenu.GetComponent<Canvas>().enabled = !exitMenu.GetComponent<Canvas>().enabled;
            eventSystem.SetSelectedGameObject(GameObject.Find("ContinueButton"));
            lastSelected = eventSystem.currentSelectedGameObject;
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
            inMainMenu = false;
            inMenu = false;
            SceneManager.LoadScene("Town");
        }
    }
}
