using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarlJump : MonoBehaviour
{
    [SerializeField]
    private bool isTouchingFloor = true;

    private void FixedUpdate()
    {

    }
    public bool getTouchingFloor()
    {
        return isTouchingFloor;
    }
}
