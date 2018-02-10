using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MyDungeon
{
    public class CreatureController : MonoBehaviour
    {
        private List<Creature> _creatures;
        private bool _creaturesMoving;

        // Use this for initialization
        void Awake()
        {
            _creatures = new List<Creature>();
        }

        // Update is called once per frame
        void Update()
        {
            if (MyDungeon.GameManager.PlayersTurn || _creaturesMoving)
                return;

            StartCoroutine(MoveCreatures());
        }

        public void AddCreatureToList(Creature script)
        {
            _creatures.Add(script);
        }

        public void RemoveCreatureFromList(Creature script)
        {
            _creatures.Remove(script);
        }

        private IEnumerator MoveCreatures()
        {
            _creaturesMoving = true;

            yield return new WaitForSeconds(0.25f);

            foreach (Creature creature in _creatures)
                creature.MoveCreature();

            yield return null;

            _creaturesMoving = false;
            MyDungeon.GameManager.PlayersTurn = true;
        }
    }
}
