using System.Collections;
using MyDungeon.Managers;
using UnityEngine;

namespace MyDungeon.Entities
{
    /// <summary>
    /// Base class for objects that move in the Dungeon scenes
    /// </summary>
    public abstract class MovingDungeonObject : MonoBehaviour
    {
        /// <summary>
        /// Layer used for collision checking in dungeons
        /// </summary>
        public LayerMask BlockingLayer;
        /// <summary>
        /// Base damage value attacks will do
        /// </summary>
        public int Strength = 1;
        /// <summary>
        /// Entity's maximum HP value
        /// </summary>
        public int MaxHealth = 10;
        /// <summary>
        /// How long the entity's turn lasts
        /// </summary>
        public float MoveTime = 0.25f;
        /// <summary>
        /// Name of the entity
        /// </summary>
        public string DisplayName;
        
        protected BoxCollider2D BoxCollider;
        protected Rigidbody2D Rb2D;
        /// <summary>
        /// Entity's current health
        /// </summary>
        protected int CurHealth;
        /// <summary>
        /// Entity's current X position
        /// </summary>
        protected int PosX;
        /// <summary>
        /// Entity's current Y position
        /// </summary>
        protected int PosY;
        /// <summary>
        /// Inverse of MoveTime used for SmoothMovement
        /// </summary>
        protected float InverseMoveTime;
        /// <summary>
        /// Entity's current moving state
        /// </summary>
        protected bool Moving;

        /// <summary>
        /// Initializes the 2DPhysics components, the entity's position, and animation movement multiplier
        /// </summary>
        protected virtual void Start()
        {
            try
            {
                BoxCollider = GetComponent<BoxCollider2D>();
                Rb2D = GetComponent<Rigidbody2D>();
            }
            catch
            {
                Utilities.MyDungeonErrors.RigidBody2DOrBoxCollider2DNotFound(gameObject.name);
            }

            PosX = (int) Mathf.Round(transform.position.x);
            PosY = (int) Mathf.Round(transform.position.y);

            InverseMoveTime = 1f / MoveTime;
        }

        /// <summary>
        /// Checks if the entity can move in the provided direction
        /// </summary>
        /// <param name="xDir">X direction the entity is attempting to move in</param>
        /// <param name="yDir">Y direction the entity is attempting to move in</param>
        /// <param name="hit">Output variable for Raycast collision check</param>
        /// <returns>True if direction is clear for movement or False if entity is currently moving or blocked</returns>
        protected virtual bool Move(int xDir, int yDir, out RaycastHit2D hit)
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(xDir, yDir);

            CheckHit(start, end, out hit);

            if (PosX + xDir < 0 || PosX + xDir > DungeonManager.DungeonGenerationSettings.Columns || PosY + yDir < 0 || PosY + yDir > DungeonManager.DungeonGenerationSettings.Rows)
            {
                return false;
            }

            if (hit || Moving) return false;

            PosX += xDir;
            PosY += yDir;
            StartCoroutine(SmoothMovement(end));
            return true;
        }

        /// <summary>
        /// Checks if entity is blocked by an object on the blocking layer in a desired direction
        /// </summary>
        /// <param name="start">Entity's current position</param>
        /// <param name="end">Entity's desired position</param>
        /// <param name="hit">Output result of the Linecast</param>
        protected virtual void CheckHit(Vector2 start, Vector2 end, out RaycastHit2D hit)
        {
            BoxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, BlockingLayer);
            BoxCollider.enabled = true;
        }

        /// <summary>
        /// Moves entity from their current position to their desired position with a smooth animation
        /// </summary>
        /// <param name="end">Desired position</param>
        /// <returns></returns>
        protected virtual IEnumerator SmoothMovement(Vector3 end)
        {
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            Moving = true;

            while (sqrRemainingDistance > float.Epsilon)
            {
                Vector3 newPosition = Vector3.MoveTowards(Rb2D.position, end, InverseMoveTime * Time.deltaTime);
                Rb2D.MovePosition(newPosition);
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                yield return null;
            }

            yield return new WaitForSeconds(MoveTime);

            Moving = false;
        }

        /// <summary>
        /// Used to ensure a turn does not end early
        /// </summary>
        protected virtual IEnumerator WaitForTurnEnd()
        {
            yield return new WaitForSeconds(MoveTime);
        }

        /// <summary>
        /// Subtracts health from the entity
        /// </summary>
        /// <param name="damage">Amount of health to subtract from current health</param>
        public virtual void LoseHealth(int damage)
        {
            CurHealth -= damage;
        }

        /// <summary>
        /// Adds health to the entity
        /// </summary>
        /// <param name="recover">Amount of health to add to the current health</param>
        public virtual void RecoverHealth(int recover)
        {
            CurHealth += recover;
        }
    }
}