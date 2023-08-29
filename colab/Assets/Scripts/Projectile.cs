using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject destroyEffect;
    public Rigidbody rb;
    public float maxLifeSpan = 30f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Projectile")
        {
            Debug.Log("Bullet Hit || Normal: " + collision.contacts[collision.contacts.Length - 1].normal);
            GameObject de = Instantiate(destroyEffect, collision.contacts[collision.contacts.Length - 1].point, Quaternion.LookRotation(collision.contacts[collision.contacts.Length - 1].normal));
            Destroy(de, 5f);
            if (collision.transform.GetComponent<Rigidbody>() != null) 
            {
                Rigidbody hitRb = collision.transform.GetComponent<Rigidbody>();
                Vector3 direction = collision.contacts[collision.contacts.Length - 1].point - hitRb.velocity.normalized;
                float momentum = rb.velocity.magnitude * rb.mass;
                Debug.Log(momentum);
                float force = momentum / hitRb.mass;
                force = force / 4f;
                Vector3 interpolatedDirection = Vector3.Lerp(direction.normalized * force, hitRb.velocity, 0.5f);
                hitRb.AddForce(direction * force, ForceMode.Impulse);
            }
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Invoke(nameof(Destroy), maxLifeSpan);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
