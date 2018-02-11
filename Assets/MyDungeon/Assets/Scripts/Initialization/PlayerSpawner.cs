using UnityEngine;

namespace MyDungeon
{
    public class PlayerSpawner : MonoBehaviour
    {
        public Player Player;
        public PlayerSpawner Spawner;

        // Use this for initialization
        void Start()
        {
            Instantiate(Player, Spawner.transform.position, Quaternion.identity);
        }
    }
}
