using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlikyController : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 50;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private GameObject target;

    public GameObject gun;
    private Animator animController;
    private float horizontalDirection;
    private float verticalDirection;
    private void OnEnable()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage()
    {
        currentHealth -= startingHealth;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animController.SetTrigger("Die");
    }

    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalDirection = Input.GetAxis("Horizontal");
        verticalDirection = Input.GetAxis("Vertical");
        //Special Dance
        if (Input.GetKey(KeyCode.LeftShift))
            if (Input.GetKeyUp(KeyCode.Z))
            {
                animController.SetTrigger("Dance");
                gun.SetActive(false);

            }


        if (animController.GetBool("Shooting"))
        {
            gun.SetActive(true);
            target.SetActive(true);

        }
        else
        {
            gun.SetActive(false);
            target.SetActive(false);

        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            animController.SetBool("Hypnosis", true);
            gun.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            animController.SetBool("Hypnosis", false);
        }
        

        //Walk-run
        animController.SetFloat("Speed", verticalDirection);
        animController.SetFloat("Direction", horizontalDirection);
     

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

    public void IncreaseHealth(int increase)
    {
        if(currentHealth <= startingHealth)
        {
            currentHealth += increase;
            healthChange((float)currentHealth / (float)startingHealth);
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
