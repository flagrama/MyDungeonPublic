using UnityEngine;

namespace MyDungeon.Entities
{
    /// <summary>
    /// Base class for entities that move in the overworld
    /// </summary>
    public class MovingOverworldObject : MonoBehaviour
    {
        /// <summary>
        /// Layer used in collision checks to block movement
        /// </summary>
        public LayerMask BlockingLayer;
        protected BoxCollider2D BoxCollider;

        /// <summary>
        /// Initializes the entity's BoxCollider2D
        /// </summary>
        protected virtual void Start()
        {
            BoxCollider = GetComponent<BoxCollider2D>();
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
    }
}
