using System.Collections;
using UnityEngine;

namespace MyDungeon.Demo
{
    public abstract class MovingObject : MonoBehaviour
    {

        public float moveTime = 0.1f;
        public LayerMask blockingLayer;
        public int level = 1;
        public int maxHealth = 10;
        public int strength = 1;
        public string displayName;

        protected int curHealth;

        BoxCollider2D boxCollider;
        Rigidbody2D rb2D;
        float inverseMoveTime;
        protected int posX;
        protected int posY;
        protected bool moving = false;

        // Use this for initialization
        protected virtual void Start()
        {
            posX = (int) Mathf.Round(transform.position.x);
            posY = (int) Mathf.Round(transform.position.y);
            boxCollider = GetComponent<BoxCollider2D>();
            rb2D = GetComponent<Rigidbody2D>();
            inverseMoveTime = 1f / moveTime;
        }

        protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(xDir, yDir);

            CheckHit(start, end, out hit);

            if (posX + xDir < 0 || posX + xDir > GameManager.instance.board.GetLength(0) || posY + yDir < 0 ||
                posY + yDir > GameManager.instance.board.GetLength(1))
            {
                return false;
            }

            if (GameManager.instance.board[posX + xDir, posY + yDir] < GridGenerator.TileType.Wall && !moving)
            {
                GameManager.instance.board[posX, posY] = GridGenerator.TileType.Floor;
                GameManager.instance.board[posX += xDir, posY += yDir] = GridGenerator.TileType.Creature;
                StartCoroutine(SmoothMovement(end));
                return true;
            }

            return false;
        }

        protected void CheckHit(Vector2 start, Vector2 end, out RaycastHit2D hit)
        {
            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, blockingLayer);
            boxCollider.enabled = true;
        }

        protected IEnumerator SmoothMovement(Vector3 end)
        {
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            moving = true;

            while (sqrRemainingDistance > float.Epsilon)
            {
                Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
                rb2D.MovePosition(newPosition);
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                yield return null;
            }

            moving = false;
        }

        protected virtual void AttemptMove<T>(int xDir, int yDir)
            where T : Component
        {
            RaycastHit2D hit;
            bool canMove = Move(xDir, yDir, out hit);

            if (hit.transform == null)
                return;

            T hitComponent = hit.transform.GetComponent<T>();

            if (!canMove && hitComponent != null)
                OnCantMove(hitComponent);
        }

        public virtual void LoseHealth(int damage)
        {
            curHealth -= damage;
            HudManager.instance.AddMessage(displayName + " took " + damage + " damage");
        }

        protected abstract void OnCantMove<T>(T Component)
            where T : Component;
    }
}
