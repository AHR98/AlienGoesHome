using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    [Range(0.5f, 1.5f)] private float fireRate = 1;
    [SerializeField] [Range(1, 10)] private int damage = 1;
    private float timer;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private ParticleSystem shootParticle;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= fireRate)
        {
            if(Input.GetButton("Fire1"))
            {
                timer = 0;
                FireGun();
            }
        }
    }

    private void FireGun()
    {
        //Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hitInfo;

        shootParticle.Play();

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            if (hitInfo.transform.CompareTag("Enemy"))
            {
                //Destroy(hitInfo.collider.gameObject);
                var healthEnemy = hitInfo.collider.GetComponent<HamburgerController>();
                if (healthEnemy != null)
                {
                    healthEnemy.TakeDamage(damage);
                }
            }
        }

    }
}
