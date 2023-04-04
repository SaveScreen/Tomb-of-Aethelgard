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
    //private CameraScript camerascript;
    public bool IsPushingPulling;

    [Header("Jumping Variables")]
    public InputAction playerjump;
    public float jumpspeed;
    public float jumpgravity;
    private bool jumped;
    private bool jumping;
    private bool isfalling;

    [Header("Character Movement")]
    public float speed;
    private Vector3 move;
    public Vector3 velocity;
    private float gravity;
    

    [Header("Looking Variable")]
    public Transform orientation;

    //**********************************************
    //ONLY CHECK THIS BOX IF USING A CONTROLLER
    public bool usingcontroller;

    //**********************************************

    public GameObject cutscene;
    private CutsceneScript cutscenescript;

    private float smoothrotationtime;
    private float smoothrotationvelocity;
    private Vector3 direction;
    private Vector3 movedir;
    
    [Header("Footsteps Audio")]
    public AudioSource footsteps;
    private GameObject pauseMenu;
    public static bool paused = false;

    //movement states if we want the player to be able to run, crouch, slide etc.
    public MovementState state;
    public enum MovementState
    {
        wallrunning,
        walking,
        idle
    }
    public bool wallrunning;

    // Start is called before the first frame update
    void Start()
    {
        //camerascript = cam.GetComponent<CameraScript>();
        charactercontroller = gameObject.GetComponent<CharacterController>();
        cutscenescript = cutscene.GetComponent<CutsceneScript>();
        isfalling = false;
        jumped = false;
        jumping = false;
        smoothrotationtime = 0.1f;
        gravity = -8f;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        IsPushingPulling = false;

        //pause menu panel
        pauseMenu = GameObject.Find("Pause Panel");
        pauseMenu.SetActive(false);

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
        if(!paused){
            StateHandler();
            move = playermove.ReadValue<Vector3>();

            if (charactercontroller.isGrounded)
            {
                jumping = playerjump.IsPressed();
            }
            
            LookAndMove();

            if (jumping == true) {
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
        }
        //Pause and unpause
            if(Input.GetKeyDown(KeyCode.Escape)){
                if(paused){
                    //unpause
                    ResumeGame();
                } else{
                    //pause
                    PauseGame();
                }
            }
    }

    private void FixedUpdate()
    {
        if (direction.magnitude >= 0.1f) {
            charactercontroller.Move(movedir.normalized * speed * Time.deltaTime); 
        }

        //General gravity is used if player is not jumping.
        if (!jumped && !charactercontroller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        if (jumped)
        {
            velocity.y += jumpgravity * Time.deltaTime;
        }

        charactercontroller.Move(velocity * Time.deltaTime); 
        
    }

    //Controls player looking around
    private void LookAndMove() {
        direction = new Vector3(move.x,0,move.z).normalized;

        if (direction.magnitude >= 0.1f) {
            float targetangle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetangle, ref smoothrotationvelocity, smoothrotationtime);

            if (IsPushingPulling !=true) 
            { 
                transform.rotation = Quaternion.Euler(0, angle, 0); 
            } 
            orientation.transform.Rotate(Vector3.up * angle);
            movedir = Quaternion.Euler(0,targetangle,0) * Vector3.forward;
        }

    }

    /*************************************************************************************
    private void Look() {
        ****************************************
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
        
    }

    *****************************************************************************************/

    private void StateHandler()
    {
        //mode-wallrunning
        if (wallrunning)
        {
            state = MovementState.wallrunning;
            footsteps.Stop();
        }

        //mode-walking
        if (charactercontroller.isGrounded)
        {
            //If player is not moving, footsteps sound stops playing
            if (direction.magnitude >= 0.1f) {
                state = MovementState.walking;
                velocity.x = 0f;

                if(!footsteps.isPlaying){
                    footsteps.Play();
                }
            }
            else {

                footsteps.Stop();
            }
            
        }
    }
    //this function is for adding force when wall jumping
    public void AddVelocity(Vector3 velocity)
    {
        this.velocity += velocity;
    }
    
    private void PauseGame(){
        Time.timeScale = 0;
        paused = true;
        pauseMenu.SetActive(true);
    }

    private void ResumeGame(){
        Time.timeScale = 1.0f;
        paused = false;
        pauseMenu.SetActive(false);
    }

}
