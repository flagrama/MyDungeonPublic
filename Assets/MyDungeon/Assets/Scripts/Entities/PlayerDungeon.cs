using UnityEngine;
using System.Collections;

namespace MyDungeon
{
    public class PlayerDungeon : MovingDungeonObject
    {
        protected override void OnCantMove<T>(T component)
        {
        }
    }
}
