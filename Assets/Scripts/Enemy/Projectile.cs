using UnityEngine;

/// <summary>
/// Handles projectile movement, damage application on collision,
/// and spawning hit VFX before destroying itself.
/// Used by turrets or other projectile-based attackers.
/// </summary>

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [Tooltip("VFX prefab spawned when projectile hits something.")]
    [SerializeField] GameObject projectileHitVFX;

    [Header("Projectile Settings")]
    [Tooltip("Projectile travel speed.")]
    [SerializeField] float speed = 30f;

    Rigidbody rb;

    int damage;

    void Awake()
    {
        // Cache rigidbody reference for physics movement.
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Fire the projectile forward immediately on spawn.
        rb.linearVelocity = transform.forward * speed;
    }

    // Initialize the projectile damage value from shooter.
    // Must be called after instantiation.
    public void Init(int damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        // Apply damage if the hit object is Player.
        PlayerHP playerHP = other.GetComponent<PlayerHP>();
        playerHP?.TakeDamage(damage);

        // Spawn Impact VFX.
        if (projectileHitVFX != null)
        {
            Instantiate(projectileHitVFX, transform.position, Quaternion.identity); 
        }
        
        Destroy(this.gameObject);
    }
}
