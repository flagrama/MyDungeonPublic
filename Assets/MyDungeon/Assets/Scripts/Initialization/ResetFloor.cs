using UnityEngine;
using System.Collections;

namespace MyDungeon
{
    public class ResetFloor : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            MyDungeon.GameManager.Floor = 0;
        }
    }
}
