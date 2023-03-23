using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("WallRunning")]

    public LayerMask IsThisWall;
    public LayerMask IsThisGround;
    public float WallRunForce;
    public float MaxWallRunTime;
    private float WallRunTimer;

    [Header("Exiting Wallrun")]

    public float WallJumpUpForce;
    public float WallJumpSideForce;

    [Header("inputs")]
    public KeyCode WallJumpKey = KeyCode.Space;
    private float horizontalInput;
    private float verticalInput;

    [Header("Detections")]
    public float WallCheckDistance;
    public float MinJumpHeight;
    private RaycastHit LeftWall;
    private RaycastHit RightWall;
    public bool WallIsRight;
    public bool WallIsLeft;

    [Header("refrences")]
    public Transform orientation;
    private PlayerScript PS;
    private CharacterController CC;

    [Header("Exiting Wallrun")]
    public bool ExitingWall;
    public float ExitWallTime;
    private float ExitWallTimer;
    // Start is called before the first frame update
    void Start()
    {
        //CC = GetComponent<CharacterController>();
        PS = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate()
    {
          if (PS.wallrunning)
            WallRunMove();
    }

    private void CheckForWall() 
    {
        WallIsRight = Physics.Raycast(transform.position, orientation.right, out RightWall, WallCheckDistance, IsThisWall);
      
        WallIsLeft = Physics.Raycast(transform.position, -orientation.right, out LeftWall, WallCheckDistance, IsThisWall);
        
    }
    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, MinJumpHeight, IsThisGround);

    }

    private void StateMachine()
    {
        //grabs the input from the old inputs, needs more testing if there will be complexcations
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        //state 1 - wall running
        if ((WallIsRight || WallIsLeft) && verticalInput > 0 && AboveGround() && !ExitingWall)
        {
            if (!PS.wallrunning)
            {
                BeginWallRun();
            }
            //wall run timer
            if (WallRunTimer > 0)
            {
                WallRunTimer -= Time.deltaTime;
            }
            if (WallRunTimer <= 0 && PS.wallrunning)
            {
                ExitingWall= true;
                ExitWallTimer = ExitWallTime;
            }

            //wall jumping 
            if (Input.GetKeyDown(WallJumpKey)) WallJump();
        }

        //state 2 - exiting wall run
        else if (ExitingWall)
        {
            if (PS.wallrunning)
            {
                EndWallRun();
            }

            if (ExitWallTimer > 0)
            {
                ExitWallTimer -= Time.deltaTime;
            }

            if (ExitWallTimer <= 0)
            {
                ExitingWall = false;
            }
        }

        //state 3 - none
        else
        {
            if (PS.wallrunning)
            {
                EndWallRun();
            }

        }
    }


    private void BeginWallRun()
    {
        PS.wallrunning = true;
        WallRunTimer = MaxWallRunTime;
    }

    private void WallRunMove()
    {


        PS.velocity = new Vector3(PS.velocity.x, 0f, PS.velocity.z);

        Vector3 wallNormal = WallIsRight ? RightWall.normal : LeftWall.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        //adds force when wall running
        //PS.AddForce(wallForward * WallRunForce, ForceMode.Force);
        
       // Debug.Log("wall running");
    }
    private void EndWallRun() 
    {
        PS.wallrunning = false;
        //PS.velocity = new Vector3(0f, 0f, 0f);
    }
    private void WallJump()
    {
        Vector3 wallNormal = WallIsRight ? RightWall.normal : LeftWall.normal;

        Vector3 ForceToApply = transform.up * WallJumpUpForce + wallNormal * WallJumpSideForce;
        //resets the y velocity adds the force
        //PS.velocity = new Vector3(PS.velocity.x, 0f, PS.velocity.z);
        PS.AddVelocity(ForceToApply);
    }
}
