using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public GameObject cam;

    public float minRecoil;
    public float maxRecoil;
    public float minAngle;
    public float maxAngle;
    private float recoil;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartRecoil(float fireate)
    {
        recoil = Random.Range(minRecoil, maxRecoil);
        angle = Random.Range(minAngle, maxAngle);
        transform.position -= cam.transform.forward * recoil;
        transform.Rotate(Vector3.left * angle);
        Invoke(nameof(ResetRecoil), (1f/fireate)-((1f/fireate)/2));
    }

    void ResetRecoil()
    {
        transform.position += cam.transform.forward * recoil;
        transform.Rotate(Vector3.right * angle);
    }
}
