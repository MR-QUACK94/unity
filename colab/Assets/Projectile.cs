using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject destroyEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Projectile")
        {
            Debug.Log("Bullet Hit || Normal: " + collision.contacts[collision.contacts.Length - 1].normal);
            Instantiate(destroyEffect, collision.contacts[collision.contacts.Length - 1].point, Quaternion.LookRotation(collision.contacts[collision.contacts.Length - 1].normal));
            Destroy(gameObject);
        }
    }
}
