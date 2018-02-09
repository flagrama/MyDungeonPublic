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
        
        public int Floor;
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
        }
    }
}