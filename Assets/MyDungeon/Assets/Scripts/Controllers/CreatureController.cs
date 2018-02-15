using System.Collections;
using System.Collections.Generic;
using MyDungeon.Entities;
using MyDungeon.Managers;
using UnityEngine;

namespace MyDungeon.Controllers
{
    /// <summary>
    /// The Creature Controller component tracks all creatures on the map and executes their AI during their turn
    /// </summary>
    public class CreatureController : MonoBehaviour
    {
        /// <summary>
        /// Indicates whether or not it is currently the creatures' turn
        /// </summary>
        protected bool CreaturesMoving;

        /// <summary>
        /// Initializes the creatures list
        /// </summary>
        protected virtual void Awake()
        {
            DungeonManager.Creatures = new List<Creature>();
        }

        /// <summary>
        /// Checks that it is not currently the player's turn then executes the MoveCreatures coroutine
        /// </summary>
        protected virtual void Update()
        {
            if (GameManager.PlayersTurn || CreaturesMoving)
                return;

            StartCoroutine(MoveCreatures());
        }

        /// <summary>
        /// Executes each creature's AI via their MoveCreature method
        /// </summary>
        protected virtual IEnumerator MoveCreatures()
        {
            CreaturesMoving = true;

            yield return new WaitForSeconds(0.25f);

            foreach (Creature creature in DungeonManager.Creatures)
                creature.MoveCreature();

            yield return null;

            CreaturesMoving = false;
            GameManager.PlayersTurn = true;
        }
    }
}
