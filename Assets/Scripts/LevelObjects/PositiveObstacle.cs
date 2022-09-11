using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositiveObstacle : MonoBehaviour
{
    private Vector3 pushDirestion;
    private float pushStrength;

    private Rigidbody objectRb;

    private void Start()
    {
        objectRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (other.gameObject.tag == "Neutral Obstacle" || other.gameObject.tag == "Hazard Obstacle")
        {
            rb.AddForce(objectRb.velocity * pushStrength * rb.mass);
        }
    }

    public void SetPushStrength(float strength) => pushStrength = strength;
}
