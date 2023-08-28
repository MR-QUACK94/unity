using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject destroyEffect;
    public Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Projectile")
        {
            Debug.Log("Bullet Hit || Normal: " + collision.contacts[collision.contacts.Length - 1].normal);
            Instantiate(destroyEffect, collision.contacts[collision.contacts.Length - 1].point, Quaternion.LookRotation(collision.contacts[collision.contacts.Length - 1].normal));
            if (collision.transform.GetComponent<Rigidbody>() != null) 
            {
                Rigidbody hitRb = collision.transform.GetComponent<Rigidbody>();
                Vector3 direction = collision.contacts[collision.contacts.Length - 1].point - hitRb.velocity.normalized;
                float momentum = rb.velocity.magnitude * rb.mass;
                float force = momentum / hitRb.mass;
                Vector3 interpolatedDirection = Vector3.Lerp(direction.normalized * force, hitRb.velocity, 0.5f);
                hitRb.AddForce(interpolatedDirection, ForceMode.Impulse);
            }
            Destroy(gameObject);
        }
    }
}
