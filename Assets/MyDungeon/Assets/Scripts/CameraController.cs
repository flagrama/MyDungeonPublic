using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    GameObject player;
    Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        offset = transform.position;
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if(player == null)
            player = GameObject.FindWithTag("Player");
        else
            transform.position = player.transform.position + offset;
    }
}
