using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject projectile;
    public GameObject target;
    public float fireRate;
    public float shootForce;
    public float bulletsToShootAtOnce;
    public Transform gunPoint;
    public float turnSpeed;
    public float maxRange;
    public Transform yawObject;
    public Transform pitchObject;
    public bool canShoot;
    public float shootAngle;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 targetDirection = target.transform.position - gunPoint.position;

            // Calculate yaw rotation
            float yawAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
            Quaternion yawRotation = Quaternion.Euler(0f, yawAngle, 0f);
            Quaternion currentYawRotation = Quaternion.Euler(0f, yawObject.localEulerAngles.y, 0f);

            // Calculate pitch rotation
            float pitchAngle = -Mathf.Asin(targetDirection.y / targetDirection.magnitude) * Mathf.Rad2Deg;
            Quaternion pitchRotation = Quaternion.Euler(pitchAngle, 0f, 0f);
            Quaternion currentPitchRotation = Quaternion.Euler(pitchObject.localEulerAngles.x, 0f, 0f);

            // Apply the rotations to yaw and pitch objects smoothly
            yawObject.localRotation = Quaternion.RotateTowards(currentYawRotation, yawRotation, turnSpeed);
            pitchObject.localRotation = Quaternion.RotateTowards(currentPitchRotation, pitchRotation, turnSpeed);
        }

        Vector3 targetDirection2 = target.transform.position - gunPoint.position;
        float angle = Vector3.Angle(gunPoint.forward, targetDirection2);
        angle = Mathf.Abs(angle);

        Debug.DrawRay(gunPoint.position, gunPoint.forward * maxRange, Color.red);
        if(canShoot && angle <= shootAngle)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        canShoot = false;
        Invoke(nameof(ResetShoot), 1f / fireRate);
        float bulletsShot = 0;
        while (bulletsShot < bulletsToShootAtOnce)
        {
            GameObject bullet = Instantiate(projectile, gunPoint.transform.position, Quaternion.Euler(gunPoint.transform.forward));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            Vector3 direction = gunPoint.transform.forward;
            rb.AddForce(direction * shootForce, ForceMode.Impulse);
            //Instantiate(shootSound, gunPoint);
            bulletsShot++;
        }
    }

    void ResetShoot()
    {
        canShoot = true;
    }
}
