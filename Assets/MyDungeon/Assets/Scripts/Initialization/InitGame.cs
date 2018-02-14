using UnityEngine;

namespace MyDungeon
{
    public class InitGame : MonoBehaviour
    {
        public float LevelStartDelay = 2f;

        protected virtual void Start()
        {
            this.Invoke(GenerateBoard, LevelStartDelay);
        }

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
