using System;
using Cinemachine;
using UnityEngine;

/// <summary>
/// Handles the firing logic of a weapon, including raycast hit detection,
/// spawning hit effects, camera impulse, and applying damage to valid targets.
/// </summary>

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Muzzle flash particle system played when firing.")]
    [SerializeField] ParticleSystem muzzleFlash;
    [Tooltip("Layers that can be hit by the weapon raycast.")]
    [SerializeField] LayerMask interactionLayers;

    CinemachineImpulseSource impulseSource;

    void Awake()
    {
        // Cache impulse source for camera shake effect.
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Fires the weapon once using a raycast from the main camera.
    // Spawns hit VFX and applies damage to IDamageable targets.
    public void Shoot(WeaponSO weaponSO)
    {
        muzzleFlash?.Play();
        impulseSource?.GenerateImpulse();
        
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, 
            out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            if (weaponSO.HitVFXPrefab != null)
            {
                // Create shoot particle effect on the surface that the ray hits
                Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);
            }
            
            // Damage anything that implements IDamageable.
            var damageable = hit.collider.GetComponentInParent<IDamageable>();
            damageable?.TakeDamage(weaponSO.Damage);
        }
    }
}
