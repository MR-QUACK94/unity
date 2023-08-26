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

    public float bulletsToShootAtOnce;

    public bool canshoot;

    private void Start()
    {
        canshoot = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canshoot)
        {
            Shoot();
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
            Vector3 spread = new Vector3(Random.Range(-xSpread, xSpread), Random.Range(-ySpread, ySpread), 0f);
            spread = gunPoint.transform.TransformDirection(spread);
            Vector3 direction = gunPoint.transform.forward + spread;
            rb.AddForce(direction * force, ForceMode.Impulse);
            bulletsShot++;
        }
    }

    private void ResetShoot()
    {
        canshoot = true;
    }
}
