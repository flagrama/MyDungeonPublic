using UnityEngine;
using System.Collections;

namespace MyDungeon
{
    public class Player : MovingObject
    {
        protected override void OnCantMove<T>(T component)
        {
        }
    }
}
