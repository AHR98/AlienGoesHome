using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class HamburgerController : MonoBehaviour
{
    [SerializeField]
    private AudioSource attackSFX;
    Animator hamHamburgerAnimator;
    private float timer;
    Animator playerAnimator;
    GameObject slinkyPlayer;
    public float lookRadius = 6f;
    Transform target;
    NavMeshAgent agentHam;
    [SerializeField]
    private Transform attackPlayer;
    [SerializeField][Range(1, 10)] private int damage = 1;
    bool isDead = false;
    private RaycastHit hitInfo;
    private Ray ray;
    private Quaternion quaternionRay;
    private Vector3 distanceRay;
    private EnemyController enemyController;
    private bool chaseSlinky = false;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.playerSlinky.transform;
       // healthBar = GetComponent<Image>(); ;
        agentHam = GetComponent<NavMeshAgent>();
        hamHamburgerAnimator = GetComponent<Animator>();
        slinkyPlayer = PlayerManager.instance.playerSlinky.gameObject;
        playerAnimator = slinkyPlayer.GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        //Debug.DrawRay(attackPlayer.position, attackPlayer.forward, Color.blue, 2f);
        isDead = enemyController.getDieInfo();
        //Check if the player is near before the chase
        quaternionRay = Quaternion.AngleAxis(100 * Time.time, Vector3.up);
        distanceRay = transform.forward * 10;
        ray = new Ray(attackPlayer.position, quaternionRay * distanceRay);
        if (Physics.Raycast(ray, out hitInfo, lookRadius))
        {
            if (hitInfo.transform.CompareTag("Player"))
            {
                //Debug.Log("Es slinky!");
                chaseSlinky = true;
            }
        }
        //   rayHamburger = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (distance <= lookRadius && !isDead && chaseSlinky)
        {
            hamHamburgerAnimator.SetBool("Chase", true);
            agentHam.SetDestination(target.position);
            playerAnimator.SetBool("Shooting", true);
            
            //  hamHamburgerAnimator.SetFloat("Distance", distance);

            if (distance <= agentHam.stoppingDistance ) //Atack
            {
                // playerAnimator.SetTrigger("Die");
                timer += Time.deltaTime;

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
         //   playerAnimator.SetBool("Shooting", false);


        }



        
    }

    private void Attack(bool attack)
    {
        //Make a 360 ray to make damage to slinky

        //Debug.DrawRay(attackPlayer.position, q * d, Color.green);
        transform.LookAt(target); //look at Slinky

        if (attack && timer >= 3f)
        {
            hamHamburgerAnimator.SetBool("Attack", attack);
            attackSFX.Play();
            quaternionRay = Quaternion.AngleAxis(100 * Time.time, Vector3.up);
            distanceRay = transform.forward * 10;
            ray = new Ray(attackPlayer.position, quaternionRay * distanceRay);
          //  Debug.DrawRay(attackPlayer.position, quaternionRay * distanceRay, Color.green);
            if (Physics.Raycast(ray, out hitInfo, agentHam.stoppingDistance))
            {
                if (hitInfo.transform.CompareTag("Player"))
                {
                    //Destroy(hitInfo.collider.gameObject);
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
            hamHamburgerAnimator.SetBool("Attack", false);

        }
    }
  
}
