using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MyDungeon.Demo
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public static bool SaveLoaded;
        public static SaveData Save;
        
        public static int Floor;
        public static bool Paused = false;
        public static bool PlayersTurn = true;

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