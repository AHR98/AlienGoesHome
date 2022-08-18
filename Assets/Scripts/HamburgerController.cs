using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HamburgerController : MonoBehaviour
{
    Animator hamHamburgerAnimator;
    Ray rayHamburger;
    RaycastHit raycastHitHamburger;
    Animator playerAnimator;
    public float lookRadius = 5f;
    Transform target;
    NavMeshAgent agentHam;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.playerSlinky.transform;
        agentHam = GetComponent<NavMeshAgent>();
        hamHamburgerAnimator = GetComponent<Animator>();
        playerAnimator  = PlayerManager.instance.playerSlinky.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
     //   rayHamburger = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if(distance <= lookRadius)
        {
            hamHamburgerAnimator.SetBool("Chase", true);
            agentHam.SetDestination(target.position);
            //  hamHamburgerAnimator.SetFloat("Distance", distance);

            if (distance <= agentHam.stoppingDistance)
            {
                playerAnimator.SetTrigger("Die");
                hamHamburgerAnimator.SetBool("Chase", false);

            }
        }
        else
        {
            hamHamburgerAnimator.SetBool("Chase", false);

        }


        //if (Physics.Raycast(rayHamburger, out raycastHitHamburger, 10))
        //{
        //    Debug.DrawLine(rayHamburger.origin, raycastHitHamburger.point, Color.red);
        //    if(raycastHitHamburger.transform.tag == "Player")
        //    {
        //        playerAnimator.SetTrigger("Die");
        //    }

        //}

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
