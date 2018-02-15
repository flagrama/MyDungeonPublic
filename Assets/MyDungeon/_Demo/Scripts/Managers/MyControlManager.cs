using MyDungeon.Managers;
using UnityEngine;

namespace MyDungeon._Demo.Managers
{
    public class MyControlManager : ControlManager {
	
        // Update is called once per frame
        protected void Update ()
        {
            MenuHorizontal = Input.GetAxisRaw("Horizontal");
            MenuVertical = Input.GetAxisRaw("Vertical");
        }
    }
}
