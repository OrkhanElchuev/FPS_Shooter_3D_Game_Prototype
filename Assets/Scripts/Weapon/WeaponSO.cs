using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")] 
public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab;
    public float Damage = 1f;
    public float FireRate = 0.5f;
    public GameObject HitVFXPrefab;
    public bool IsAutomatic = false;
    public bool CanZoom = false;
    public float ZoomAmount = 10f;
    public float zoomRotationSpeed = 0.3f;
    public int magazineSize = 12;
}
