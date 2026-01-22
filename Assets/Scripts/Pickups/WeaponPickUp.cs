using UnityEngine;

/// <summary>
/// Pickup that switches the player's active weapon to a specified weapon
/// defined by a Weapon ScriptableObject.
/// </summary>

public class WeaponPickup : Pickup
{
    [Header("References")]
    [Tooltip("Weapon ScriptableObject to switch to when picked up.")]
    [SerializeField] WeaponSO weaponSO;

    // Switches the player's weapon to the provided Weapon Scriptable Object.
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        if(!activeWeapon) return;
        activeWeapon.SwitchWeapon(weaponSO);
    }
}
