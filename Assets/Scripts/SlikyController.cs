using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlikyController : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 10;
    private int currentHealth;
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
                this.gun.SetActive(false);

            }


        if (animController.GetBool("Shooting") == true)
        {
            this.gun.SetActive(true);

        }
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            //Shot
            this.gun.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            animController.SetBool("Hypnosis", true);
            this.gun.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            animController.SetBool("Hypnosis", false);
        }
        

        //Walk-run
        animController.SetFloat("Speed", verticalDirection);
        animController.SetFloat("Direction", horizontalDirection);
      
        //if (verticalDirection > 0.1f
        //    || (verticalDirection > 0.5f && horizontalDirection > 0.5f
        //    || verticalDirection > 0.5f && horizontalDirection < -0.5f))
        //{
        //    animController.SetBool("Run", true);
        //}
        //else if (verticalDirection < 0.0f) //Back
        //{
        //    animController.SetBool("TurnAroundRight", true);
        //    //  animController.SetBool("Run", true);
        //}
        //else if (horizontalDirection > 0.0f && verticalDirection == 0.0f)
        //{
        //    animController.SetBool("TurnAroundRight", true);
        //}
        //else if (horizontalDirection < 0.0f && verticalDirection == 0.0f)
        //{
        //    animController.SetBool("TurnAroundLeft", true);

        //}
        //else
        //{
        //    animController.SetBool("TurnAroundRight", false);
        //    animController.SetBool("TurnAroundLeft", false);
        //    animController.SetBool("Run", false);
        //}



    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Door"))
        {
        }
    }
}
