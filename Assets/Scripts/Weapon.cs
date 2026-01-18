using StarterAssets;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ParticleSystem muzzleFlash;

    [Header("Settings")]
    [SerializeField] int damageAmount = 1;

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

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            EnemyHP enemyHP = hit.collider.GetComponent<EnemyHP>();
            enemyHP?.TakeDamage(damageAmount);

            starterAssetsInputs.ShootInput(false);
        }
    }
}
