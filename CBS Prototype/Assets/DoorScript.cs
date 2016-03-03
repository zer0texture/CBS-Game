using UnityEngine;
using System.Collections;

public class DoorScript : buttonScript {

    public int m_ID = -1;
    public bool slidingDoor = false;

    protected override void TriggerAction()
    {
        PlayerInventory.Collect newkey = new PlayerInventory.Collect(m_ID, CollectablesScript.collectType.KEYS);
        if (m_Player == null)
            return;
        PlayerController player = m_Player.GetComponent<PlayerController>();
        if (player)
        {
            if (player.GetInventory().HasObject(newkey))
            {
                openDoor();

                /*
                bool isOpen = GetComponent<Animator>().GetBool("Open");
                GetComponent<Animator>().SetBool("Open", !isOpen);
                */
            }
            else
                Debug.Log("Key not aquired");
        }

    }
    IEnumerator DoorSlide()
    {
        while (transform.position.y > -100)
        {
            Vector3 tempTransform = new Vector3(0f, -0.1f, 0f);
            tempTransform *= Time.deltaTime;
            transform.Translate(0f,-0.1f,0f);
            yield return new WaitForEndOfFrame();
        }

    }

    private void openDoor()
    {
        m_Interractable = false;
        if (slidingDoor)
        {
            StartCoroutine(DoorSlide());
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void TriggerDoor()
    {
        openDoor();
    }
}
