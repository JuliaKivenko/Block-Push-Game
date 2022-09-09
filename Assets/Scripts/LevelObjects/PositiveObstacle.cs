using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositiveObstacle : MonoBehaviour
{
    private Vector3 pushDirestion;
    private float pushStrength;

    private void OnCollisionEnter(Collision other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (other.gameObject.tag == "Neutral Obstacle" || other.gameObject.tag == "Hazard Obstacle")
        {
            rb.AddForce(pushDirestion * pushStrength * rb.mass);
        }
    }

    public void SetPushDirectionAndForce(Vector3 direction, float pushForce)
    {
        if (direction != Vector3.zero)
            pushDirestion = direction;
        pushStrength = pushForce;
    }
}
