using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour {

    public GameObject triggerOBJ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Trigger()
    {
        if(triggerOBJ == null)
        {
            Debug.Log("No trigger object, you dummy!");
            return;
        }
        DoorScript door = triggerOBJ.GetComponent<DoorScript>();
        if(door != null)
        {
            door.TriggerDoor();
        }
    }
}
