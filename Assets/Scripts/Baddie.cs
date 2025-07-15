using UnityEngine;

public class Baddie : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float damageThreshold = 0.2f;
    [SerializeField] private GameObject deathEffectParticles;
    [SerializeField] private AudioClip deathSound;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void DamageBaddie(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        GameManager.Instance.RemoveBadddie(this);

        if (deathEffectParticles != null)
            Instantiate(deathEffectParticles, this.transform.position, Quaternion.identity);
        else
            UnityEngine.Debug.LogWarning("Baddie: deathEffectParticles is not assigned.");

        if (deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        else
            UnityEngine.Debug.LogWarning("Baddie: deathSound is not assigned.");

        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactVelocity = collision.relativeVelocity.magnitude;

        if(impactVelocity >= damageThreshold)
        {
            DamageBaddie(impactVelocity);
        }
    }
}
