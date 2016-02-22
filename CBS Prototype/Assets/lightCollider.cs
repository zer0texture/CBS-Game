using UnityEngine;
using System.Collections;

public class lightCollider : MonoBehaviour
{

    float lanternSize;
    Vector3 lanternScale;
    public Transform player;
    public Light lantern;
    Vector3 snakePos;
    float detectionDist = 10;
    public LayerMask playerMask;
    Vector3 lanternOffset;

    // Use this for initialization
    void Start()
    {

    }


    void OnTriggerStay(Collider other)
    {
        //print("Hit");
        if (other.gameObject.tag == "Snake")
        {
            //print("snake detected");
            snakePos = ((lantern.transform.position + lantern.transform.forward * 3) - (other.transform.position)).normalized;
            RaycastHit hit;
            Ray ray = new Ray(lantern.transform.position + lantern.transform.forward * 3, -snakePos);
            if (Physics.Raycast(ray, out hit))
            {
                 //print(hit.transform.tag);
                 //print(hit.transform.name);
                if (hit.transform.tag == "Snake")
                {
                    other.gameObject.GetComponent<MoveTo>().hitByPowerLantern();
                }
            }
            Debug.DrawRay(lantern.transform.position, -snakePos * detectionDist, Color.cyan);
        }

        else
        {

        }
    }

}