using UnityEngine;
using System.Collections;

namespace MyDungeon.Demo
{
    public class MyCreature : MyDungeon.Creature
    {
        private Transform _target;

        protected override void Start()
        {
            base.Start();
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        public override void MoveCreature()
        {
            int xDir = 0;
            int yDir = 0;

            if (Mathf.Abs(_target.position.x - transform.position.x) < Mathf.Epsilon)
                yDir = _target.position.y > transform.position.y ? 1 : -1;
            else
                xDir = _target.position.x > transform.position.x ? 1 : -1;

            AttemptMove<Player>(xDir, yDir);
        }
    }
}
