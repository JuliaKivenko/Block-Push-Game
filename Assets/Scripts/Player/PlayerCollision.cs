using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float pushStrength;
    private Vector3 pushDirestion;

    private Rigidbody playerRB;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (other.gameObject.GetComponent<PositiveObstacle>())
        {
            rb.AddForce(playerRB.velocity * pushStrength);
            other.gameObject.GetComponent<PositiveObstacle>().SetPushStrength(pushStrength);
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
        }
    }
}
