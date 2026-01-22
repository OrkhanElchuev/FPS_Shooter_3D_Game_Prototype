using UnityEngine;
using UnityEngine.Rendering;

public class ExplosionDamage : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] float radius = 1.5f;
    [SerializeField] int maxDamage = 20;
    [SerializeField] int minDamage = 9;

    bool didExplode;
    
    void Update()
    {
        OnExplodeDamage();
    }

    // For Debugging 
    void OnDrawGizmos() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void OnExplodeDamage()
    {
        if (didExplode) return;
        didExplode = true;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        int randomDamage = Random.Range(minDamage, maxDamage + 1);

        foreach (Collider hitCollider in hitColliders)
        {
            PlayerHP playerHP = hitCollider.GetComponent<PlayerHP>();

            if (!playerHP) continue;

            playerHP.TakeDamage(randomDamage);

            break;
        }

        enabled = false;
    }
}
