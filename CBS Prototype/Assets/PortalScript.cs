using UnityEngine;
using System.Collections;

public class PortalScript : buttonScript {



	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            // m_Player = other.gameObject;
            LevelLoader.m_Instance.LoadLevel(1, "", true);
        }
    }
}
