using UnityEngine;

/// <summary>
/// Pickup that switches the player's active weapon to a specified weapon
/// defined by a Weapon ScriptableObject.
/// </summary>

public class WeaponPickup : Pickup
{
    [Header("References")]
    [SerializeField] WeaponSO weaponSO;

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.SwitchWeapon(weaponSO);
    }
}
