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
    public float WallRunTimer;

    [Header("inputs")]
    private float horizontalInput;
    private float verticalInput;

    [Header("Detections")]
    public float WallCheckDistance;
    public float MinJumpHeight;
    private RaycastHit LeftWall;
    private RaycastHit RightWall;
    private bool WallIsRight;
    private bool WallIsLeft;

    [Header("refrences")]
    public Transform orientation;
    private PlayerScript PS;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PS = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
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
        if ((WallIsRight || WallIsLeft) && verticalInput > 0 && AboveGround())
        {
            //wallrunning script here
        }
    }
}
