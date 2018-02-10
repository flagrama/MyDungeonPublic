using System.Collections;
using UnityEngine;

namespace MyDungeon
{
    public abstract class MovingObject : MonoBehaviour
    {
        private BoxCollider2D _boxCollider;
        private float _inverseMoveTime;
        private Rigidbody2D _rb2D;
        public LayerMask BlockingLayer;
        protected int CurHealth;
        public string DisplayName;
        public int Level = 1;
        public int MaxHealth = 10;
        public float MoveTime = 0.25f;
        protected bool Moving;
        protected int PosX;
        protected int PosY;
        protected GameObject DungeonManager;
        public int Strength = 1;

        // Use this for initialization
        protected virtual void Start()
        {
            DungeonManager = GameObject.FindGameObjectWithTag("DungeonManager");
            PosX = (int) Mathf.Round(transform.position.x);
            PosY = (int) Mathf.Round(transform.position.y);
            _boxCollider = GetComponent<BoxCollider2D>();
            _rb2D = GetComponent<Rigidbody2D>();
            _inverseMoveTime = 1f / MoveTime;
        }

        protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(xDir, yDir);

            CheckHit(start, end, out hit);


            if (PosX + xDir < 0 || PosX + xDir > DungeonManager.GetComponent<GridGenerator>().Columns || PosY + yDir < 0 ||
                PosY + yDir > DungeonManager.GetComponent<GridGenerator>().Rows)
            {
                return false;
            }

            if (hit || Moving) return false;

            PosX += xDir;
            PosY += yDir;
            StartCoroutine(SmoothMovement(end));
            return true;
        }

        protected void CheckHit(Vector2 start, Vector2 end, out RaycastHit2D hit)
        {
            _boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, BlockingLayer);
            _boxCollider.enabled = true;
        }

        protected IEnumerator SmoothMovement(Vector3 end)
        {
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            Moving = true;

            while (sqrRemainingDistance > float.Epsilon)
            {
                Vector3 newPosition = Vector3.MoveTowards(_rb2D.position, end, _inverseMoveTime * Time.deltaTime);
                _rb2D.MovePosition(newPosition);
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                yield return null;
            }

            Moving = false;
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
            CurHealth -= damage;
            GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<MessageLogDisplay>().AddMessage(DisplayName + " took " + damage + " damage");
        }

        protected abstract void OnCantMove<T>(T component)
            where T : Component;
    }
}