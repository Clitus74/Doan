using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] public GunData gunData;
    [SerializeField] public Camera cam;
    [SerializeField] public Animator gunAnimator;
    [SerializeField] private FirstpersonShooterController FPSController;
    [SerializeField] private Recoil recoil;
    [SerializeField] private Transform gunTransform;
    
    protected float timeBtwBullets;

    private void OnEnable()
    {
        gunData.Reloading = false;
        timeBtwBullets = 0;
    }
    private void OnDisable()
    {
        gunData.Reloading = false;
        timeBtwBullets = 0;
    }
    private void Awake()
    {
        gunData.currentAmmo = 30;
        
    }
    private void Start()
    {
        FirstpersonShooterController.shootInput += Shoot;
        FirstpersonShooterController.reloadInput += StartReloading;
        FirstpersonShooterController.ADSInput += AimDownSightFOV;
    }
    public void StartReloading()
    {
        if (!gunData.Reloading)
        {
            //Reload
            StartCoroutine(Reload());
        }
    }
    private IEnumerator Reload()
    {
        gunData.Reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        gunData.Reloading = false;
    }
    private bool ReadyToShoot => !gunData.Reloading && timeBtwBullets > 1f / (gunData.fireRate / 60);

    public void Shoot()
    {
        if(gunData.currentAmmo > 0)
        {
            if (ReadyToShoot)
            {
                gunAnimator.SetBool("Shooting", true);
                recoil.RecoilFire();
                //Điểm giữa màn hình
                Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);
                
                Ray ray = cam.ScreenPointToRay(center);
                Transform hitTransform = null;
                if (Physics.Raycast(ray, out RaycastHit hit, 999f))
                {
                    if (hit.transform != null) hitTransform = hit.transform;
                }

                if (hitTransform != null && hitTransform.GetComponent<BulletTarget>() != null)
                {
                    Debug.Log("This is an enemy");
                    hitTransform.GetComponent<BulletTarget>().GetHit(2);
                }
                else
                {
                    Debug.Log("Just a normal object");
                }
                gunData.currentAmmo--;
                timeBtwBullets = 0;
                
                //Vector3 bulletDir = (hit.point - bulletSpawnPos.position).normalized;
                //Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.LookRotation(bulletDir, Vector3.up));
                
            }
        }
    }

    public void AimDownSightFOV()
    {
        if (Input.GetMouseButton(1))
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, FPSController.defaultFOV * 0.75f, Time.deltaTime * 13f);
            gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, gunData.adsPos, Time.deltaTime * 20f);
            gunData.Aiming = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            gunData.Aiming = false;
            cam.fieldOfView = FPSController.defaultFOV;
            gunTransform.localPosition = gunData.initialPos;
        }
    }

    private void Update()
    {
        timeBtwBullets += Time.deltaTime;
        //Debug.Log("Ammo: " + gunData.currentAmmo);
        //Debug.Log("Ready: " + ReadyToShoot);
        //Debug.Log("Reloading: " + gunData.Reloading);
    }
}
