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
       // healthBar = GetComponent<Image>(); ;
        agentHam = GetComponent<NavMeshAgent>();
        hamHamburgerAnimator = GetComponent<Animator>();
        playerAnimator = PlayerManager.instance.playerSlinky.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        //   rayHamburger = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (distance <= lookRadius)
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
            //Damage Slinky
            Debug.DrawRay(attackPlayer.position, attackPlayer.forward * 100, Color.blue, 2f);
        }
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, lookRadius);
    //}

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
