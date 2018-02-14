using UnityEngine;

namespace MyDungeon
{
    public class MovingOverworldObject : MonoBehaviour
    {
        public LayerMask BlockingLayer;
        protected BoxCollider2D BoxCollider;

        protected virtual void Start()
        {
            BoxCollider = GetComponent<BoxCollider2D>();
        }

        protected virtual void CheckHit(Vector2 start, Vector2 end, out RaycastHit2D hit)
        {
            BoxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, BlockingLayer);
            BoxCollider.enabled = true;
        }
    }
}
