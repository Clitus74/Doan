using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    PlayerInputAction input;
    private Vector3 moveDir;
    private Rigidbody rb;
    [SerializeField] float sprintSpeed, walkSpeed, jumpForce, smoothTime;
    Vector3 moveAmount;
    Vector2 moveValue;
    Vector3 smoothMove;
    bool grounded;
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
        GravityDown();
        
        
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime    );
        
    }
    public void Move()
    {

        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed: walkSpeed), ref smoothMove,smoothTime) ;
        if(Input.GetKey(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }
    
    public void SetGroundedState (bool _grounded)
    {
        grounded = _grounded;
    }

    public void GravityDown()
    {
        if (grounded)
            rb.AddForce(Vector3.down * 80f,ForceMode.Force);
    }
}
