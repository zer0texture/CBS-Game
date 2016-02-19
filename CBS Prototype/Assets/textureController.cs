using UnityEngine;
using System.Collections;

public class textureController : MonoBehaviour {


    public float m_FadeTime = 5.0f;
    private float m_Timer = 0.0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, m_FadeTime);
	}
	
	// Update is called once per frame
	void Update () {

        m_Timer += Time.deltaTime;

        float percent = 1 - m_Timer / m_FadeTime;

        Color color = GetComponent<Renderer>().material.color;
        color.a = percent;
        GetComponent<Renderer>().material.color = color;

        if (m_Timer >= m_FadeTime)
        {
            Destroy(gameObject);
        }
	}

}
