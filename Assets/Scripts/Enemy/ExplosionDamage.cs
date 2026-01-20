using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;


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
        
    }
}
