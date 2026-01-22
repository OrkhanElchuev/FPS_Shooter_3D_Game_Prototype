using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Automated turret that tracks a player target point, rotates toward it,
/// and periodically fires projectiles until the player is destroyed.
/// </summary>

public class Turret : MonoBehaviour
{
    [Header("Reference")]
    [Tooltip("Projectile prefab fired by this turret. Must contain a Projectile component.")]
    [SerializeField] GameObject projectilePrefab;
    [Tooltip("Transform that rotates to face the target.")]
    [SerializeField] Transform turretHead;
    [Tooltip("Target point on the player. If player is destroyed, this becomes null.")]
    [SerializeField] Transform playerTargetPoint;
    [Tooltip("Where the projectile spawns from.")]
    [SerializeField] Transform projectileSpawnPoint;

    [Header("Turret Settings")]
    [Tooltip("Seconds between shots.")]
    [SerializeField] float fireRate = 3f;
    [Tooltip("Damage dealt by each projectile.")]
    [SerializeField] int damage = 2;

    PlayerHP player;
    
    void Start()
    {
        player = FindFirstObjectByType<PlayerHP>();
        StartCoroutine(ShootRoutine());
    }

    void Update()
    {
        // Prevent MissingReferenceException when player gets destroyed.
        if (!player || playerTargetPoint == null) return;

        // Rotate turret head toward target point.
        turretHead.LookAt(playerTargetPoint.position);
    }

    IEnumerator ShootRoutine()
    {   
        // Keep shooting while player and target exist.
        while (player && playerTargetPoint != null)
        {
            yield return new WaitForSeconds(fireRate);

            // Spawn projectile, aim it, and initialize damage.
            Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, 
                Quaternion.identity).GetComponent<Projectile>();
            newProjectile.transform.LookAt(playerTargetPoint);
            newProjectile.Init(damage);
        }
    }
}
