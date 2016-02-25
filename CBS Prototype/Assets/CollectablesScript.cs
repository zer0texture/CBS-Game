using UnityEngine;
using System.Collections;

public class CollectablesScript : buttonScript
{

    public enum collectType { KEYS, TROPHIES, INFO, END_ITEM}
    public LevelType m_TunnelTypeToLoad;

    public int m_ID = -1;
    public float m_OffsetFromCamera = 1.2f;
    public collectType m_Type = collectType.INFO;

    float m_WorkingTimescale;
    Vector3 m_PreviousMouseRot;
    float m_IgnoreTimeScale = 0.00001f;

    public bool m_SkipInspecting = false;

    enum InspectorState
    {
        NORMAL,
        INSPECTING
    }

    InspectorState m_State = InspectorState.NORMAL;

    void Start()
    {
        m_WorkingTimescale = Time.timeScale;
        m_PreviousMouseRot = new Vector3(playerMovementController.xRot, playerMovementController.yRot, 0.0f);
    }

    void Update()
    {
        if (m_State == InspectorState.INSPECTING)
        {
            Vector3 currentRot = transform.rotation.eulerAngles;
            Vector3 newRot = new Vector3(playerMovementController.xRot - m_PreviousMouseRot.x, playerMovementController.yRot - m_PreviousMouseRot.y, 0.0f);
            transform.Rotate(Camera.main.transform.right, newRot.x * m_WorkingTimescale);
            transform.Rotate(Camera.main.transform.up, newRot.y * m_WorkingTimescale);
            m_PreviousMouseRot = new Vector3(playerMovementController.xRot, playerMovementController.yRot, 0.0f);

            if (m_WorkingTimescale != Time.timeScale && Time.timeScale != m_IgnoreTimeScale)
                m_WorkingTimescale = Time.timeScale;
            Time.timeScale = m_IgnoreTimeScale;

            if (playerMovementController.use && m_Interractable)
            {
                TriggerAction();
            }

            if (m_Interractable == false)
                m_Interractable = true;
        }
        else
        {
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() && m_State == InspectorState.NORMAL)
        {
            m_Player = null;
        }
    }

    protected override void TriggerAction()
    {
        if (m_State == InspectorState.INSPECTING)
        {
             EndPickup();        
        }
        else
        {
            if (!m_SkipInspecting)
            {
                CloseUp();
            }
            else
            {
                SaveItem();
                EndPickup();  
            }
            saveOnAction();
        }
    }

    protected void CloseUp()
    {
        transform.parent = m_Player.GetComponent<PlayerController>().Screen.transform.GetChild(1);
        transform.localPosition = Vector3.forward * m_OffsetFromCamera;
        m_Interractable = false;

        m_State = InspectorState.INSPECTING;
        SaveItem();

    }

    protected void EndPickup()
    {
        Time.timeScale = 1.0f;
        Destroy(gameObject);

        if(m_Type == collectType.END_ITEM)
        {
            //Application.LoadLevel("Tunnel");
            LevelLoader.m_Instance.LoadLevel(-1, "Tunnel");
            TunnelSpawner.levelType = m_TunnelTypeToLoad;
        }
    }

    protected void SaveItem()
    {
        Debug.Log(m_Player.name);
        PlayerController player = m_Player.GetComponent<PlayerController>();
        PlayerInventory.Collect newcollect = new PlayerInventory.Collect(m_ID, m_Type);

        player.GetInventory();
        player.GetInventory().CollectObject(newcollect);
    }

}