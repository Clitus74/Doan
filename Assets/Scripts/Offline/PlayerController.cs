using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    float hInput, vInput;
    
    public Vector3 moveDir ;
    private Rigidbody rb;
    [SerializeField] float sprintSpeed, walkSpeed, jumpForce;
        
    CharacterGroundState charGroundState;
    public float groundDrag;

    [Header("Slope Check")] public float maxSlopeAngle ;
    private RaycastHit slopeHit;
    public bool onSlope;
    public float angleCheck;
    [SerializeField]
    private LayerMask layerMask;
    private void Awake()
    {        
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        MyInput();
        
        //GravityDown();
        
        
    }
    private void FixedUpdate()
    {
        Move();

        if (OnSlope())
        {
            onSlope = true;
            
        }
        else
        {
            onSlope = false;
            
        }
        
        
    }

    public void MyInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
    }
    public void Move()
    {
        moveDir = transform.forward* vInput + transform.right* hInput ;
        rb.AddForce(moveDir.normalized * walkSpeed * 10f, ForceMode.Force);
    }
    
    

    /*public void GravityDown()
    {
        if (grounded)
            rb.AddForce(Vector3.down * 80f,ForceMode.Force);
    }*/

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

    public void GroundSpeedAndDragMultiplier()
    {

    }

    public void SetGroundState(CharacterGroundState _charGroundState)
    {
        charGroundState = _charGroundState;
    }
}
