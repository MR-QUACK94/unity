using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemy : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float flyHeight;
    public float upForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, flyHeight))
        {
            rb.AddForce(Vector3.up * upForce, ForceMode.Force);
        }
    }
}
