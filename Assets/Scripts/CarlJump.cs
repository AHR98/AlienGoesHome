using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarlJump : MonoBehaviour
{
    [SerializeField]
    private bool isTouchingFloor = true;

    //void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("IS JUMPING");
    //    isTouchingFloor = false;

    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.gameObject.CompareTag("Floor"))
    //    {
    //        //Slinky is touching the floor
    //        isTouchingFloor = true;
    //    }
    //    else  //
    //    {
    //        Debug.Log("IS JUMPING");
    //        isTouchingFloor = false;

    //    }
    //}
    //private void FixedUpdate()
    //{
        
    //}
    public bool getTouchingFloor()
    {
        return isTouchingFloor;
    }
}
