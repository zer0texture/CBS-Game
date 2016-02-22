using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour
{

    public static Rigidbody rb;
    public Animator anim;
    public static float speed;
    public float stairSpeed;
    public float maxSpeed;
    public float slowDown;
    public float jumpForce;
    public static float noise;
    public float stairGrav;


    // Use this for initialization
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.drag = slowDown;
        speed = 20;
        noise = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement();
        jump();
        mouseLook();
        
        stairGravity();


       // anim.speed = rb.velocity.magnitude / 6;
    }

    void movement()
    {
        noise = 0;
        if (playerMovementController.forward)
        {
            if (gravity.onStairs)
            {
                rb.AddForce(transform.forward.normalized * stairSpeed, ForceMode.Force);
            }
            else
            {
                rb.AddForce(transform.forward.normalized * speed, ForceMode.Force);
            }

            noise = (transform.forward.magnitude * speed);
        }

        if (playerMovementController.back)
        {
            if (gravity.onStairs)
            {
                rb.AddForce(-transform.forward.normalized * stairSpeed, ForceMode.Force);
            }
            else
            {
                rb.AddForce(-transform.forward.normalized * speed, ForceMode.Force);
            }

            noise = transform.forward.magnitude * speed;
        }

        if (playerMovementController.right)
        {
            if (gravity.onStairs)
            {
                rb.AddForce(transform.right.normalized * stairSpeed, ForceMode.Force);
            }
            else
            {
                rb.AddForce(transform.right.normalized * speed, ForceMode.Force);
            }

            noise = transform.right.magnitude * speed;
        }

        if (playerMovementController.left)
        {
            if (gravity.onStairs)
            {
                rb.AddForce(-transform.right.normalized * stairSpeed, ForceMode.Force);
            }
            else
            {
                rb.AddForce(-transform.right.normalized * speed, ForceMode.Force);
            }

            noise = transform.right.magnitude * speed;
        }


        // Max speed to stop Vic flying through the air 
        if (rb.velocity.magnitude >= maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

    }

    void jump()
    {
        if (playerMovementController.jump && gravity.grounded)
        {
            rb.AddRelativeForce(transform.up.normalized * jumpForce, ForceMode.Force);
        }


    }

    void mouseLook()
    {
       transform.rotation = Quaternion.Euler(0, playerMovementController.yRot, 0);
    }


    void stairGravity()
    {
        if (!gravity.grounded && gravity.onStairs)
        {
            rb.AddForce(-transform.up.normalized * stairGrav, ForceMode.Force);
        }
    }
    void OnTriggerStay()
    {
        // Used for lightCollider due to unity bug
        // https://issuetracker.unity3d.com/issues/triggercollider-ontriggerstay-is-not-called-for-child-objects
        // FUCKY AS FUCK
    }
}
