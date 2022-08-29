using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField]
    private Image gunPower;
    [SerializeField]
    private int maxBullets = 10;
    [SerializeField]
    private int currentBullets;
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
                currentBullets--;
                shootingBarChange((float)currentBullets / (float)maxBullets);
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

    private void OnEnable()
    {
        currentBullets = maxBullets;
    }

    private void shootingBarChange(float _bulletsDamage)
    {
        StartCoroutine(shootingBar(_bulletsDamage));
    }

    private IEnumerator shootingBar(float _bulletsDamage)
    {
        float preHealth = gunPower.fillAmount;
        float elapsed = 0f;
        while (elapsed < 0.2f)
        {
            elapsed += Time.deltaTime;
            gunPower.fillAmount = Mathf.Lerp(preHealth, _bulletsDamage, elapsed / 0.2f);
            yield return null;
        }

        gunPower.fillAmount = _bulletsDamage;
    }

}
