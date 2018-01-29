using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour {

    public float levelStartDelay = 2f;
    public static GameManager instance = null;
    public GameObject loadingCanvas;
    public int seed;
    public GameObject player;

    [HideInInspector] public bool playersTurn = true;
    [HideInInspector] public bool paused = false;
    [HideInInspector] public GridGenerator.TileType[,] board;
    [HideInInspector] public int floor = 0;

    Text loadingText;
    GameObject loadingImage;
    GridGenerator boardScript;
    List<Creature> creatures;
    bool creaturesMoving;
    bool doingSetup = true;

    // Use this for initialization
    void Awake ()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("EventSystem"));

        creatures = new List<Creature>();
        boardScript = GetComponent<GridGenerator>();

        if (SceneManager.GetActiveScene().name == "Dungeon")
            InitGame();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization()
    {
        //register the callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Dungeon")
            instance.InitGame();
        else if (arg0.name == "Town")
        {
            Instantiate(instance.player, new Vector2(0, 0), Quaternion.identity);
            MenuManager.instance.InitMenus();
            HudManager.instance.InitUi();
            instance.floor = 0;
        }
    }

    void InitGame()
    {
        doingSetup = true;
        MenuManager.instance.InitMenus();
        HudManager.instance.InitUi();
        loadingCanvas = (GameObject)Instantiate(Resources.Load("loadingCanvas"));
        loadingImage = GameObject.Find("LoadingImage");
        loadingText = GameObject.Find("LoadingText").GetComponent<Text>();
        loadingImage.SetActive(true);
        creatures.Clear();
        board = new GridGenerator.TileType[boardScript.rows, boardScript.columns];
        floor++;
        Invoke("GenerateBoard", levelStartDelay);
        Invoke("HideLoadingImage", levelStartDelay);
    }

    void GenerateBoard()
    {
        boardScript.GenerateBoard();
    }

    void HideLoadingImage()
    {
        loadingImage.SetActive(false);
        doingSetup = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playersTurn || creaturesMoving || doingSetup)
            return;

        StartCoroutine(MoveCreatures());
	}

    public void AddCreatureToList(Creature script)
    {
        creatures.Add(script);
    }

    public void RemoveCreatureFromList(Creature script)
    {
        creatures.Remove(script);
    }

    public void GameOver()
    {
        loadingText.text = "YOU DIED";
        loadingImage.SetActive(true);
        enabled = false;
    }

    IEnumerator MoveCreatures()
    {
        creaturesMoving = true;

        yield return new WaitForSeconds(0.2f);

        for(int i = 0; i < creatures.Count; i++)
            creatures[i].MoveCreature();

        yield return null;

        creaturesMoving = false;
        playersTurn = true;
    }

    public void Save()
    {
        Save save = new Save();
        save.inventory = PlayerManager.instance.inventory;
        save.displayName = PlayerManager.instance.playerName;
        save.maxHealth = PlayerManager.instance.maxHealth;

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.persistentDataPath + "/save.sav", json);
        HudManager.instance.AddMessage("Game Saved!");
#if UNITY_EDITOR
        Debug.Log("Game Saved to " + Application.persistentDataPath + "/save.sav");
#endif
    }
}
