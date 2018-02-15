namespace MyDungeon
{
    /// <summary>
    /// Base class for the Player in Overworld scenes
    /// </summary>
    public abstract class PlayerOverworld : MovingOverworldObject
    {
        protected abstract void Interact();
    }
}
