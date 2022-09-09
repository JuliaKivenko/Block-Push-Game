using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float appliedForce = 10f;
    private PlayerInputActions playerInputActions;
    private Rigidbody rb;

    private Vector3 direction;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnApplicationQuit()
    {
        playerInputActions.Disable();
    }

    private void Update()
    {
        //get direction to apply force in from player input
        Vector2 input = playerInputActions.Player.Move.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0, input.y);
    }

    private void FixedUpdate()
    {
        //apply force to the ball
        rb.AddForce(direction * appliedForce);
    }
}
