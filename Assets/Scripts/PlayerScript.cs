using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement Variables")]
    public InputAction playermove;
    private CharacterController charactercontroller;

    [Header("Camera Refrence")]
    public GameObject cam;
    private CameraScript camerascript;

    [Header("Jumping Variables")]
    public InputAction playerjump;
    public float jumpspeed;
    private bool jumped;
    private bool jumping;
    private bool isfalling;

    [Header("Character Movement")]
    public float speed;
    private Vector3 move;
    public Vector3 velocity;
    public float gravity;
    

    [Header("Looking Variable")]
    public Transform orientation;

    //**********************************************
    //ONLY CHECK THIS BOX IF USING A CONTROLLER
    public bool usingcontroller;

    //**********************************************

    private float smoothrotationtime;
    private float smoothrotationvelocity;
    private Vector3 direction;
    private Vector3 movedir;
    

    //movement states if we want the player to be able to run, crouch, slide etc.
    public MovementState state;
    public enum MovementState
    {
        wallrunning,
        walking
    }
    public bool wallrunning;

    // Start is called before the first frame update
    void Start()
    {
        camerascript = cam.GetComponent<CameraScript>();
        charactercontroller = gameObject.GetComponent<CharacterController>();
        isfalling = false;
        jumped = false;
        jumping = false;
        smoothrotationtime = 0.1f;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       
    }

    private void OnEnable()
    {
        playermove.Enable();
        playerjump.Enable();
    }

    private void OnDisable()
    {
        playermove.Disable();
        playerjump.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        StateHandler();
        move = playermove.ReadValue<Vector3>();
        jumping = playerjump.IsPressed();
        Look();

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
        if (direction.magnitude >= 0.1f) {
            charactercontroller.Move(movedir.normalized * speed * Time.deltaTime); 
        }
               
        
        velocity.y += gravity * Time.deltaTime;
        charactercontroller.Move(velocity * Time.deltaTime); 
        
    }


    //Controls player looking around
    private void Look() {
        /****************************************
        OLD LOOK/ROTATION SYSTEM
        *****************************************

        look = playerrotate.ReadValue<Vector2>();

        lookx = look.x * sensitivity * Time.deltaTime;
        looky = look.y * sensitivity * Time.deltaTime;

        rotationx -= looky;
        rotationy += lookx;

        rotationx = Mathf.Clamp(rotationx,-90.0f,90.0f);
        transform.rotation = Quaternion.Euler(0,-rotationy,0);
        orientation.transform.Rotate(Vector3.up * lookx);
        */

        direction = new Vector3(move.x,0,move.z).normalized;

        if (direction.magnitude >= 0.1f) {
            float targetangle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetangle, ref smoothrotationvelocity, smoothrotationtime);

            
            transform.rotation = Quaternion.Euler(0,angle,0);
            orientation.transform.Rotate(Vector3.up * angle);
            movedir = Quaternion.Euler(0,targetangle,0) * Vector3.forward;
        }

    }

    private void StateHandler()
    {
        //mode-wallrunning
        if (wallrunning)
        {
            state = MovementState.wallrunning;
           
        }

        //mode-walking
        if (charactercontroller.isGrounded)
        {
            state = MovementState.walking;
            velocity.x = 0f;
        }
    }
    //this function is for adding force when wall jumping
    public void AddVelocity(Vector3 velocity)
    {
        this.velocity += velocity;
    }
    
}
