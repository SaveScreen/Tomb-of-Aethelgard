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
        
    }
}
