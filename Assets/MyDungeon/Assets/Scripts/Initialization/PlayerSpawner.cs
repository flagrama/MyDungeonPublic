using UnityEngine;

namespace MyDungeon
{
    public class PlayerSpawner : MonoBehaviour
    {
        public PlayerOverworld PlayerOverworld;
        public PlayerSpawner Spawner;

        // Use this for initialization
        void Start()
        {
            Instantiate(PlayerOverworld, Spawner.transform.position, Quaternion.identity);
        }
    }
}
