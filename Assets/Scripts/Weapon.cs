using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ParticleSystem muzzleFlash;

    public void Shoot(WeaponSO weaponSO)
    {
        RaycastHit hit;
        muzzleFlash.Play();

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            // Create shoot particle effect on the surface that the ray hits
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);
            EnemyHP enemyHP = hit.collider.GetComponent<EnemyHP>();
            enemyHP?.TakeDamage(weaponSO.Damage);
        }
    }
}
