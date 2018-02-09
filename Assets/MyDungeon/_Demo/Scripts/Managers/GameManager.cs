using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MyDungeon.Demo
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public static bool saveLoaded;
        public static SaveData save;

        private GridGenerator _boardScript;
        private GameObject _loadingImage;
        private Text _loadingText;
        
        public int Floor;
        public float LevelStartDelay = 2f;
        public GameObject LoadingCanvas;
        public bool Paused = false;
        public bool PlayersTurn = true;

        // Use this for initialization
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            _boardScript = GetComponent<GridGenerator>();

            if (SceneManager.GetActiveScene().name == "Dungeon")
                InitGame();

            if (SceneManager.GetActiveScene().name == "Town")
                Instance.Floor = 0;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void CallbackInitialization()
        {
            //register the callback to be called everytime the scene is loaded
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name == "Dungeon")
                Instance.InitGame();
            else if (arg0.name == "Town")
            {
                Instance.Floor = 0;
            }
        }

        private void InitGame()
        {
            LoadingCanvas = (GameObject) Instantiate(Resources.Load("loadingCanvas"));
            _loadingImage = GameObject.Find("LoadingImage");
            _loadingText = GameObject.Find("LoadingText").GetComponent<Text>();
            _loadingImage.SetActive(true);
            Floor++;
            Camera.main.GetComponent<FloorDisplay>().UpdateFloor(Floor);
            Invoke("GenerateBoard", LevelStartDelay);
            Invoke("HideLoadingImage", LevelStartDelay);
        }

        private void GenerateBoard()
        {
            _boardScript.GenerateBoard();
        }

        private void HideLoadingImage()
        {
            _loadingImage.SetActive(false);
        }

        public void GameOver()
        {
            _loadingText.text = "YOU DIED";
            _loadingImage.SetActive(true);
            enabled = false;
        }
    }
}