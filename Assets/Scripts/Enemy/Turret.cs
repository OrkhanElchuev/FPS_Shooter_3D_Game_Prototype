using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform projectileSpawnPoint;

    [Header("Turret Settings")]
    [SerializeField] float fireRate = 3f;
    [SerializeField] int damage = 2;

    PlayerHP player;
    
    void Start()
    {
        player = FindFirstObjectByType<PlayerHP>();
        StartCoroutine(ShootRoutine());
    }

    void Update()
    {
        // Player destroyed? stop using its target point
        if (!player || playerTargetPoint == null) return;

        turretHead.LookAt(playerTargetPoint.position);
    }

    IEnumerator ShootRoutine()
    {
        while (player && playerTargetPoint != null)
        {
            yield return new WaitForSeconds(fireRate);
            Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, 
                Quaternion.identity).GetComponent<Projectile>();
            newProjectile.transform.LookAt(playerTargetPoint);
            newProjectile.Init(damage);
        }
    }
}
