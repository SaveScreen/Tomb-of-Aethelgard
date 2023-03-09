using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //Movement variables
    public InputAction playermove;
    private CharacterController charactercontroller;

    //Camera reference
    public GameObject cam;
    private CameraScript camerascript;

    //Jumping variables
    public InputAction playerjump;
    public float jumpspeed;
    private bool jumped;
    private bool jumping;
    private bool isfalling;

    //Character movement
    public float speed;
    public float rotationspeed;
    private Vector3 move;
    private Vector3 movement;
    public Vector3 velocity;
    public float gravity;
    public float wallrunspeed;

    //Looking variables
    public InputAction playerrotate;
    public float sensitivity;
    private Vector2 look;
    private float lookx;
    //private float looky;
    //private float rotationx;
    private float rotationy;
    public Transform orientation;
    private bool moving;
    

    //movement states if we want the player to be able to run, crouch, slide etc.
    public MovementState state;
    public enum MovementState
    {
        wallrunning,
        walking
    }
    public bool wallrunning;
    //*******************************************************************************************************
    //!!!CHECK THIS IN INSPECTOR TO TURN LOCK PLAYER ORIENTATION TO CAMERA ORIENTATION WHEN MOVING FORWARD!!!

    //NOTE: THE PLAYER WILL REVERT TO IT'S ORIGINAL ORIENTATION UPON RELEASE OF THE CAMERALOCK
    
    public bool tiedtocamera;

    //*******************************************************************************************************

    // Start is called before the first frame update
    void Start()
    {
        camerascript = cam.GetComponent<CameraScript>();
        charactercontroller = gameObject.GetComponent<CharacterController>();
        moving = false;
        isfalling = false;
        jumped = false;
        jumping = false;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       
    }

    private void OnEnable()
    {
        playermove.Enable();
        playerrotate.Enable();
        playerjump.Enable();
    }

    private void OnDisable()
    {
        playermove.Disable();
        playerrotate.Disable();
        playerjump.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        StateHandler();
        move = playermove.ReadValue<Vector3>();
        jumping = playerjump.IsPressed();

        //Determines if player is moving
        if (movement.x > 0 || movement.z > 0 || movement.x < 0 || movement.z < 0) {
            moving = true;

        }
        if (movement.x == 0 || movement.z == 0) {
            moving = false;
        }

        //Only look around if camera is not currently turning
        if (camerascript.cameralocked == false) {    
            Look();    
            
        }
        if (camerascript.cameralocked == true) {
            if (tiedtocamera == true) {
                if (moving) {
                    LookDirectionOfCamera();
                }
            }
                
        }

        if (jumping == true && charactercontroller.isGrounded) {
            jumped = true;
        }

        if (jumped == true) {
            if (!isfalling) {
                velocity.y = jumpspeed;
                isfalling = true;
                
            }
            else {
                if (charactercontroller.isGrounded == true) {
                    isfalling = false;
                    jumped = false;
                }
            }
        }   
           

        //Debug feature for quitting the game
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }



    }

    private void FixedUpdate()
    {
        movement = (move.z * transform.forward) + (move.x * transform.right);
        movement.y = 0.0f;

        charactercontroller.Move(movement * speed * Time.deltaTime);
        
        velocity.y += gravity * Time.deltaTime;
        charactercontroller.Move(velocity * Time.deltaTime); 
        
    }


    //Controls player looking around
    private void Look() {
        look = playerrotate.ReadValue<Vector2>();

        lookx = look.x * sensitivity * Time.deltaTime;
        //looky = look.y * sensitivity * Time.deltaTime;

        //rotationx -= looky;
        rotationy += lookx;

        //rotationx = Mathf.Clamp(rotationx,-90.0f,90.0f);
        transform.rotation = Quaternion.Euler(0,-rotationy,0);
        orientation.transform.Rotate(Vector3.up * lookx);
    }

    //When variable "tiedtocamera" is checked, this locks the player's orientation with the camera orientation
    void LookDirectionOfCamera() {
        
        transform.rotation = Quaternion.Euler(0,-camerascript.rotationy,0);
        orientation.transform.Rotate(Vector3.up * camerascript.lookx);
    }

    private void StateHandler()
    {
        //mode-wallrunning
        if (wallrunning)
        {
            state = MovementState.wallrunning;
            //speed = wallrunspeed;
        }

        //mode-walking
        if (charactercontroller.isGrounded)
        {
            state = MovementState.walking;
            
        }
    }
}
