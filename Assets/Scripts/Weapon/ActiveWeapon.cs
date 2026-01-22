using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;

/// <summary>
/// Controls the player's currently equipped weapon, including shooting,
/// ammo management, weapon switching, animations, and zoom behavior.
/// Acts as the main bridge between player input and weapon functionality.
/// </summary>

public class ActiveWeapon : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Weapon equipped at game start.")]
    [SerializeField] WeaponSO startingWeapon;
    [Tooltip("Cinemachine camera following the player.")]
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [Tooltip("Weapon camera (used for zoom FOV).")]
    [SerializeField] Camera weaponCamera;
    [Tooltip("Vignette effect enabled during zoom.")]
    [SerializeField] GameObject zoomEffectVignette;
    [Tooltip("UI text showing current ammo.")]
    [SerializeField] TMP_Text ammoText;

    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;
    WeaponSO currentWeaponSO;
    FirstPersonController firstPersonController;

    const string SHOOT_STRING = "Shoot";

    float timeSinceLastShot = 0f;
    float defaultFOV;
    float defaultRotationSpeed;
    int currentAmmo;

    void Awake()
    {
        // Cache input/controller/components
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();

        // Cache defaults so it can restore after zoom effect.
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    void Start()
    {
        SwitchWeapon(startingWeapon);
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    // Add/Remove ammo and update UI. Clamps to magazine size of each weapon.
    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;

        if (currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize;
        }

        // 2 Full Digits at minimum are Displayed (e.g. 01, 10).
        ammoText.text = currentAmmo.ToString("D2");
    }

    // Destroys current weapon object and instantiates the new one from WeaponSO.
    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.WeaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.currentWeaponSO = weaponSO;

        // Fill ammo on switch.
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    private void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if (!starterAssetsInputs.shoot) return;

        // Fire if fire rate allows and there is still ammo.
        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            currentWeapon.Shoot(currentWeaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0f;
            AdjustAmmo(-1);
        }

        // Semi-auto weapons consume the input, so another click is required to shoot.
        if (!currentWeaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    private void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            // Apply Zoom settings for Sniper Rifle.
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
            zoomEffectVignette.SetActive(true);
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            // Restore defaults after going back from Zoom to Normal Scope mode.
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;
            zoomEffectVignette.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }
}
