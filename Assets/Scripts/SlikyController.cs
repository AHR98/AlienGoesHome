using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlikyController : MonoBehaviour
{
    private Animator animController;
  
    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Walk-run
        animController.SetFloat("Speed", Input.GetAxis("Vertical"));
        animController.SetFloat("Direction", Input.GetAxis("Horizontal"));

        if(Input.GetAxis("Vertical") > 0.1f 
            || Input.GetAxis("Horizontal") > 0.0f 
            || Input.GetAxis("Horizontal") < 0.0f) //Forward 
        {
            animController.SetBool("Run", true);
        }
        else if (Input.GetAxis("Vertical") < 0.0f) //Back
        {
            animController.SetBool("TurnAround", true);
          //  animController.SetBool("Run", true);
        }
 
        else
        {
            animController.SetBool("TurnAround", false);
            animController.SetBool("Run", false);
        }

        //Special Dance
        if (Input.GetKey(KeyCode.LeftShift) )
            if(Input.GetKeyUp(KeyCode.D))
                animController.SetTrigger("Dance");
        
    }
}
