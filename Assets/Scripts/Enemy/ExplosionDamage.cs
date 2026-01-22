using UnityEngine;

/// <summary>
/// Applies a one-time area-of-effect damage burst (explosion).
/// Uses a physics overlap check to detect player within a radius
/// and applies randomized damage once, then disables itself.
/// </summary>

public class ExplosionDamage : MonoBehaviour
{
    [Header("Explosion Settings")]
    [Tooltip("Radius of the explosion overlap check.")]
    [SerializeField] float radius = 1.5f;
    [Tooltip("Minimum explosion damage.")]
    [SerializeField] int minDamage = 9;
    [Tooltip("Maximum explosion damage.")]
    [SerializeField] int maxDamage = 20;

    bool didExplode; // Ensure damage is applied only once.
    
    void Update()
    {
        // Runs every frame, didExplode ensure damage applies only once.
        OnExplodeDamage();
    }

    // Applies a single burst of damage to the player within the radius.
    void OnExplodeDamage()
    {
        if (didExplode) return;
        didExplode = true;

        // Find colliders affected by the explosion.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
 
        int randomDamage = Random.Range(minDamage, maxDamage + 1);

        foreach (Collider hitCollider in hitColliders)
        {
            // Only damages the player.
            PlayerHP playerHP = hitCollider.GetComponent<PlayerHP>();
            if (!playerHP) continue;

            playerHP.TakeDamage(randomDamage);

            // Break so the damage applies once, even if multiple colliders exist on player.
            break;
        }

        // Disable script so it doesn't run again (Explosion VFX can continue playing).
        enabled = false;
    }
}
