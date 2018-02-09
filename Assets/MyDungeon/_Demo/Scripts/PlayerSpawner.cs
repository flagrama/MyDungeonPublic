using UnityEngine;
using System.Collections;

namespace MyDungeon.Demo
{
    public class PlayerSpawner : MonoBehaviour
    {
        public Player Player;

        // Use this for initialization
        void Start()
        {
            GameObject spawner = GameObject.Find("PlayerSpawn");
            Instantiate(Player, spawner.transform.position, Quaternion.identity);
        }
    }
}
