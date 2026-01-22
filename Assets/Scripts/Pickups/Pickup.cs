using UnityEngine;

/// <summary>
/// Abstract base class for all pickup items. Handles rotation visuals,
/// player trigger detection, and cleanup, while allowing derived classes
/// to define their specific pickup behavior.
/// </summary>

public abstract class Pickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    [Tooltip("Rotation speed in degrees/sec for pickup visual.")]
    [SerializeField] float rotationSpeed = 100f;
    
    const string PLAYER_STRING = "Player";

    void Update()
    {
        RotatePickupObjects();
    }

    void RotatePickupObjects()
    {
        // Idle rotation effect.
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            // Find active weapon on the player so derived pickups can interact with it.
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();

            // Let the child class apply its effect.
            OnPickup(activeWeapon);

            // Remove pickup after use.
            Destroy(this.gameObject);
        }
    }

    // Implement in derived classes to apply pickup effect.
    protected abstract void OnPickup(ActiveWeapon activeWeapon);
}
