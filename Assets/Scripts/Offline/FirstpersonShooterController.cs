using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FirstpersonShooterController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform testObject;
    [SerializeField] private LayerMask aimLayerMask;
    [SerializeField] private float defaultFOV = 60f;
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private Transform bulletPrefab;


    bool aimState = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Điểm giữa màn hình
        Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = cam.ScreenPointToRay(center);
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimLayerMask))
        {
            //testObject.position = hit.point;
            hitTransform = hit.transform;
        }

        //Set FOV when use ADS
        AimDownSightFOV();
        
        //Shoot
        if(Input.GetMouseButtonDown(0))
        {
            if (hitTransform != null)
            {
                //if ...
            }
            Vector3 bulletDir = (hit.point - bulletSpawnPos.position).normalized;
            //Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.LookRotation(bulletDir, Vector3.up));
        }
    }

    private void AimDownSightFOV()
    {
        if (Input.GetMouseButton(1))
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, defaultFOV * 2 / 3, Time.deltaTime * 20f);
            aimState = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            cam.fieldOfView = defaultFOV;
            aimState = false;
        }
    }
}
