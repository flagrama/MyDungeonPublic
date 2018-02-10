using UnityEngine;
using System.Collections;

namespace MyDungeon.Demo
{
    public class MyExitMenu : MyDungeon.ExitMenu
    {
        private bool _collided;
        private GameObject _collision;

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            if (_collided && Mathf.Approximately(transform.position.x, _collision.transform.position.x)
                && Mathf.Approximately(transform.position.y, _collision.transform.position.y))
            {
                _collision.GetComponent<PlayerController>().enabled = false;
                ShowMenu();
                _collided = false;
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                _collided = true;
                _collision = collision.gameObject;
            }
        }
    }
}
