using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MyDungeon
{
    public class CreatureController : MonoBehaviour
    {
        protected List<Creature> Creatures;
        protected bool CreaturesMoving;

        // Use this for initialization
        protected virtual void Awake()
        {
            Creatures = new List<Creature>();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (GameManager.PlayersTurn || CreaturesMoving)
                return;

            StartCoroutine(MoveCreatures());
        }

        public virtual void AddCreatureToList(Creature script)
        {
            Creatures.Add(script);
        }

        public virtual void RemoveCreatureFromList(Creature script)
        {
            Creatures.Remove(script);
        }

        protected virtual IEnumerator MoveCreatures()
        {
            CreaturesMoving = true;

            yield return new WaitForSeconds(0.25f);

            foreach (Creature creature in Creatures)
                creature.MoveCreature();

            yield return null;

            CreaturesMoving = false;
            GameManager.PlayersTurn = true;
        }
    }
}
