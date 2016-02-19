using UnityEngine;
using System.Collections;

public class BloodController : MonoBehaviour {



	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.mainTexture = Resources.Load("DropTextures/BloodTexts/" + Random.Range(1,4)) as Texture;

         //GetComponent<Renderer>().material.SetTexture("_MainTex", )
//("_MainTex", textures[Random.Range(0, textures.Length)]);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
