using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float appliedForce = 10f;
    private PlayerInputActions playerInputActions;
    private Rigidbody rb;
    private PlayerCollision playerCollision;

    private Vector3 direction;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        rb = GetComponent<Rigidbody>();
        playerCollision = GetComponent<PlayerCollision>();
    }
    private void OnEnable()
    {
        playerInputActions.Enable();

        GameManager.onGameStateChange += ResetPlayer;
    }

    private void OnApplicationQuit()
    {
        playerInputActions.Disable();

        GameManager.onGameStateChange -= ResetPlayer;
    }

    private void Update()
    {
        if (GameManager.Instance.gameState != GameState.Running)
            return;

        //get direction to apply force in from player input
        Vector2 input = playerInputActions.Player.Move.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0, input.y);

    }

    private void FixedUpdate()
    {
        //apply force to the ball
        rb.AddForce(direction * appliedForce);
    }

    private void ResetPlayer()
    {
        if (GameManager.Instance.gameState != GameState.Starting)
            return;

        direction = Vector3.zero;
        rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        gameObject.SetActive(true);
    }
}
