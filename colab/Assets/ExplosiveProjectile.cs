using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class ExplosiveProjectile : MonoBehaviour
{
    public GameObject destroyEffect;
    public Rigidbody rb;
    public float maxLifeSpan = 30f;
    public int damage;
    public float radius;
    private bool hit;
    public float knockback;
    public List<GameObject> hitAlready;
    public List<GameObject> hitAlreadyRb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Projectile" && !hit && collision.gameObject.tag != "Player")
        {
            hit = true;
            //Debug.Log("Bullet Hit || Normal: " + collision.contacts[collision.contacts.Length - 1].normal);
            
            GameObject de = Instantiate(destroyEffect, collision.contacts[collision.contacts.Length - 1].point, Quaternion.LookRotation(collision.contacts[collision.contacts.Length - 1].normal));
            Destroy(de, 5f);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            //Debug.Log(hitColliders.Length);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.transform != transform) 
                {
                    //Debug.Log(hitCollider.transform.name);
                    if (hitCollider.transform.GetComponentInParent<IDamageable>() != null)
                    {
                        bool _hitAlready = false;

                        foreach (GameObject t in hitAlready)
                        {
                            if (t.gameObject == hitCollider.transform.GetComponentInParent<IDamageable>().GetGameObject())
                            {
                                _hitAlready = true;
                            }
                        }

                        if (!_hitAlready)
                        {
                            float distance = Mathf.Abs(Vector3.Distance(transform.position, hitCollider.transform.position));
                            float damageToApply = (distance/radius);
                            damageToApply = 1f - damageToApply;
                            damageToApply = damageToApply * damage;
                            damageToApply = Mathf.Clamp(damageToApply, 0.0f, damage);
                            hitCollider.transform.GetComponentInParent<IDamageable>().Damage(Mathf.RoundToInt(damageToApply));
                            hitAlready.Add(hitCollider.transform.GetComponentInParent<IDamageable>().GetGameObject());
                            Debug.Log("Applied " + damageToApply.ToString() + " damage from " + hitCollider.transform.name + " to " + hitCollider.transform.root.name + " || Distance: " + distance);
                            /*foreach(GameObject t in hitAlready)
                            {
                                string st = "";
                                st = st + " || " + t;
                                Debug.Log(st);
                            */
                        
                        }
                    }
                    if (hitCollider.transform.GetComponentInParent<Rigidbody>() != null)
                    {
                        bool _hitAlreadyRb = false;

                        foreach (GameObject t in hitAlreadyRb)
                        {
                            if (t.gameObject == hitCollider.transform.GetComponentInParent<Rigidbody>().gameObject)
                            {
                                _hitAlreadyRb = true;
                            }
                        }

                        if (!_hitAlreadyRb)
                        {
                            //Debug.Log("Has Rigidbody");
                            float distance = Mathf.Abs(Vector3.Distance(transform.position, hitCollider.transform.position));
                            float force = (distance / radius);
                            force = 1f - force;
                            force = force * knockback;
                            Vector3 direction = hitCollider.transform.position - transform.position;
                            Debug.DrawRay(direction, hitCollider.transform.position, Color.red, 10f);
                            hitCollider.transform.GetComponentInParent<Rigidbody>().AddForce(direction.normalized * force, ForceMode.Impulse);
                        }
                    }
                }
            }
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        hit = false;
        Invoke(nameof(Destroy), maxLifeSpan);
    }
}
