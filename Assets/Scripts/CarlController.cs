using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CarlController : MonoBehaviour
{
    [SerializeField]
    private AudioSource punchSFX;
    Animator carlAnimator;
    private float timer;
    Animator playerAnimator;
    GameObject slinkyPlayer;
    public float lookRadius = 6f;
    Transform target;
    NavMeshAgent agentCarl;
    [SerializeField]
    private Transform attackPlayer;
    [SerializeField][Range(1, 30)] private int damage = 15;
    public bool isDead = false;
    private RaycastHit hitInfo;
    private Ray ray;
    private Quaternion quaternionRay;
    private Vector3 distanceRay;
    private EnemyController enemyController;
    private bool chaseSlinky = false;
    private bool activateShootingSlinky = false;


    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.playerSlinky.transform;
        agentCarl = GetComponent<NavMeshAgent>();
        carlAnimator = GetComponent<Animator>();
        slinkyPlayer = PlayerManager.instance.playerSlinky.gameObject;
        playerAnimator = slinkyPlayer.GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
      
        //Check if the enemy is dead
        isDead = enemyController.getDieInfo();
        if(isDead)
        {
            GameManager.instance.carlDied();
        }
        //Check if the player is near before the chase
        quaternionRay = Quaternion.AngleAxis(100 * Time.time, Vector3.up);
        distanceRay = transform.forward * 10;
        ray = new Ray(attackPlayer.position, quaternionRay * distanceRay);
        if (Physics.Raycast(ray, out hitInfo, lookRadius))
        {
            if (hitInfo.transform.CompareTag("Player"))
            {
                chaseSlinky = true;
            }
        }
        
       

        if (distance <= lookRadius && !isDead  )
        {
            playerAnimator.SetBool("Shooting", true);
            agentCarl.SetDestination(target.position);
            carlAnimator.SetFloat("Direction", 1f);
            activateShootingSlinky = true;

            if (distance <= agentCarl.stoppingDistance) //Atack
            {
                carlAnimator.SetFloat("Direction", 0f);
                timer += Time.deltaTime;

                Attack(true);

            }
            else
            {
                Attack(false);
            }
        }
        else
        {
            Attack(false);           
            carlAnimator.SetFloat("Direction", 0f);
            if (activateShootingSlinky)
            {
                playerAnimator.SetBool("Shooting", false);
                activateShootingSlinky = false;
            }
        }

        



    }


    private void Attack(bool attack)
    {
       
        if (attack && timer >= 3f)
        {
            transform.LookAt(target); //look at Slinky
            carlAnimator.SetBool("Attack", attack);
            punchSFX.Play();
            quaternionRay = Quaternion.AngleAxis(100 * Time.time, Vector3.up);
            distanceRay = transform.forward * 10;
            ray = new Ray(attackPlayer.position, quaternionRay * distanceRay);
            if (Physics.Raycast(ray, out hitInfo, agentCarl.stoppingDistance))
            {
                if (hitInfo.transform.CompareTag("Player"))
                {
                    var healthSlinky = hitInfo.collider.GetComponent<SlikyController>();
                    if (healthSlinky != null)
                    {
                        healthSlinky.TakeDamage(damage);
                        timer = 0;

                    }
                }
            }



        }
        else
        {
            carlAnimator.SetBool("Attack", false);

        }
    }
}
