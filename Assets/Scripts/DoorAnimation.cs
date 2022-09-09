using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject pressKeyF;
    public Transform PlayerCamera;
    [Header("MaxDistance you can open or close the door.")]
    public float MaxDistance = 3;

    private bool opened = false;
    private Animator anim;



    void Update()
    {
        Pressed();
    }

    void Pressed()
    {
        //This will name the Raycasthit and came information of which object the raycast hit.
        RaycastHit doorhit;

        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out doorhit, MaxDistance))
        {

            // if raycast hits, then it checks if it hit an object with the tag Door.
            if (doorhit.transform.CompareTag("Door"))
            {
                pressKeyF.SetActive(true);
                //This will tell if the player press F on the Keyboard. P.S. You can change the key if you want.
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //This line will get the Animator from  Parent of the door that was hit by the raycast.
                    anim = doorhit.transform.GetComponent<Animator>();

                    //This will set the bool the opposite of what it is.
                    opened = !opened;

                    //This line will set the bool true so it will play the animation.
                    anim.SetBool("Opened", !opened);
                }
                
               
            }
            else if(doorhit.transform.CompareTag("ExitDoor"))
            {
                //Codigo para puerta exit
            }
            else
            {
                pressKeyF.SetActive(false);

            }
        }
    }
}
