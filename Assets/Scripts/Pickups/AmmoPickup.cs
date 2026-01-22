using System;
using UnityEngine;

/// <summary>
/// Pickup that adds ammunition to the player's currently active weapon
/// when collected.
/// </summary>

public class AmmoPickup : Pickup
{
    [SerializeField] int ammoAmount = 100;
    
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.AdjustAmmo(ammoAmount);
    }
}
