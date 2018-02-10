using MyDungeon.Utilities;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace MyDungeon.Demo
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject MainMenuPrefab;
        public GameObject FirstSelectedGameObject;
        public SceneField NewGameScene;
        public SceneField LoadGameScene;
        protected EventSystem EventSystem;
        protected GameObject LastSelected;

        // Use this for initialization
        void Awake()
        {
            Instantiate(MainMenuPrefab);
            GameObject firstSelected = GameObject.Find(FirstSelectedGameObject.name);
            EventSystem = EventSystem.current;
            EventSystem.firstSelectedGameObject = firstSelected;
            LastSelected = EventSystem.firstSelectedGameObject;
        }

        // Update is called once per frame
        void Update()
        {
            int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
            int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
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

        public void NewGame()
        {
            SceneManager.LoadScene(NewGameScene.SceneName);
        }

        public void LoadGame()
        {
            string path = Application.persistentDataPath + "/save.sav";
            SaveData save = gameObject.GetComponent<SaveLoad>().Load<SaveData>(path);

            if (save != null)
            {
                GameManager.saveLoaded = true;
                GameManager.save = save;
                SceneManager.LoadScene(LoadGameScene.SceneName);
            }
        }

        public void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        Application.Quit();
#endif
        }
    }
}
