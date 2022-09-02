using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class HamburgerController : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 10;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private Image healthBar;
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
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.playerSlinky.transform;
       // healthBar = GetComponent<Image>(); ;
        agentHam = GetComponent<NavMeshAgent>();
        hamHamburgerAnimator = GetComponent<Animator>();
        slinkyPlayer = PlayerManager.instance.playerSlinky.gameObject;
        playerAnimator = slinkyPlayer.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        //Debug.DrawRay(attackPlayer.position, attackPlayer.forward, Color.blue, 2f);

        
        //   rayHamburger = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (distance <= lookRadius && !isDead)
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
            playerAnimator.SetBool("Shooting", false);


        }




    }

    private void Attack(bool attack)
    {
        //Make a 360 ray to make damage to slinky
       
        //Debug.DrawRay(attackPlayer.position, q * d, Color.green);


        if (attack && timer >= 3f)
        {
            hamHamburgerAnimator.SetBool("Attack", attack);
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
   

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, lookRadius);
    //}

    private void Die()
    {
        isDead = true;
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
