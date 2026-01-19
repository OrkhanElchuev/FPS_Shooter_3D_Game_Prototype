using StarterAssets;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] GameObject hitVFXPrefab;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] Animator animator;

    const string SHOOT_STRING = "Shoot";

    StarterAssetsInputs starterAssetsInputs;

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();    
    }

    void Update()
    {
        HandleShoot();
    }

    private void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;

        muzzleFlash.Play();
        animator.Play(SHOOT_STRING, 0, 0f);

        starterAssetsInputs.ShootInput(false);

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            // Create shoot particle effect on the surface that the ray hits
            Instantiate(hitVFXPrefab, hit.point, quaternion.identity);

            EnemyHP enemyHP = hit.collider.GetComponent<EnemyHP>();
            enemyHP?.TakeDamage(weaponSO.Damage);
        }
    }
}
