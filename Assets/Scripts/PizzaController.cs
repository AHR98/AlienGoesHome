using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class PizzaController : MonoBehaviour
{
    Animator animatorPizza;
    [SerializeField]
    private float timer;
    Animator slinkyAnimator;
    public GameObject slinkyPlayer;
    public float lookRadius = 6f;
    Transform target;
    NavMeshAgent pizzaAgent;
    [SerializeField]
    private Transform attackPlayer;
    [SerializeField][Range(1, 10)] private int damage = 10;
    bool isDead = false;
    private RaycastHit hitInfo;
    private Ray ray;
    private Quaternion quaternionRay;
    private Vector3 distanceRay;

    [SerializeField]
    [Range(0.5f, 1.5f)] private float fireRate = 1;
    [SerializeField][Range(1, 10)] private int damagePepperoni = 1;
    public GameObject pepperoniPrefab;
    public float pepperoniSpeed = 1;
    [SerializeField]
    public Transform attackPlayerSpawn;
    private bool hasAttacked = false;
    AnimatorStateInfo animStateInfo;
    public float NTime;
    private EnemyController enemyController;
    private bool chaseSlinky = false;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.playerSlinky.transform;
        enemyController = GetComponent<EnemyController>();
        pizzaAgent = GetComponent<NavMeshAgent>();
        animatorPizza = GetComponent<Animator>();
        slinkyPlayer = PlayerManager.instance.playerSlinky.gameObject;
        slinkyAnimator = slinkyPlayer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        isDead = enemyController.getDieInfo();
        //raycast o sphercast choque con el personaje <- mejora
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

        if (distance <= lookRadius && !isDead && chaseSlinky)
        {
            slinkyAnimator.SetBool("Shooting", true);
            animatorPizza.SetBool("Chase", true);
            pizzaAgent.SetDestination(target.position);

            //  hamHamburgerAnimator.SetFloat("Distance", distance);

            if (distance <= pizzaAgent.stoppingDistance ) //Atack
            {
                timer += Time.deltaTime;
                transform.LookAt(target); //look at Slinky
                animatorPizza.SetBool("Chase", false);
                Attack(true);
                

            }
            //else
            //{
            //    animatorPizza.SetBool("Chase", true);
            //    Attack(false);
            //}
        }
        else
        {
            Attack(false);
            animatorPizza.SetBool("Chase", false);
       //     slinkyAnimator.SetBool("Shooting", false);


        }

        if(hasAttacked)
        {
            hasAttacked = hasAttackSlinky();
        }


    }

    private void Attack(bool attack)
    {


        if (attack && timer >= 2.7f)
        {
            //animatorPizza.SetTrigger("ThrowPizza");
            ray = new Ray(attackPlayer.position, attackPlayer.forward * 3);
            if (Physics.Raycast(ray, out hitInfo, pizzaAgent.stoppingDistance))
            {
                if (hitInfo.transform.CompareTag("Player") && !hasAttacked )
                {
                    animatorPizza.SetBool("Attack", attack);

                    instatiatePepperoniBullets();
                    hasAttacked = true;

                    var healthSlinky = hitInfo.collider.GetComponent<SlikyController>();
                    if (healthSlinky != null)
                    {
                        healthSlinky.TakeDamage(damage);


                    }
                }
            }



        }
        else
        {
            animatorPizza.SetBool("Attack", false);

        }
    }
    private void instatiatePepperoniBullets()
    {
        var pepperoniBullet = Instantiate(pepperoniPrefab, attackPlayerSpawn.position, attackPlayerSpawn.rotation);
        pepperoniBullet.GetComponent<Rigidbody>().velocity = attackPlayerSpawn.forward * pepperoniSpeed;
        timer = 0;
    }

    private bool hasAttackSlinky()
    {

        bool animationFinished = true;
        if(animatorPizza.GetCurrentAnimatorStateInfo(0).IsName("ThrowPizza"))
        {
            animStateInfo = animatorPizza.GetCurrentAnimatorStateInfo(0);
            NTime = animStateInfo.normalizedTime;
            if (NTime > 0.95f)
            {
                animationFinished = false;

            }

        }
        
        
        return animationFinished;
    }

    
}
