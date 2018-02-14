using UnityEngine;

namespace MyDungeon
{
    public abstract class PlayerDungeon : MovingDungeonObject
    {
        protected abstract void AttemptMove(int xDir, int yDir);
    }
}
