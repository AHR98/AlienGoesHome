using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : MonoBehaviour
{
    [SerializeField]
    private Transform firePoint;
    private int health = 5;
    private void getItems()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hitInfo;
        Debug.DrawRay(firePoint.position, firePoint.forward, Color.green);
        if (Physics.Raycast(ray, out hitInfo, 8))
        {
            if (hitInfo.transform.CompareTag("Health"))
            {
                if(Input.GetKeyDown(KeyCode.F))
                {
                    GetComponent<SlikyController>().IncreaseHealth(health);
                    Destroy(hitInfo.transform.gameObject);
                }

            }
        }
    }

    private void Update()
    {
        getItems();
    }
}
