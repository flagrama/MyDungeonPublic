using UnityEngine;
using System.Collections;

public class MyControlManager : MyDungeon.ControlManager {
	
	// Update is called once per frame
	protected void Update ()
	{
	    MenuHorizontal = Input.GetAxisRaw("Horizontal");
	    MenuVertical = Input.GetAxisRaw("Vertical");
    }
}
