using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    PlayerInputAction input;
    private Vector3 moveDir;
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    Vector3 moveAmount;
    Vector2 moveValue;
    Vector3 smoothMove;
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

        Move();
        
        
        
    }
    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.deltaTime    );
        rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);
    }
    public void Move()
    {
        moveValue = input.Player.Move.ReadValue<Vector2>();
        moveDir = transform.forward * moveValue.y + transform.right * moveValue.x;

    }
    
}
