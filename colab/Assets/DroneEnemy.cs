using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemy : MonoBehaviour, IDamageable
{
    [Header("References")]
    public Rigidbody rb;
    public Transform targetObject;

    [Header("Movement Settings")]
    public float speed;
    public float flyHeight;
    public float flyHeightRandomOffset;
    public float upForce;
    public float torqueForce;
    public float maxRollAngle = 15.0f;
    [Range(0f, 1f)]
    public float centerSpeed;
    public float maxSpeed;
    public bool shouldMove;
    public float timeBetweenMoves;
    public Vector3 randomPos;
    public float maxAnglularVelocity;

    [Header("Shoot Settings")]
    public float fireRate;
    public GameObject projectile;
    public Transform gunPoint;
    private bool canShoot;
    public float shootAngle;
    public float shootForce;
    public float bulletsToShootAtOnce;
    public int damage;

    [Header("Move Settings")]
    public float maxRangeMove;

    public int health;
    public int maxHealth;
    public Material glowMat;
    public Material deadMat;
    private int[] matIndexes;
    private bool alive;
    public GameObject deathEffect;
    public float deathScaleDownSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canShoot = true;
        shouldMove = true;
        alive = true;
        randomPos = transform.position + new Vector3(Random.Range(-maxRangeMove, maxRangeMove), 0f, Random.Range(-maxRangeMove, maxRangeMove));
        flyHeight = flyHeight + Random.Range(-flyHeightRandomOffset, flyHeightRandomOffset);
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && alive)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (!alive) return;

        if (Physics.Raycast(transform.position, Vector3.down, flyHeight))
        {
            rb.AddForce(Vector3.up * upForce, ForceMode.Force);
        }

        if(targetObject != null)
        {
            Rotate();
        }

        Vector3 directionToTarget = targetObject.position - transform.position;
        float angle = Vector3.Angle(directionToTarget, transform.forward);
        angle = Mathf.Abs(angle);

        if (angle <= shootAngle && canShoot)
        {
            Shoot();
        }

        if (shouldMove)
        {
            Move();
        }

        if(rb.angularVelocity.magnitude > maxAnglularVelocity)
        {
            rb.angularVelocity = rb.angularVelocity.normalized * maxAnglularVelocity;
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
            bullet.GetComponent<Projectile>().damage = damage;
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

    void Rotate()
    {
        Vector3 directionToTarget = targetObject.position - transform.position;
        float angle = Vector3.Angle(directionToTarget, transform.forward);
        angle = Mathf.Abs(angle);

        // Calculate the rotation axis (cross product between forward and target direction).
        Vector3 rotationAxis = Vector3.Cross(transform.forward, directionToTarget);

        // Apply torque to the Rigidbody to make it face the target.
        rb.AddTorque(rotationAxis * torqueForce * angle, ForceMode.Force);

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f));

        if(angle < 0.5f && rb.angularVelocity.magnitude > 0f )
        {
            rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, centerSpeed);
        }
    }

    void Move()
    {
        Vector3 direction = randomPos - transform.position;
        float distance = Vector3.Distance(new Vector3(randomPos.x, transform.position.y, randomPos.z), transform.position);
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(direction * speed, ForceMode.Force);
        }
        if (distance < 1f)
        {
            randomPos = transform.position + new Vector3(Random.Range(-maxRangeMove, maxRangeMove), 0f, Random.Range(-maxRangeMove, maxRangeMove));
        }
    }

    public void Damage(int _damage)
    {
        health = health - _damage;
        Debug.Log(health);
    }

    void Die()
    {
        alive = false;
        rb.constraints = RigidbodyConstraints.None;
        MeshRenderer[] mrs = GetComponentsInChildren<MeshRenderer>();
        Debug.Log(mrs.Length);
        if (glowMat != null && deadMat != null)
        {
            foreach (MeshRenderer mr in mrs)
            {
                Debug.Log(mr.transform.name);
                for (int i = 0; i < mr.sharedMaterials.Length; i++)
                {
                    Debug.Log(mr.sharedMaterials[i] + " || " + glowMat);
                    if (mr.sharedMaterials[i] == glowMat)
                    {
                        Debug.Log("Glow Mat Found");
                        var materialsCopy = mr.materials;
                        materialsCopy[i] = deadMat;
                        mr.materials = materialsCopy;
                    }
                }
            }
        }
        if(deathEffect != null)
        {
            Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity), 10f);
        }
        transform.localScale = Vector3.zero;
    }
}
