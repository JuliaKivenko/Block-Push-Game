using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float pushStrength;
    private Vector3 pushDirestion;


    private void OnCollisionEnter(Collision other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (other.gameObject.GetComponent<PositiveObstacle>())
        {
            rb.AddForce(pushDirestion * pushStrength);
            other.gameObject.GetComponent<PositiveObstacle>().SetPushDirectionAndForce(pushDirestion, pushStrength);
        }

        if (other.gameObject.tag == "Hazard")
        {
            gameObject.SetActive(false);
            GameManager.Instance.ChangeState(GameState.Lose);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Point")
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.AddPoints();
            Debug.Log(GameManager.Instance.points);
        }
    }

    public void SetPushDirection(Vector3 direction)
    {
        if (direction != Vector3.zero)
            pushDirestion = direction;
    }
}
