using UnityEngine;
using System.Collections;

public class WallHandIKController : MonoBehaviour
{

    protected Animator animator;
    public GameObject m_RightHandTarget;
    public GameObject m_LeftHandTarget;
    //public bool m_HandOnWall;
    public float m_HandLerpTime = 2.0f;
    public float m_HandLerpResetDist = 2.0f;

    public float m_ViewDist = 5.0f;

    private float m_HandTimerRight = 0.0f;
    private float m_HandTimerLeft = 0.0f;
    private Vector3 m_PreviousRightHandTargetPos;
    private Vector3 m_PreviousLeftHandTargetPos;

    public float m_RotateLerpTime = 1.5f;
    private float m_RotateTimer = 0.0f;

    bool m_Forward = playerMovementController.forward;
    bool m_Back = playerMovementController.back;
    bool m_Left = playerMovementController.left;
    bool m_Right = playerMovementController.right;

    // Use this for initialization
    void Start()
    {
        //m_HandOnWall = false;
        animator = GetComponent<Animator>();
        animator.SetLayerWeight(1, 1);
        animator.SetLayerWeight(2, 1);
        m_PreviousRightHandTargetPos = m_RightHandTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // for (and if) when we will have hand animation
        //animator.SetBool("RightHandWall", m_HandOnWall);

        bool forward = playerMovementController.forward;
        bool back = playerMovementController.back;
        bool left = playerMovementController.left;
        bool right = playerMovementController.right;


        m_RotateTimer += Time.deltaTime;

        if (forward || back)
        {
            float direction = 1;
            if (forward)
            {
                animator.SetFloat("Forward Innertia", 1.0f);
            }
            else
            {
                animator.SetFloat("Forward Innertia", - 1.0f);
                direction = -1;
            }
            if (left || right)
            {
                if (left)
                {
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0.0f, -45.0f * direction, 0.0f), m_RotateTimer / m_RotateLerpTime);
                    animator.SetFloat("Side Innertia", -0.5f);
                }
                else
                {
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0.0f, 45.0f * direction, 0.0f), m_RotateTimer / m_RotateLerpTime);
                    animator.SetFloat("Side Innertia", 0.5f);
                }
            }
            else
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), m_RotateTimer / m_RotateLerpTime);
                animator.SetFloat("Forward Innertia", 1.0f);
                animator.SetFloat("Side Innertia", 0.0f);
            }

        }
        else if (left || right)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), m_RotateTimer / m_RotateLerpTime);
            animator.SetFloat("Forward Innertia", 0.0f);
            if (left)
            {
                animator.SetFloat("Side Innertia", -1.0f);
            }
            else
            {
                animator.SetFloat("Side Innertia", 1.0f);
            }
        }
        else
        {
            Debug.Log("MOVING NOT");
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), m_RotateTimer / m_RotateLerpTime);
            animator.SetFloat("Forward Innertia", 0.0f);
            animator.SetFloat("Side Innertia", 0.0f);
        }

        if (m_Forward != forward || left != m_Left || m_Right != right || m_Back != back)
        {
            m_RotateTimer = 0.0f;
            m_Forward = forward;
            m_Back = back;
            m_Left = left;
            m_Right = right;
        }

        if (gravity.grounded || gravity.onStairs)
        {
            animator.SetBool("TransFromAny", true);
            animator.SetFloat("Vertical Innertia", 0.0f);
        }
        else
        {
            //animator.SetBool("TransFromAny", false);
            animator.SetFloat("Vertical Innertia", 1.0f);
        }
    }

    void DisableTransFromAny()
    {
        animator.SetBool("TransFromAny", false);
    }


    void OnAnimatorIK(int layerIndex)
    {
        Debug.Log("checking IK");

        ///
        /// RIGHT HAND
        ///
        Vector3 handPos1 = Vector3.zero;
        Vector3 handPos2 = Vector3.zero;
        float weight1 = 0.0f;
        float weight2 = 0.0f;

        RaycastHit hit;
        Ray ray = new Ray(transform.position + transform.up * 3, transform.right * 1.5f);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        if(Physics.Raycast(ray, out hit, m_ViewDist))
        {
            if (!hit.collider.isTrigger)
            {
                handPos1 = hit.point;
                weight1 = (m_ViewDist - hit.distance) / m_ViewDist;
            }
        }

        ray = new Ray(transform.position + transform.up * 3, transform.forward * 1.5f + transform.right * 0.4f);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        if (Physics.Raycast(ray, out hit, m_ViewDist))
        {
            if (!hit.collider.isTrigger)
            {
                handPos2 = hit.point;
                weight2 = (m_ViewDist - hit.distance) / m_ViewDist;
            }
        }

        float finalWeight = Mathf.Max(weight1, weight2);
        Vector3 finalPos = Vector3.zero;
        if (weight1 > weight2)
            finalPos = handPos1;
        else
            finalPos = handPos2;
        if (finalWeight == 0 || m_HandTimerRight >= m_HandLerpTime || Vector3.Distance(m_PreviousRightHandTargetPos, finalPos) >= m_HandLerpResetDist)
        {
            m_HandTimerRight = 0.0f;
        }

        m_RightHandTarget.transform.position = Vector3.Lerp(m_RightHandTarget.transform.position, finalPos, m_HandTimerRight / m_HandLerpTime);
        m_HandTimerRight += Time.deltaTime;

        m_PreviousRightHandTargetPos = finalPos;



        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, finalWeight);        
        animator.SetIKPosition(AvatarIKGoal.RightHand, m_RightHandTarget.transform.position);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, finalWeight);
        Vector3 rot = m_RightHandTarget.transform.rotation.eulerAngles - Vector3.right * 90;
        Quaternion newrot = Quaternion.Euler(rot.x, rot.y, rot.z);
        animator.SetIKRotation(AvatarIKGoal.RightHand, newrot);


        ///
        /// LEFT HAND
        ///
        handPos1 = Vector3.zero;
        handPos2 = Vector3.zero;
        weight1 = 0.0f;
        weight2 = 0.0f;

        ray = new Ray(transform.position + transform.up * 3, transform.right * (-1.5f ));
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        if (Physics.Raycast(ray, out hit, m_ViewDist))
        {
            if (!hit.collider.isTrigger)
            {
                handPos1 = hit.point;
                weight1 = (m_ViewDist - hit.distance) / m_ViewDist;
            }
        }

        ray = new Ray(transform.position + transform.up * 3, transform.forward * 1.5f - transform.right * 0.4f);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        if (Physics.Raycast(ray, out hit, m_ViewDist))
        {
            if (!hit.collider.isTrigger)
            {
                handPos2 = hit.point;
                weight2 = (m_ViewDist - hit.distance) / m_ViewDist;
            }
        }

        finalWeight = Mathf.Max(weight1, weight2);
        finalPos = Vector3.zero;
        if (weight1 > weight2)
            finalPos = handPos1;
        else
            finalPos = handPos2;

        if (finalWeight == 0 || m_HandTimerLeft >= m_HandLerpTime || Vector3.Distance(m_PreviousLeftHandTargetPos, finalPos) >= m_HandLerpResetDist)
        {
            m_HandTimerLeft = 0.0f;
        }

        m_LeftHandTarget.transform.position = Vector3.Lerp(m_LeftHandTarget.transform.position, finalPos, m_HandTimerLeft / m_HandLerpTime);
        m_HandTimerLeft += Time.deltaTime;

        m_PreviousLeftHandTargetPos = finalPos;



        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, finalWeight);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, m_LeftHandTarget.transform.position);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, finalWeight);
        rot = m_LeftHandTarget.transform.rotation.eulerAngles - Vector3.right * 90;
        newrot = Quaternion.Euler(rot.x, rot.y, rot.z);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, newrot);

    }
}
