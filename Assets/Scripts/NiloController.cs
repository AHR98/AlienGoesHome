using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class NiloController : MonoBehaviour
{
    Animator niloAnimnator;
    private float timer;
    Animator playerAnimator;
    GameObject slinkyPlayer;
    public float lookRadius = 3f;
    NavMeshAgent agentNilo;
    Transform target;
    public bool followSlinky = false;
    public bool saveData = false;
    [SerializeField]
    private GameObject hiSlinky;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.playerSlinky.transform;
        // healthBar = GetComponent<Image>(); ;
        agentNilo = GetComponent<NavMeshAgent>();
        niloAnimnator = GetComponent<Animator>();
        slinkyPlayer = PlayerManager.instance.playerSlinky.gameObject;
        playerAnimator = slinkyPlayer.GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            followSlinky = true;
            agentNilo.SetDestination(target.position);

        }

        if (followSlinky)
        {
            
            if (!saveData)
            {
                GameManager.instance.saveData();
                saveData = true;
                hiSlinky.SetActive(true);
                StartCoroutine(desactivateHelloPanel());
            }
            
            agentNilo.SetDestination(target.position);
            niloAnimnator.SetBool("Walk", true);
            if (distance <= agentNilo.stoppingDistance)
            {
                niloAnimnator.SetBool("Walk", false);
            }
        }
    }

    private IEnumerator desactivateHelloPanel()
    {
        yield return new WaitForSeconds(6);
        hiSlinky.SetActive(false);
    }    
}
