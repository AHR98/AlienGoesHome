using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollectItems : MonoBehaviour
{
    [SerializeField]
    private GameObject pressKeyF;
    [SerializeField]
    private Transform firePoint;
    private int health = 5;
    private int bullets = 3;
    private void getItems()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hitInfo;
        Debug.DrawRay(firePoint.position, firePoint.forward, Color.green);
        if (Physics.Raycast(ray, out hitInfo, 3))
        {
            
            if (hitInfo.transform.CompareTag("Health"))
            {
                pressKeyF.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    pressKeyF.SetActive(false);
                    GetComponent<SlikyController>().IncreaseHealth(health);
                    Destroy(hitInfo.transform.gameObject);

                }


            }
            else if(hitInfo.transform.CompareTag("Bullet"))
            {
                pressKeyF.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    pressKeyF.SetActive(false);

                    GetComponent<Gun>().increaseBulletsBar(bullets);
                    Destroy(hitInfo.transform.gameObject);
                }

            }

        }
        else
            pressKeyF.SetActive(false);

    }

    private void Update()
    {
        getItems();
        //pressKeyF.SetActive(false);

    }
}
