using UnityEngine;
using System.Collections;


public class playerMovementController : MonoBehaviour
{

    public static bool forward;
    public static bool back;
    public static bool left;
    public static bool right;
    public static bool use;
    public static bool jump;
    public static bool lantern;
    public static bool powerLantern;

    public float lookSensitivity;
    public static float xRot;
    public static float yRot;
 
    void Start()
    {

    }

    void Update()
    {
        if (use)
            use = false;
        holdKeyCheck();
        toggleKeyCheck();
        mouseLook();
    }

    void holdKeyCheck()
    {
        //-- KeyDown --//
        if (Input.GetAxis("Vertical") < -0.5)
        {
            forward = true;
        }

        if (Input.GetAxis("Vertical") > 0.5)
        {
            back = true;
        }
    
        if (Input.GetAxis("Horizontal") < -0.2)
        {
            left = true;
        }

        if (Input.GetAxis("Horizontal") > 0.2)
        {
            right = true;
        }
       
      //if (Input.GetKeyDown(KeyCode.W)) //|| (Input.GetAxis("Vertical") < -0.5))
      //  {
      //      forward = true;
      //  }

      //  if (Input.GetKeyDown(KeyCode.S))// || (Input.GetAxis("JoystickLeftStickVertical") > 0.5))
      //  {
      //      back = true;
      //  }

      //  if (Input.GetKeyDown(KeyCode.A)) // || (Input.GetAxis("JoystickLeftStickHorizontal") < -0.5))
      //  {
      //      left = true;
      //  }

      //  if (Input.GetKeyDown(KeyCode.D))// || (Input.GetAxis("JoystickLeftStickHorizontal") > 0.5))
      //  {
      //      right = true;
      //  }

        if (Input.GetButtonDown("Jump")/*Input.GetKeyDown(KeyCode.Space)*/)
        {
            jump = true;
        }

        if (Input.GetButtonDown("Interact"))
        {
            use = true;
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Application.LoadLevel(0);           // Quit to main menu
        //}

        //-- KeyUp --//
        if (Input.GetAxis("Vertical") > -0.5)
        {
            forward = false;
        }

        if (Input.GetAxis("Vertical") < 0.5)
        {
            back = false;
        }

        if (Input.GetAxis("Horizontal") > -0.2)
        {
            left = false;
        }

        if (Input.GetAxis("Horizontal") < 0.2)
        {
            right = false;
        }

       //if (Input.GetKeyUp(KeyCode.W)) //|| (Input.GetAxis("JoystickLeftStickVertical") > -0.5))
       // {
       //     forward = false;
       // }

       // if (Input.GetKeyUp(KeyCode.S)) //|| (Input.GetAxis("JoystickLeftStickVertical") < 0.5))
       // {
       //     back = false;
       // }

       // if (Input.GetKeyUp(KeyCode.A))// || (Input.GetAxis("JoystickLeftStickHorizontal") > -0.5))
       // {
       //     left = false;
       // }

       // if (Input.GetKeyUp(KeyCode.D))// || (Input.GetAxis("JoystickLeftStickHorizontal") < 0.5))
       // {
       //     right = false;
       // }

        if (Input.GetButtonUp("Jump")/*Input.GetKeyUp(KeyCode.Space)*/)
        {
            jump = false;
        }

        if (Input.GetButtonUp("Interact"))
        {
            use = false;
        }
    }

    void toggleKeyCheck()
    {
        if (Input.GetButtonUp("Lantern"))
        {
           if (lantern)
           {
                lantern = false;
           }
           else
          {
               lantern = true;
          }
       }
        if (Input.GetButtonUp("Power Lantern"))
        {
            if (powerLantern)
            {
                powerLantern = false;
            }
            else
            {
                powerLantern = true;
            }
        }
    }
  
    void mouseLook()
    {
        xRot -= Input.GetAxis("Mouse Y") * lookSensitivity;                   //Rotational axis to equal mouse axis
        //float newYRot = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse X");
        //if(Mathf.Abs(mouseY) > 1.5f)
            yRot += mouseY * lookSensitivity;//previousYRot - newYRot;                   //Rotational axis to equal mouse axis
       // previousYRot = newYRot;
      //  Debug.Log(newYRot);
    }

    void OnLevelWasLoaded(int level)
    {
        forward = false;
        back = false;
        left = false;
        right = false;
        use = false;
        jump = false;
    }
}