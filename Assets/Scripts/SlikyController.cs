using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlikyController : MonoBehaviour
{
    [SerializeField]
    private GameObject pressKeyK;
    [SerializeField]
    private int startingHealth = 50;
    [SerializeField]
    private Image healthBar;
    public int currentHealth;
    private int hypnosis = 20;
    public int currentHypnosis;
    private int damageHypnosis = 5;
    [SerializeField]
    private AudioSource hypnosisSFX;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject niloAnim;
    [SerializeField]
    private Image hypnosisBar;
    public GameObject gun;
    private Animator animController;
    private float horizontalDirection;
    private float verticalDirection;
    private float jumpingForce = 8.5f;
    private CharacterController characterController;
    public bool isTouchingFloor = false;
    private Vector3 velocity;
    public bool level1 = false;
    public bool isDead = false;

    [SerializeField]
    private ParticleSystem hypnosisParticle;
    private void OnEnable()
    {
        currentHealth = startingHealth;
        currentHypnosis = hypnosis;
    }


    private void Die()
    {
        niloAnim.GetComponent<Animator>().SetTrigger("SlinkyDied");
        animController.SetTrigger("Die");
        //characterController.Move(velocity * Time.deltaTime);
        isDead = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        isDead = false;
        isTouchingFloor = false;
        level1 = false;
        currentHypnosis = hypnosis;
        animController = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move;
        velocity.x = 0;

        velocity.z = 0;
        horizontalDirection = Input.GetAxis("Horizontal");
        verticalDirection = Input.GetAxis("Vertical");
        //Walk-run
        animController.SetFloat("Speed", verticalDirection);
        animController.SetFloat("Direction", horizontalDirection);

        move = transform.right * horizontalDirection + transform.forward * verticalDirection;
        //Debug.Log("Move " + move.ToString());

        //characterController.Move(move  * Time.deltaTime);
        animController.SetBool("Jump", false);
        animController.SetBool("Hypnosis", false);

        //Special Dance
        if (Input.GetKey(KeyCode.LeftShift))
            if (Input.GetKeyUp(KeyCode.Z))
            {
                animController.SetTrigger("Dance");
                gun.SetActive(false);

            }

        if (Input.GetKeyDown(KeyCode.Space) && isTouchingFloor)
        {
            animController.SetBool("Jump", true);
            isTouchingFloor = false; //Bcs he is jumping

            velocity.y = jumpingForce;
            //transform.position += velocity;

        }
        else if(!isTouchingFloor && !level1)
        {
            velocity.z = move.z * 3;
            velocity.x = move.x * 3;

        }
       

        if (animController.GetBool("Shooting"))
        {
            gun.SetActive(true);
            target.SetActive(true);

            if (!level1) //Level2
            {
                pressKeyK.SetActive(true);
                if (Input.GetKeyDown(KeyCode.K))
                {
                    hypnosisParticle.transform.gameObject.SetActive(true);
                    AttackHypnosis();
                }

            }

        }
        else
        {
            gun.SetActive(false);
            target.SetActive(false);
            pressKeyK.SetActive(false);
            hypnosisParticle.transform.gameObject.SetActive(false);

        }




        velocity.y += -9.81f * Time.deltaTime; //Así siempre lo ancla hacia abajo, hacia la tierra
        characterController.Move(velocity * Time.deltaTime);
        setGameManagerLevel();

    }
    private void setGameManagerLevel()
    {
        if(level1)
            GameManager.instance.setGameLevel(1);
        if(isTouchingFloor)
            GameManager.instance.setGameLevel(2);     
        if (animController.GetBool("Shooting"))
            GameManager.instance.setGameLevel(3);
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.gameObject.CompareTag("Floor")) //Level2
        {
            //Slinky is touching the floor
            isTouchingFloor = true;
            level1 = false;
        }
        if(collision.transform.gameObject.CompareTag("FloorLevel1"))
        {
            level1 = true;
            isTouchingFloor = false;

        }
    }
    private void AttackHypnosis()
    {
       
        if (currentHypnosis != 0)
        {
            animController.SetBool("Hypnosis", true);
            hypnosisParticle.Play();
            hypnosisSFX.Play();
            currentHypnosis -= damageHypnosis;
            hypnosisChange((float)currentHypnosis / (float)hypnosis);
            GetComponent<Gun>().FireGun(damageHypnosis);
        }
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthChange((float)currentHealth / (float)startingHealth);
        StartCoroutine(pauseGame());
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private IEnumerator pauseGame()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.3f);
        GameManager.instance.ResumeGame();
    }
    public void setLoadData()
    {
        healthChange((float)currentHealth / (float)startingHealth);
        hypnosisChange((float)currentHypnosis / (float)hypnosis);
    }
    public void IncreaseHealth(int increase)
    {
        if(currentHealth < startingHealth)
        {
            if ((currentHealth + increase) == startingHealth)
                currentHealth = startingHealth;
            else
                currentHealth += increase;
            healthChange((float)currentHealth / (float)startingHealth);
        }
      
    }
    public void IncreaseHypnosis(int increase)
    {
        if (currentHypnosis < hypnosis)
        {
            if ((currentHypnosis + increase) == hypnosis)
                currentHypnosis = hypnosis;
            else
                currentHypnosis += increase;
            hypnosisChange((float)currentHypnosis / (float)hypnosis);
        }

    }
    private void healthChange(float _health)
    {
        StartCoroutine(changeHealthBar(_health));
    }

    private void hypnosisChange(float _hpynosis)
    {
        StartCoroutine(changeHpynosisBar(_hpynosis));
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
    private IEnumerator changeHpynosisBar(float _hpynosis)
    {
        float preHpynosis = hypnosisBar.fillAmount;
        float elapsed = 0f;
        while (elapsed < 0.2f)
        {
            elapsed += Time.deltaTime;
            hypnosisBar.fillAmount = Mathf.Lerp(preHpynosis, _hpynosis, elapsed / 0.2f);
            yield return null;
        }

        hypnosisBar.fillAmount = _hpynosis;
    }

   
}
