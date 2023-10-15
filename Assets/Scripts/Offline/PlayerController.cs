using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    PlayerInputAction input;
    
    public Vector3 moveDir ;
    private Rigidbody rb;
    [SerializeField] float sprintSpeed, walkSpeed, jumpForce, smoothTime;
    Vector3 moveAmount;
    
    Vector3 smoothMove;
    bool grounded;

    [Header("Slope Check")] public float maxSlopeAngle ;
    private RaycastHit slopeHit;
    public bool onSlope;
    public float angleCheck;
    [SerializeField]
    private LayerMask layerMask;
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
        
        if(OnSlope())
        {
            onSlope = true;
            rb.MovePosition(rb.position + transform.TransformDirection(GetSlopeMoveDirection())*Time.deltaTime);
        }
        else
        {
            onSlope = false;
            rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        }
        
        
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

    public bool OnSlope()
    {
        
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit , 4f, ~layerMask))
        {
            Debug.DrawRay(transform.position,Vector3.down *3f,Color.red);
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            angleCheck = angle;
            return angle < maxSlopeAngle && angle != 0;
            
            
            
            
        }
        
        return false;
    }

    public Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDir, slopeHit.normal).normalized;
    }
}
