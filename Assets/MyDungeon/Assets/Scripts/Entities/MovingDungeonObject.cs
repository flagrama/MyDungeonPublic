using System.Collections;
using MyDungeon.Utilities;
using UnityEngine;

namespace MyDungeon
{
    public abstract class MovingDungeonObject : MonoBehaviour
    {
        public LayerMask BlockingLayer;
        public int Level = 1;
        public int Strength = 1;
        public int MaxHealth = 10;
        public float MoveTime = 0.25f;
        public string DisplayName;

        protected GameObject DungeonManager;
        protected GridGenerator DungeonMap;
        protected BoxCollider2D BoxCollider;
        protected Rigidbody2D Rb2D;
        protected Animator Animator;
        protected int CurHealth;
        protected int PosX;
        protected int PosY;
        protected float InverseMoveTime;
        protected bool Moving;

        // Use this for initialization
        protected virtual void Start()
        {
            DungeonManager = GameObject.FindGameObjectWithTag("DungeonManager");

            try
            {
                DungeonMap = DungeonManager.GetComponent<GridGenerator>();
            }
            catch
            {
                if(DungeonManager != null)
                    Utilities.MyDungeonErrors.GridGeneratorOnDungeonManagerNotFound(DungeonManager.name);
                else
                    Utilities.MyDungeonErrors.DungeonManagerNotFound();
            }

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

        protected void SetAnimation(int xDir, int yDir)
        {
            Animator.SetFloat("MoveX", xDir);
            Animator.SetFloat("MoveY", yDir);
        }

        protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(xDir, yDir);

            CheckHit(start, end, out hit);

            if (PosX + xDir < 0 || PosX + xDir > DungeonMap.Columns || PosY + yDir < 0 || PosY + yDir > DungeonMap.Rows)
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
            BoxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, BlockingLayer);
            BoxCollider.enabled = true;
        }

        protected IEnumerator SmoothMovement(Vector3 end)
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
        }

        public virtual void RecoverHealth(int recover)
        {
            CurHealth += recover;
        }

        protected abstract void OnCantMove<T>(T component)
            where T : Component;
    }
}