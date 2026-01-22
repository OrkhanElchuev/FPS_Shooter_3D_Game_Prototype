using UnityEngine;

/// <summary>
/// ScriptableObject defining a weapon's static data such as damage,
/// fire rate, magazine size, zoom behavior, and associated prefabs.
/// </summary>

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")] 
public class WeaponSO : ScriptableObject
{
    [Header("References")]
    [Tooltip("Weapon prefab instantiated when switching to this weapon.")]
    public GameObject WeaponPrefab;
    [Tooltip("Impact VFX prefab spawned where the ray hits.")]
    public GameObject HitVFXPrefab;

    [Header("Shooting Settings")]
    [Tooltip("Damage dealt per shot.")]
    public int Damage = 1;
    [Tooltip("Minimum time between shots (seconds).")]
    public float FireRate = 0.5f;
    
    [Header("Weapon Settings")]
    [Tooltip("If true, holding shoot continues firing.")]
    public bool IsAutomatic = false;
    [Tooltip("Maximum ammo held in the weapon (magazine size).")]
    public int MagazineSize = 12;
    [Tooltip("If true, weapon supports zoom mode using a scope.")]
    public bool CanZoom = false;
    [Tooltip("Field Of View (FOV) used during zoom.")]
    public float ZoomAmount = 10f;
    [Tooltip("Rotation speed while zooming (lower is slower).")]
    public float ZoomRotationSpeed = 0.3f;
}
