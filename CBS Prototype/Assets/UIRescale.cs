using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIRescale : MonoBehaviour {


    public float m_PercentHeight;
    public float m_PercentWidth;
    public bool m_KeepAspect;
    public float m_PercentOffsetHorrizontal;
    public float m_PercentOffsetVertical;

	// Use this for initialization
	void Start () {

        float scale = 0;

        scale = transform.GetComponentInParent<CanvasScaler>().scaleFactor;

        Scale(scale);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Scale(float scale)
    {


        RectTransform rect = GetComponent<RectTransform>();
        float orrHeight = rect.rect.height;
        float orrWidth = rect.rect.width;

        float height = Screen.height / scale * m_PercentHeight;
        float width = Screen.width / scale * m_PercentWidth;

        float heightScale = height / orrHeight;
        float widthScale = width / orrWidth;

        
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.rect.width * widthScale);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.rect.height * (m_KeepAspect ? widthScale : heightScale));

        if(m_KeepAspect)
        {

        }
        rect.anchoredPosition = new Vector2((-(Screen.width / 2) + (m_PercentOffsetHorrizontal * Screen.width)) / scale + rect.rect.width /2, 
                                            ((Screen.height / 2) - (m_PercentOffsetVertical * Screen.height)) / scale -rect.rect.height /2);


        foreach (RectTransform trans in GetComponentsInChildren<RectTransform>())
        {
            if (trans == rect)
                continue;
            heightScale = height / trans.rect.height; ;
            widthScale = width / trans.rect.width;

            trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, trans.rect.width * widthScale);
            trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, trans.rect.height * (m_KeepAspect ? widthScale : heightScale));

            Vector2 oldpos = trans.anchoredPosition;
            Vector2 newpos = oldpos;
            newpos = new Vector2(oldpos.x * widthScale, oldpos.y * (m_KeepAspect ? widthScale : heightScale));
            trans.anchoredPosition = newpos;
        }
    }        
}
