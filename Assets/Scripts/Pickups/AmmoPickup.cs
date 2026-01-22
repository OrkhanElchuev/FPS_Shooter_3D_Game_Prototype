using System;
using UnityEngine;

/// <summary>
/// Pickup that adds ammunition to the player's currently active weapon
/// when collected.
/// </summary>

public class AmmoPickup : Pickup
{
    [Header("Ammo Pickup Settings")]
    [Tooltip("How much ammo to add when picked up.")]
    [SerializeField] int ammoAmount = 100;
    
    // Adds ammo to the player's currently active weapon system.
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        if (!activeWeapon) return;
        activeWeapon.AdjustAmmo(ammoAmount);
    }
}
