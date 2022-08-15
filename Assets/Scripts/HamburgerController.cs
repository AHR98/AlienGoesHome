using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamburgerController : MonoBehaviour
{
    Ray rayHamburger;
    RaycastHit raycastHitHamburger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rayHamburger = new Ray(transform.position, transform.TransformDirection(Vector3.back));
        if (Physics.Raycast(rayHamburger, out raycastHitHamburger, 100))
        {
            Debug.DrawLine(rayHamburger.origin, raycastHitHamburger.point, Color.red);

           
        }
        
    }
}
