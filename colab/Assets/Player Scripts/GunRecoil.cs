using System;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    public Transform gunTransform;
    public Transform cam;
    public float backwardSpeed = 2.0f; // Speed of moving the gun backward.
    public float forwardSpeed = 5.0f; // Speed of moving the gun forward.
    public float backwardDistance = 0.1f; // Distance to move the gun backward.

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMovingBackward = false;
    private bool isMovingForward = false;

    void Start()
    {
        originalPosition = gunTransform.localPosition;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartRecoil();
        }
        if(Input.GetMouseButtonUp(0))
        {
            StopRecoil();
        }

        gunTransform.localPosition = targetPosition;
    }

    public void StartRecoil()
    {
        isMovingBackward = true;
        targetPosition = gunTransform.position - (gunTransform.forward * backwardDistance);
    }

    public void StopRecoil()
    {
        targetPosition = originalPosition;
    }
}
