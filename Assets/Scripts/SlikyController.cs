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

        //Special Dance
        if(Input.GetKey(KeyCode.LeftShift) )
            if(Input.GetKeyUp(KeyCode.D))
                animController.SetTrigger("Dance");
        
    }
}
