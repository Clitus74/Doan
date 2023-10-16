using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    float hInput, vInput;
    
    public Vector3 moveDir ;
    private Rigidbody rb;
    [SerializeField] float sprintSpeed, walkSpeed,moveSpeed, jumpForce, jumpCD;
    public float groundDrag, speedMultiplier;
    public bool readyToJump = true;
    public bool grounded;

    CharacterGroundState charGroundState;
    CCState ccState;

    

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

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
        SetCCState(CCState.Normal);
    }
    void Update()
    {
        MyInput();
        GroundSpeedAndDragMultiplier();
        CCStateManager();
    }

    
    private void FixedUpdate()
    {
        Move();
              
    }

    public void MyInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && grounded && readyToJump)
        {
            Jump();
        }
            
    }
    public void Move()
    {
        moveDir = transform.forward* vInput + transform.right* hInput ;
        moveSpeed = walkSpeed * speedMultiplier ;
        rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
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

    public void GroundSpeedAndDragMultiplier()
    {
        switch (charGroundState)
        {            
            case CharacterGroundState.Ground:
                {
                    grounded = true;
                    rb.drag = groundDrag;
                    speedMultiplier = 1f;
                    break;
                }
                
            case CharacterGroundState.Airborne:
                {
                    grounded = false;
                    rb.drag = 0;
                    speedMultiplier = 0.8f;
                    break;
                }
            case CharacterGroundState.Ice:
                {
                    grounded = true;
                    rb.drag = 0;
                    speedMultiplier = 1f;
                    break;
                }
            case CharacterGroundState.Swamp:
                {
                    grounded = true;
                    rb.drag = groundDrag * 1.25f;
                    speedMultiplier = 0.6f;
                    break;
                }
        }

    }
    public void SetGroundState(CharacterGroundState _charGroundState)
    {
        charGroundState = _charGroundState;
    }

    public void SetCCState(CCState _ccState)
    {
        ccState = _ccState;
    }

    private void SpeedControll()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y ,limitedVel.z);
        }
    }

    private void Jump()
    {
        
       
            readyToJump = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            Invoke(nameof(ResetJump), jumpCD);
        
        
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
    public void CCStateManager()
    {
        switch(ccState)
        {
            case CCState.Normal:
                {
                    SpeedControll();
                    break;
                }
        }
    }
}
