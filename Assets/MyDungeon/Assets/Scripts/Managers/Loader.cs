using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject gameManager;
    public GameObject soundManager;
    public GameObject hudManager;
    public GameObject playerManager;
    public GameObject menuManager;
    
	void Awake ()
    {

        if (SoundManager.instance == null)
        {
            Instantiate(soundManager);
        }

        if (HudManager.instance == null)
        {
            Instantiate(hudManager);
        }

        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        if(PlayerManager.instance == null)
        {
            Instantiate(playerManager);
        }

        if (MenuManager.instance == null)
        {
            Instantiate(menuManager);
        }
	}
}
