using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerInputAction input;
    private Vector2 moveDir = Vector2.zero;
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    private void Awake()
    {
        input = new PlayerInputAction();
        rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    private void OnEnable()
    {
        input.Enable();
    }

    void Start()
    {
        
    }

    void Update()
    {
        moveDir = input.Player.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.y * speed);
    }
}
