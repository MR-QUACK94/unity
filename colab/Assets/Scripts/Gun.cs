using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Gun : MonoBehaviour
{
    public Transform gunPoint;
    public GameObject projectile;
    public float force;
    public float fireRate;
    public float xSpread;
    public float ySpread;
    public int damage;

    public float bulletsToShootAtOnce;

    public bool canshoot;

    public bool automatic;

    public GameObject shootSound;

    public Camera cam;

    public Recoil recoil;
    public Rigidbody playerRb;

    public Vector3 adsPos;
    public bool ads;
    public bool isAds;
    private bool canAds;
    private Vector3 startPos;
    [Range(0f, 1f)]
    public float adsSpreadMultiplyer;
    public float adsZoom;
    private float startFov;
    public bool hideCrosshair;
    public GameObject crosshair;

    private void Start()
    {
        canshoot = true;
        startPos = transform.localPosition;
        startFov = cam.fieldOfView;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canshoot && !automatic)
        {
            Shoot();
        }
        if (Input.GetMouseButton(0) && canshoot && automatic)
        {
            Shoot();
        }

        if(ads && Input.GetMouseButtonDown(1))
        {
            transform.localPosition = adsPos;
            cam.fieldOfView = startFov / adsZoom;
            crosshair.SetActive(!hideCrosshair);
            isAds = true;
        }
        if(ads && Input.GetMouseButtonUp(1))
        {
            transform.localPosition = startPos;
            cam.fieldOfView = startFov;
            crosshair.SetActive(true);
            isAds = false;
        }
    }

    private void Shoot()
    {
        canshoot=false;
        Invoke(nameof(ResetShoot), 1f / fireRate);
        float bulletsShot = 0;
        while(bulletsShot < bulletsToShootAtOnce)
        {
            GameObject bullet = Instantiate(projectile, gunPoint.transform.position, Quaternion.Euler(gunPoint.transform.forward));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if(bullet.GetComponent<Projectile>() != null ) { bullet.GetComponent<Projectile>().damage = damage; }
            if (bullet.GetComponent<ExplosiveProjectile>() != null) { bullet.GetComponent<ExplosiveProjectile>().damage = damage; }
            Vector3 spread = new Vector3(Random.Range(-xSpread, xSpread), Random.Range(-ySpread, ySpread), 0f);
            if(isAds) spread = spread * adsSpreadMultiplyer;
            spread = gunPoint.transform.TransformDirection(spread);
            Vector3 direction = gunPoint.transform.forward + spread;
            rb.AddForce(direction * force, ForceMode.Impulse);
            rb.velocity = rb.velocity + playerRb.velocity;
            Instantiate(shootSound, gunPoint);
            bulletsShot++;
        }
        recoil.StartRecoil(fireRate);
    }

    private void ResetShoot()
    {
        canshoot = true;
    }
}
