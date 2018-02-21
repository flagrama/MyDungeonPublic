namespace MyDungeon.Initialization
{
    using DungeonGeneration.GridBasedGenerator;
    using Entities;
    using Managers;
    using Utilities;
    using UnityEngine;

    /// <summary>
    /// Initializes the dungeon scenes
    /// </summary>
    public class InitGame : MonoBehaviour
    {
        /// <summary>
        /// Time to wait before enabling player
        /// </summary>
        public float LevelStartDelay = 2f;

        private PlayerDungeon _player;

        /// <summary>
        /// Initialize dungeon and PlayerDungeon
        /// </summary>
        protected virtual void Start()
        {
            //this.Invoke(GenerateBoard, LevelStartDelay);
            GenerateBoard();
            try
            {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDungeon>();
            _player.enabled = false;
            this.Invoke(EnableDungeonPlayer, LevelStartDelay);
            }
            catch
            { 
                MyDungeonErrors.PlayerDungeonMustBeSpawnedInDungeon();
            }
        }

        /// <summary>
        /// Enable the DungeonPlayer
        /// </summary>
        protected virtual void EnableDungeonPlayer()
        {
            _player.enabled = true;
        }

        /// <summary>
        /// Start dungeon generation
        /// </summary>
        protected virtual void GenerateBoard()
        {
            try
            {
                DungeonManager.DungeonGenerationSettings = GetComponent<GridGenerator>();
                DungeonManager.DungeonGenerationSettings.GenerateBoard();
            }
            catch
            {
                Utilities.MyDungeonErrors.GridGeneratorOnDungeonManagerNotFound(gameObject.name);
            }
        }
    }
}
