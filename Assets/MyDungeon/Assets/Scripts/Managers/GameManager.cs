namespace MyDungeon
{
    using System.Collections.Generic;

    /// <summary>
    /// GameManager stores the current game state that must be retained between scene transitions
    /// </summary>
    public static class GameManager
    {
        /// <summary>
        /// Whether a game save was loaded or not
        /// </summary>
        public static bool SaveLoaded;
        /// <summary>
        /// The current dungeon floor
        /// </summary>
        public static int Floor;
        /// <summary>
        /// Whether the game is paused or not
        /// </summary>
        public static bool Paused;
        /// <summary>
        /// Whether it is currently the player's turn or not
        /// </summary>
        public static bool PlayersTurn = true;

        /// <summary>
        /// Resets all GameManager properties to default values
        /// </summary>
        public static void Reset()
        {
            SaveLoaded = false;
            Floor = 0;
            Paused = false;
            PlayersTurn = true;
        }
    }
}