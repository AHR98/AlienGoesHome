using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DoorAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject pressKeyF;
    public Transform PlayerCamera;
    [Header("MaxDistance you can open or close the door.")]
    public float MaxDistance = 3;
    
    private bool opened = false;
    private Animator anim;
    [SerializeField]
    private GameObject niloScript;
    private bool niloFounded = false;

    void Update()
    {
        getNilo();
        Pressed();
    }

    void Pressed()
    {
        RaycastHit doorhit;

        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out doorhit, MaxDistance))
        {

            if (doorhit.transform.CompareTag("Door"))
            {
                pressKeyF.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    anim = doorhit.transform.GetComponent<Animator>();

                    opened = !opened;

                    anim.SetBool("Opened", !opened);
                }
                
               
            }
            else if(doorhit.transform.CompareTag("ExitDoor") && niloFounded) //Encontró a su amigo?
            {
                //Codigo para puerta exit
                pressKeyF.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    anim = doorhit.transform.GetComponent<Animator>();

                    opened = !opened;

                    anim.SetBool("Opened", !opened);
                }
            }


        }
        else
        {
            pressKeyF.SetActive(false);
        }
    }

    
    void getNilo()
    {
        niloFounded = niloScript.GetComponent<NiloController>().followSlinky;
    }
}
