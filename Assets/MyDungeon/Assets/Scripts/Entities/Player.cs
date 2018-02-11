using UnityEngine;
using System.Collections;

namespace MyDungeon
{
    public class Player : MovingDungeonObject
    {
        protected override void OnCantMove<T>(T component)
        {
        }
    }
}
