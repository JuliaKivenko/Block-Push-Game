using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") GameManager.Instance.ChangeState(GameState.Lose);

        other.gameObject.SetActive(false);

    }
}
