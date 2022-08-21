using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlikyController : MonoBehaviour
{
    private Animator animController;
    private float horizontalDirection;
    private float verticalDirection;
    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalDirection = Input.GetAxis("Horizontal");
        verticalDirection = Input.GetAxis("Vertical");
        //Special Dance
        if (Input.GetKey(KeyCode.LeftShift))
            if (Input.GetKeyUp(KeyCode.Z))
                animController.SetTrigger("Dance");



        //Walk-run
        animController.SetFloat("Speed", verticalDirection);
        animController.SetFloat("Direction", horizontalDirection);
        if (verticalDirection != 0.0f || horizontalDirection != 0.0f)
        {
            animController.SetBool("Run", true);
        }
        else
        {
            animController.SetBool("Run", false);
        }

        //if (verticalDirection > 0.1f
        //    || (verticalDirection > 0.5f && horizontalDirection > 0.5f
        //    || verticalDirection > 0.5f && horizontalDirection < -0.5f))
        //{
        //    animController.SetBool("Run", true);
        //}
        //else if (verticalDirection < 0.0f) //Back
        //{
        //    animController.SetBool("TurnAroundRight", true);
        //    //  animController.SetBool("Run", true);
        //}
        //else if (horizontalDirection > 0.0f && verticalDirection == 0.0f)
        //{
        //    animController.SetBool("TurnAroundRight", true);
        //}
        //else if (horizontalDirection < 0.0f && verticalDirection == 0.0f)
        //{
        //    animController.SetBool("TurnAroundLeft", true);

        //}
        //else
        //{
        //    animController.SetBool("TurnAroundRight", false);
        //    animController.SetBool("TurnAroundLeft", false);
        //    animController.SetBool("Run", false);
        //}



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
