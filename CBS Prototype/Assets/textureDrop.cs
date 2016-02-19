using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class textureDrop : MonoBehaviour
{
    public enum TextureType { FOOTPRINT, HANDPRINT, BLOOD }
    public TextureType m_texType = TextureType.FOOTPRINT;

    public GameObject m_footprintPrefab;
    //public List<GameObject> m_fpList = new List<GameObject>();
    public GameObject m_bloodPrefab;

    public bool m_RightFootPrint = true;
    int m_footprintIndex = 0;

    bool timerStarted;
    float startTime;
    float currentTime;

    Vector3 rightFoot = new Vector3(-1, 0, 0);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (PlayerSpawner.playerDied())
        {

        }
        //if (Hand on wall)
        //{
            //m_textType = TextureType.HANDPRINT
          // dropTexture();
       // }

            //Always drop a footprint
            m_texType = TextureType.FOOTPRINT;
            dropTexture();
    }

    void dropTexture()
    {
        if(Time.timeScale < 0.1f)
        {
            return;
        }
        switch (m_texType)
        {
            case (TextureType.BLOOD):

                GameObject bloodSplat;

                //On 'Death' trigger, drop blood

                RaycastHit myRay;
                if (Physics.Raycast(transform.position, Vector3.down, out myRay,100))
                {
                    bloodSplat = Instantiate(m_bloodPrefab, myRay.point , transform.rotation) as GameObject;
                }


                break;
            case (TextureType.HANDPRINT):
                break;
            case (TextureType.FOOTPRINT):
                if (gravity.grounded)
                {
                    if (Physics.Raycast(transform.position, Vector3.down, out myRay, 100))
                    {
                        GameObject newFootprint;
                        //newFootprint.transform.Rotate(Vector3.up, 180);
                        if (transform.GetComponent<customTimer>().ourTimer(0.5f))
                        {
                            if (m_RightFootPrint)
                            {
                                Debug.Log("Right Print");
                                //newFootprint.transform.Rotate(Vector3.up, 180);//TEMP FIX FOR ROTATED TEXTURES
                                newFootprint = Instantiate(m_footprintPrefab, myRay.point, transform.rotation) as GameObject;
                                newFootprint.transform.Rotate(Vector3.up, 180);
                                
                                Vector3 newScale = transform.localScale;
                                newScale.x = -1f;
                                newScale.y = 0.001f;



                                newFootprint.transform.localScale = newScale;

                            }
                            else
                            {
                                Debug.Log("Left Print");
                                //transform.Rotate(Vector3.up, 180);  //TEMP FIX FOR ROTATED TEXTURES                           
                                newFootprint = Instantiate(m_footprintPrefab, myRay.point, transform.rotation) as GameObject;
                                newFootprint.transform.Rotate(Vector3.up, 180);


                            }
                            newFootprint.name = "footprint " + m_footprintIndex;
                            m_RightFootPrint = (!m_RightFootPrint);
                            print(m_RightFootPrint);
                            newFootprint.transform.parent = myRay.transform;
                            m_footprintIndex++;
                        }
                    }
                }
                break;
            default:
                break;
            //IF NOT MOVING, SET 'm_RightFootPrint' TO TRUE

        }
    }
    public bool ourTimer(int inputTime)
    {
        if (!timerStarted)
        {
            startTime = Time.time;
            timerStarted = true;
            // print("timer started");
        }

        if (timerStarted)
        {
            currentTime = Time.time;
            if ((startTime + inputTime) <= currentTime)
            {
                //    print("timer done");
                timerStarted = false;
                return true;

            }
        }
        return false;
    }


    public void playerHasDied()
    {
        m_texType = TextureType.BLOOD;
        dropTexture();
    }
}
