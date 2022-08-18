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
        //Special Dance
        if (Input.GetKey(KeyCode.LeftShift))
            if (Input.GetKeyUp(KeyCode.Z))
                animController.SetTrigger("Dance");

        //Walk-run
        animController.SetFloat("Speed", Input.GetAxis("Vertical"));
        animController.SetFloat("Direction", Input.GetAxis("Horizontal"));

        if(Input.GetAxis("Vertical") > 0.1f 
            || (Input.GetAxis("Vertical") > 0.5f && Input.GetAxis("Horizontal") > 0.5f)
            || (Input.GetAxis("Vertical") > 0.5f && Input.GetAxis("Horizontal") < -0.5f) ) 
        {
            animController.SetBool("Run", true);
        }
        else if (Input.GetAxis("Vertical") < 0.0f) //Back
        {
            animController.SetBool("TurnAroundRight", true);
          //  animController.SetBool("Run", true);
        }
        else if(Input.GetAxis("Horizontal") > 0.0f && Input.GetAxis("Vertical") == 0.0f)
        {
            animController.SetBool("TurnAroundRight", true);
        }
        else if(Input.GetAxis("Horizontal") < 0.0f && Input.GetAxis("Vertical") == 0.0f)
        {
            animController.SetBool("TurnAroundLeft", true);

        }
        else
        {
            animController.SetBool("TurnAroundRight", false);
            animController.SetBool("TurnAroundLeft", false);
            animController.SetBool("Run", false);
        }

        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("IS A COLLIDER");

        if (collision.collider.CompareTag("Door"))
        {
            Debug.Log("IS A DOOR");
        }
    }
}
