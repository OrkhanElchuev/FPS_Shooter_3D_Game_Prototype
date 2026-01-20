using Cinemachine;
using StarterAssets;
using UnityEngine;


public class ActiveWeapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] GameObject zoomEffectVignette;

    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;
    FirstPersonController firstPersonController;

    const string SHOOT_STRING = "Shoot";

    float timeSinceLastShot = 0f;
    float defaultFOV;
    float defaultRotationSpeed;

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.weaponSO = weaponSO;
    }

    private void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if (!starterAssetsInputs.shoot) return;

        if (timeSinceLastShot >= weaponSO.FireRate)
        {
            currentWeapon.Shoot(weaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0f;
        }

        if (!weaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    private void HandleZoom()
    {
        if (!weaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            playerFollowCamera.m_Lens.FieldOfView = weaponSO.ZoomAmount;
            zoomEffectVignette.SetActive(true);
            firstPersonController.ChangeRotationSpeed(weaponSO.zoomRotationSpeed);
        }
        else
        {
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            zoomEffectVignette.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }
}
