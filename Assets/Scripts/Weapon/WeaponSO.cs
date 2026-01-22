using UnityEngine;

/// <summary>
/// ScriptableObject defining a weapon's static data such as damage,
/// fire rate, magazine size, zoom behavior, and associated prefabs.
/// </summary>

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")] 
public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab;
    public int Damage = 1;
    public float FireRate = 0.5f;
    public GameObject HitVFXPrefab;
    public bool IsAutomatic = false;
    public bool CanZoom = false;
    public float ZoomAmount = 10f;
    public float zoomRotationSpeed = 0.3f;
    public int MagazineSize = 12;
}
