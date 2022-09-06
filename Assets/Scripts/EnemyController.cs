using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 10;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private Image healthBar;
    public bool isDead = false;
    private void OnEnable()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage, Animator enemyAnimator, Animator slinkyAnimator)
    {
        currentHealth -= damage;
        healthChange((float)currentHealth / (float)startingHealth);
        if (currentHealth <= 0)
        {
             Die(enemyAnimator, slinkyAnimator);
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

    private void Die(Animator enemyAnimator, Animator slinkyAnimator)
    {
        isDead = true;
        enemyAnimator.SetTrigger("Die");
        slinkyAnimator.SetBool("Shooting", false);
    }

    public bool getDieInfo()
    {
        return isDead;
    }
}
