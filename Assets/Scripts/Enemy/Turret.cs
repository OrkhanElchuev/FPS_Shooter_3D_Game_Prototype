using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetPoint;

    void Update()
    {
        turretHead.LookAt(playerTargetPoint.position);
    }
}
