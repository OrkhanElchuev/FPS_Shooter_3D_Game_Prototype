using UnityEngine;
using UnityEngine.Rendering;

public class ExplosionDamage : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] float damage = 3f;
    

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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hitCollider in hitColliders)
        {
            PlayerHP playerHP = hitCollider.GetComponent<PlayerHP>();

            if (!playerHP) continue;

            playerHP.TakeDamage(damage);

            break;
        }
    }
}
