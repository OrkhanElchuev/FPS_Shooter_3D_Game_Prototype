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
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] LayerMask interactionLayers;

    CinemachineImpulseSource impulseSource;

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO)
    {
        muzzleFlash.Play();
        impulseSource.GenerateImpulse();
        
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, 
            out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            // Create shoot particle effect on the surface that the ray hits
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);
            var damageable = hit.collider.GetComponentInParent<IDamageable>();
            damageable?.TakeDamage(weaponSO.Damage);
        }
    }
}
