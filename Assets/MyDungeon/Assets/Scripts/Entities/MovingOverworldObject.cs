using UnityEngine;
using System.Collections;

namespace MyDungeon
{
    public class MovingOverworldObject : MonoBehaviour
    {
        public LayerMask BlockingLayer;
        protected BoxCollider2D BoxCollider;
        protected Rigidbody2D Rb2D;
        protected Animator Animator;

        protected virtual void Start()
        {
            Animator = GetComponent<Animator>();
        }

        protected void SetAnimation(int xDir, int yDir)
        {
            Animator.SetFloat("MoveX", xDir);
            Animator.SetFloat("MoveY", yDir);
        }

        protected void CheckHit(Vector2 start, Vector2 end, out RaycastHit2D hit)
        {
            BoxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, BlockingLayer);
            BoxCollider.enabled = true;
        }
    }
}
