using System.Collections.Generic;
using UnityEngine;

namespace MyDungeon
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

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