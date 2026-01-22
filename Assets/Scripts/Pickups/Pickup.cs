using UnityEngine;

/// <summary>
/// Abstract base class for all pickup items. Handles rotation visuals,
/// player trigger detection, and cleanup, while allowing derived classes
/// to define their specific pickup behavior.
/// </summary>

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    const string PLAYER_STRING = "Player";

    void Update()
    {
        RotatePickupObjects();
    }

    void RotatePickupObjects()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            OnPickup(activeWeapon);
            Destroy(this.gameObject);
        }
    }

    protected abstract void OnPickup(ActiveWeapon activeWeapon);
}
