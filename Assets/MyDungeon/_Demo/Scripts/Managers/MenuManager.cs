#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyDungeon.Demo
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        // Use this for initialization
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void Continue()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Continue();
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
            SceneManager.LoadScene("Town");
        }

        public void SaveGame(SaveData saveData)
        {
            gameObject.GetComponent<SaveLoad>().Save(saveData, Application.persistentDataPath + "/save.sav");
            HudManager.Instance.AddMessage("Game Saved!");
        }

        public void LoadGame()
        {
            string path = Application.persistentDataPath + "/save.sav";
            SaveData save = gameObject.GetComponent<SaveLoad>().Load<SaveData>(path);

            if (save != null)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Load(save);
                SceneManager.LoadScene("Town");
            }
        }
    }
}