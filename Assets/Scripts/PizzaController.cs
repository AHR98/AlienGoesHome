using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class PizzaController : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 10;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private Image healthBar;
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
    [SerializeField][Range(1, 10)] private int damage = 1;
    bool isDead = false;
    private RaycastHit hitInfo;
    private Ray ray;

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
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.playerSlinky.transform;
        // healthBar = GetComponent<Image>(); ;
        pizzaAgent = GetComponent<NavMeshAgent>();
        animatorPizza = GetComponent<Animator>();
        slinkyPlayer = PlayerManager.instance.playerSlinky.gameObject;
        slinkyAnimator = slinkyPlayer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(target.position, transform.position);
        //   rayHamburger = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (distance <= lookRadius && !isDead)
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
            else
            {
                animatorPizza.SetBool("Chase", true);
                Attack(false);
            }
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
    private void Die()
    {
        isDead = true;
        animatorPizza.SetTrigger("Die");
        slinkyAnimator.SetBool("Shooting", false);
    }

    private void OnEnable()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthChange((float)currentHealth / (float)startingHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void healthChange(float _health)
    {
        StartCoroutine(changeHealthBar(_health));
    }

    private IEnumerator changeHealthBar(float _health)
    {
        float preHealth = healthBar.fillAmount;
        float elapsed = 0f;
        while (elapsed < 0.2f)
        {
            elapsed += Time.deltaTime;
            healthBar.fillAmount = Mathf.Lerp(preHealth, _health, elapsed / 0.2f);
            yield return null;
        }

        healthBar.fillAmount = _health;
    }
}
