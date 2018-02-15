namespace MyDungeon
{
    using UnityEngine;

    /// <summary>
    /// PlayerSpawner spawns a PlayerOverworld at the position of the PlayerSpawner
    /// </summary>
    public class PlayerSpawner : MonoBehaviour
    {
        /// <summary>
        /// PlayerOverworld prefab
        /// </summary>
        public PlayerOverworld PlayerOverworld;

        /// <summary>
        /// Instantiates PlayerOverworld at the location of the PlayerSpawner
        /// </summary>
        // Use this for initialization
        protected virtual void Start()
        {
            Instantiate(PlayerOverworld, gameObject.transform.position, Quaternion.identity);
        }
    }
}
