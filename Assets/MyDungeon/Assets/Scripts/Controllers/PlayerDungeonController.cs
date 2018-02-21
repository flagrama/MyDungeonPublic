namespace MyDungeon.Controllers
{
    using System.Collections;
    using Entities;
    using Managers;
    using UnityEngine;

    /// <summary>
    /// Base class for controller for the Player in the Dungeon scenes
    /// </summary>
    public abstract class PlayerDungeonController : PlayerDungeon
    {
        /// <inheritdoc />
        /// <summary>
        /// Sets the player's current health
        /// </summary>
        protected override void Start()
        {
            UpdateHealth();

            base.Start();
        }

        /// <summary>
        /// Attempts to move the player and ends the player's turn if they moved
        /// </summary>
        /// <param name="xDir">The X direction the player is attempting to move in</param>
        /// <param name="yDir">The Y direction the player is attempting to move in</param>
        protected override void AttemptMove(int xDir, int yDir)
        {
            RaycastHit2D hit;
            bool canMove = Move(xDir, yDir, out hit);

            if (canMove)
            {
                GameManager.PlayersTurn = false;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Reduce player's current health and executes UpdateHealth
        /// </summary>
        /// <param name="damage"></param>
        public override void LoseHealth(int damage)
        {
            base.LoseHealth(damage);

            UpdateHealth();
        }

        /// <inheritdoc />
        /// <summary>
        /// Increase player's current health and executes UpdateHealth
        /// </summary>
        /// <param name="recover"></param>
        public override void RecoverHealth(int recover)
        {
            base.RecoverHealth(recover);

            UpdateHealth();
        }

        /// <summary>
        /// Check if player is out of HP and if so disable the PlayerDungeonController
        /// </summary>
        protected virtual void CheckIfGameOver()
        {
            if (CurHealth <= 0) enabled = false;
        }

        /// <summary>
        /// Ensure health does not go above MaxHealth or below 0 then executes CheckIfGameOver
        /// </summary>
        protected virtual void UpdateHealth()
        {
            if (CurHealth > MaxHealth)
                CurHealth = MaxHealth;
            if (CurHealth < 0)
                CurHealth = 0;

            CheckIfGameOver();
        }

        protected abstract IEnumerator Attack();
    }
}