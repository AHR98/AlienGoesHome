using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{
    [SerializeField]
    private AudioSource shootingSFX;
    [SerializeField]
    private GameObject pressJ;
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
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    [SerializeField]
    public Transform bulletSpawn;
    bool isShootingSlinky = false;
    private EnemyController enemyController;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        isShootingSlinky = GetComponent<Animator>().GetBool("Shooting");       
            
            
        if (timer >= fireRate)
        {
            if(isShootingSlinky)
            {
                pressJ.SetActive(true);
                if (Input.GetButton("Fire1"))
                {
                    timer = 0;
                    var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * bulletSpeed;
                    shootingSFX.Play();
                    shootParticle.Play();
                    FireGun(damage);
                    currentBullets--;
                    shootingBarChange((float)currentBullets / (float)maxBullets);
                }
            }
            else
            {
                pressJ.SetActive(false);

            }

        }
        else
        {
            pressJ.SetActive(false);

        }
    }

    public void FireGun(int _damage)
    {
        //Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            if (hitInfo.transform.CompareTag("Enemy"))
            {
                var enemyAnimator = hitInfo.collider.GetComponent<Animator>();
                if (enemyAnimator != null)
                {
                    enemyController = hitInfo.collider.GetComponent<EnemyController>();
                    enemyController.TakeDamage(_damage, enemyAnimator, GetComponent<Animator>());
                    
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
    public void increaseBulletsBar(int _bulletsIncrease)
    {
        if(currentBullets < maxBullets)
        {
            if ((currentBullets + _bulletsIncrease) > maxBullets) //Si lo suma y es mayor a el máximo dejar el máximo
                currentBullets = maxBullets;
            else
                currentBullets += _bulletsIncrease;

            StartCoroutine(shootingBar((float)currentBullets / (float)maxBullets));
             
        }
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
