using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollectItems : MonoBehaviour
{
    [SerializeField]
    private AudioSource collectItemsSFX;
    [SerializeField]
    private AudioSource healthSFX;
    [SerializeField]
    private ParticleSystem openBoxParticle;
    [SerializeField]
    private GameObject pressKeyF;
    [SerializeField]
    private Transform firePoint;
    private Animator anim;
    private int health = 20;
    private int bullets = 10;
    private int hypnosis = 10;
    private void getItems()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        Debug.DrawRay(firePoint.position, firePoint.forward, Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 3))
        {

            if (hitInfo.transform.CompareTag("Health"))
            {
                pressKeyF.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    pressKeyF.SetActive(false);
                    GetComponent<SlikyController>().IncreaseHealth(health);
                    healthSFX.Play();
                    Destroy(hitInfo.transform.gameObject);

                }


            }
            else if (hitInfo.transform.CompareTag("Bullet"))
            {
                pressKeyF.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    pressKeyF.SetActive(false);
                    GetComponent<Gun>().increaseBulletsBar(bullets);
                    collectItemsSFX.Play();
                    Destroy(hitInfo.transform.gameObject);
                }

            }
            else if (hitInfo.transform.CompareTag("SurpriseBox"))
            {
                pressKeyF.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    anim = hitInfo.transform.GetComponentInParent<Animator>();
                    anim.SetTrigger("Opened");
                    collectItemsSFX.Play();
                    openBoxParticle.Play();
                    GetComponent<SlikyController>().IncreaseHypnosis(hypnosis*2);
                    StartCoroutine(hideBoxes(hitInfo.transform.parent.gameObject));
            
                }
            }
            else if (hitInfo.transform.CompareTag("SurpriseBoxHealth"))
            {
                pressKeyF.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    anim = hitInfo.transform.GetComponentInParent<Animator>();
                    anim.SetTrigger("Opened");
                    healthSFX.Play();
                    openBoxParticle.Play();
                    GetComponent<SlikyController>().IncreaseHealth(health*3);
                    StartCoroutine(hideBoxes(hitInfo.transform.parent.gameObject));

                }
            }
            else if (hitInfo.transform.CompareTag("SurpriseBoxBullets"))
            {
                pressKeyF.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    anim = hitInfo.transform.GetComponentInParent<Animator>();
                    anim.SetTrigger("Opened");
                    collectItemsSFX.Play();
                    openBoxParticle.Play();
                    GetComponent<Gun>().increaseBulletsBar(bullets*3);
                    StartCoroutine(hideBoxes(hitInfo.transform.parent.gameObject));

                }
            }
        }
        
        else
            pressKeyF.SetActive(false);

    }
    private IEnumerator hideBoxes(GameObject boxes)
    {
        yield return new WaitForSeconds(3);
        boxes.SetActive(false);
        //unhideBoxes
        StartCoroutine(unhideBoxes(boxes));
    }
    private IEnumerator unhideBoxes(GameObject boxes)
    {
        yield return new WaitForSeconds(30);
        boxes.SetActive(true);
    }
    private void Update()
    {
        getItems();
        //pressKeyF.SetActive(false);

    }
}
