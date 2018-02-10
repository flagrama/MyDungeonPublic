using UnityEngine;
using System.Collections;

namespace MyDungeon
{
    public class PlayerSpawner : MonoBehaviour
    {
        public Player Player;

        // Use this for initialization
        void Start()
        {
            GameObject spawner = GameObject.FindGameObjectWithTag("PlayerSpawn");
            Instantiate(Player, spawner.transform.position, Quaternion.identity);
        }
    }
}
