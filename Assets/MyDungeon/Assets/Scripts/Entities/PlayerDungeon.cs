namespace MyDungeon.Entities
{
    /// <summary>
    /// Base class for the Player in Dungeon scenes
    /// </summary>
    public abstract class PlayerDungeon : MovingDungeonObject
    {
        protected abstract void AttemptMove(int xDir, int yDir);
    }
}
