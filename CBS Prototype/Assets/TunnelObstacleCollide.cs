﻿ using UnityEngine;
using System.Collections;

public class TunnelObstacleCollide : MonoBehaviour {

    public GameObject particlePrefab;
    public GameObject SnakeChase;
    Quaternion nullQuart;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        // Move snake forward?
        Instantiate(particlePrefab, gameObject.transform.position, nullQuart);
        Destroy(gameObject);
    }
}
