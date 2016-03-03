using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextMessageHandler : MonoBehaviour {

    float m_DialogTimer;

    float m_DialogExpire;

    static TextMessageHandler m_Instance = null;

    public static TextMessageHandler GetMessageHandler()
    {
        return m_Instance;
    }

    public void Dialog(string message, float time)
    {
        transform.GetChild(1).GetComponent<Text>().text = message;
        m_DialogExpire = time;
        m_DialogTimer = 0;
    }


    // Use this for initialization
    void Start () {

        transform.GetChild(1).GetComponent<Text>().text = "";
        m_Instance = this;

    }
	
	// Update is called once per frame
	void Update () {

        m_DialogTimer += Time.deltaTime;

        if (m_DialogTimer >= m_DialogExpire)
        {
            transform.GetChild(1).GetComponent<Text>().text = "";
        }

    }
}
