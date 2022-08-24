using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HamburgerController : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 5;
    [SerializeField]
    private int currentHealth;
    Animator hamHamburgerAnimator;
    Ray rayHamburger;
    RaycastHit raycastHitHamburger;
    Animator playerAnimator;
    public float lookRadius = 6f;
    Transform target;
    NavMeshAgent agentHam;
    [SerializeField]
    private Transform attackPlayer;
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
            playerAnimator.SetBool("Shooting", true);

            //  hamHamburgerAnimator.SetFloat("Distance", distance);

            if (distance <= agentHam.stoppingDistance) //Atack
            {
               // playerAnimator.SetTrigger("Die");
                hamHamburgerAnimator.SetBool("Chase", false);
                Attack(true);
            }
            else
            {
                hamHamburgerAnimator.SetBool("Chase", true);
                Attack(false);
            }
        }
        else
        {
            Attack(false);
            hamHamburgerAnimator.SetBool("Chase", false);        
            playerAnimator.SetBool("Shooting", false);


        }


       

    }

    private void Attack(bool attack)
    {
        hamHamburgerAnimator.SetBool("Attack", attack);
        if (attack)
        {
            //Damage player
            Debug.DrawRay(attackPlayer.position, attackPlayer.forward * 100, Color.blue, 2f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void Die()
    {
        hamHamburgerAnimator.SetTrigger("Die");
        playerAnimator.SetBool("Shooting", false);
    }

    private void OnEnable()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
