using UnityEngine;
using System.Collections;

namespace MyDungeon
{
    public class InitGame : MonoBehaviour
    {
        public float LevelStartDelay = 2f;

        protected virtual void Start()
        {
            MyDungeon.GameManager.Floor++;
            Invoke("UpdateFloor", LevelStartDelay);
            Invoke("GenerateBoard", LevelStartDelay);
        }

        protected virtual void GenerateBoard()
        {
            GetComponent<GridGenerator>().GenerateBoard();
        }

        protected virtual void UpdateFloor()
        {
            GameObject.FindGameObjectWithTag("HudManager").GetComponent<FloorDisplay>().UpdateFloor(MyDungeon.GameManager.Floor);
        }
    }
}
