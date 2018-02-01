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
        private GridGenerator _boardScript;
        private List<Creature> _creatures;
        private bool _creaturesMoving;
        private bool _doingSetup = true;
        private GameObject _loadingImage;
        private Text _loadingText;
        [HideInInspector] public GridGenerator.TileType[,] Board;
        [HideInInspector] public int Floor;
        public float LevelStartDelay = 2f;
        public GameObject LoadingCanvas;
        [HideInInspector] public bool Paused = false;
        public GameObject Player;
        [HideInInspector] public bool PlayersTurn = true;
        public int Seed;

        // Use this for initialization
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            _creatures = new List<Creature>();
            _boardScript = GetComponent<GridGenerator>();

            if (SceneManager.GetActiveScene().name == "Dungeon")
                InitGame();

            if (SceneManager.GetActiveScene().name == "Town")
            {
                Instantiate(Instance.Player, new Vector2(0, 0), Quaternion.identity);
                Instance.Floor = 0;
            }
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
                Instantiate(Instance.Player, new Vector2(0, 0), Quaternion.identity);
                MenuManager.Instance.InitMenus();
                HudManager.Instance.InitUi();
                Instance.Floor = 0;
            }
        }

        private void InitGame()
        {
            _doingSetup = true;
            MenuManager.Instance.InitMenus();
            HudManager.Instance.InitUi();
            LoadingCanvas = (GameObject) Instantiate(Resources.Load("loadingCanvas"));
            _loadingImage = GameObject.Find("LoadingImage");
            _loadingText = GameObject.Find("LoadingText").GetComponent<Text>();
            _loadingImage.SetActive(true);
            _creatures.Clear();
            Board = new GridGenerator.TileType[_boardScript.Rows, _boardScript.Columns];
            Floor++;
            Invoke("GenerateBoard", LevelStartDelay);
            Invoke("HideLoadingImage", LevelStartDelay);
        }

        private void GenerateBoard()
        {
            Board = _boardScript.GenerateBoard();
        }

        private void HideLoadingImage()
        {
            _loadingImage.SetActive(false);
            _doingSetup = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (PlayersTurn || _creaturesMoving || _doingSetup)
                return;

            StartCoroutine(MoveCreatures());
        }

        public void AddCreatureToList(Creature script)
        {
            _creatures.Add(script);
        }

        public void RemoveCreatureFromList(Creature script)
        {
            _creatures.Remove(script);
        }

        public void GameOver()
        {
            _loadingText.text = "YOU DIED";
            _loadingImage.SetActive(true);
            enabled = false;
        }

        private IEnumerator MoveCreatures()
        {
            _creaturesMoving = true;

            yield return new WaitForSeconds(0.2f);

            foreach (Creature creature in _creatures)
                creature.MoveCreature();

            yield return null;

            _creaturesMoving = false;
            PlayersTurn = true;
        }

        public void SaveGame()
        {
            SaveData saveData = new SaveData
            {
                Inventory = PlayerManager.Instance.Inventory,
                DisplayName = PlayerManager.Instance.PlayerName,
                MaxHealth = PlayerManager.Instance.MaxHealth
            };

            gameObject.GetComponent<SaveLoad>().Save(saveData, Application.persistentDataPath + "/save.sav");

            HudManager.Instance.AddMessage("Game Saved!");
#if UNITY_EDITOR
            Debug.Log("Game Saved to " + Application.persistentDataPath + "/save.sav");
#endif
        }

        public void LoadGame()
        {
            string path = Application.persistentDataPath + "/save.sav";

            SaveData save = gameObject.GetComponent<SaveLoad>().Load<SaveData>(path);

            if (save != null)
            {
                PlayerManager.Instance.InitPlayer(save.DisplayName, save.MaxHealth);
                PlayerManager.Instance.Inventory = save.Inventory;
                PlayerManager.Instance.Initialized = true;

                MenuManager.Instance.InMenu = false;
                MenuManager.Instance.InMainMenu = false;
                SceneManager.LoadScene("Town");

#if UNITY_EDITOR
                Debug.Log("Game Loaded");
#endif
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("File " + path + " Not Found!");
#endif
            }
        }
    }
}