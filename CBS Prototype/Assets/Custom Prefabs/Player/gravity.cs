using UnityEngine;
using System.Collections;

public class gravity : MonoBehaviour
{

    public static bool grounded;
    public static bool onStairs;
    RaycastHit hit;
    public float groundDistance;
    public float stairDistance;

    Vector3 downward;


    // Use this for initialization
    void Start()
    {

        downward.Set(0, -1, 0);

    }

    // Update is called once per frame
    void Update()
    {

        if (Physics.Raycast(transform.position, downward, out hit, groundDistance))
        {
            grounded = true;
        }

        else
        {
            grounded = false;

        }

        if (Physics.Raycast(transform.position, downward, out hit, stairDistance))
        {
            if (hit.transform.tag == "Stairs")
            {
                onStairs = true;
                Debug.Log("ON STAIRS");
            }
            else
            {
                onStairs = false;
            }
        }


        Debug.DrawRay(transform.position, downward * stairDistance, Color.blue);
        Debug.DrawRay(transform.position, downward * groundDistance, Color.red);

    }
}
