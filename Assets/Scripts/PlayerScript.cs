using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement Variables")]
    public InputAction playermove;
    public CharacterController charactercontroller;
    public bool isWallRunningOnLeftWall;
    public bool isWallRunningOnRightWall;
    public float legCrunchTime = 1f;

    [Header("Camera Refrence")]
    public GameObject cam;
    //private CinemachineFreeLook flcam;
    //private CameraScript camerascript;
    public bool IsPushingPulling;

    [Header("Jumping Variables")]
    public InputAction playerjump;
    public InputAction JumpAudioPlay;
    private bool JumpPlay;
    public float jumpspeed;
    public float jumpgravity;
    private bool jumped;
    private bool jumping;
    private bool isfalling;

    [Header("Refrence for Rotate mirror")]
    public float DistanceCheck = 1f;
    public LayerMask RotateMirror;
    private RaycastHit RotatingMirror;
    public bool IsrotatingMirror;
    public bool CanInteractRotatingMirror;

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

    //ACTIVATES CUTSCENES
    //public GameObject cutscene;
    //private CutsceneScript cutscenescript;

    private float smoothrotationtime;
    private float smoothrotationvelocity;
    private Vector3 direction;
    private Vector3 movedir;

    [Header("Footsteps Audio")]
    private PlayerSoundsScript PSS;
    public AudioSource footsteps;

    [Header("Pause Input")]
    public InputAction pausing;
    
    public static bool lose = false;
    public bool won = false;
    public bool paused = false;
    //movement states if we want the player to be able to run, crouch, slide etc.
    public MovementState state;
    public enum MovementState
    {
        wallrunning,
        walking
    }
    public bool wallrunning;
    public Animator anim;


    public GameObject winLoseManager;
    private WinLoseScript winLoseScript;

    private RupeeHUDScript rupeeHUD;

    private static int rupees;

    private float legCrunchTimer;
    private bool playerCanMove;

    [Header("Powerups")]
    public GameObject rupeeMagnetZone;
    private static bool rupeeMagnetCollected = false;

    [Header("Player Material")]
    public Material playerMat;
    public Texture2D[] playerTextures;

    void Start()
    {
        //camerascript = cam.GetComponent<CameraScript>();
        charactercontroller = gameObject.GetComponent<CharacterController>();
        //flcam = cam.GetComponent<CinemachineFreeLook>();
        PSS = GetComponent<PlayerSoundsScript>();
        //cutscenescript = cutscene.GetComponent<CutsceneScript>();
        isfalling = false;
        jumped = false;
        jumping = false;
        JumpPlay = false;
        CanInteractRotatingMirror = false;
        isWallRunningOnLeftWall = false;
        isWallRunningOnRightWall= false;
        smoothrotationtime = 0.1f;
        gravity = -3f;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        IsPushingPulling = false;

        anim = GameObject.Find("Breathing_Idle_1").GetComponent<Animator>();

        winLoseScript = winLoseManager.GetComponent<WinLoseScript>();
        rupeeHUD = GameObject.Find("RupeeCounter").GetComponent<RupeeHUDScript>();

        if(rupeeMagnetCollected){
            rupeeMagnetZone.SetActive(true);
        }

        //find player textures
        //string texturePath = "Assets/Materials/Materials/Player Skins";
        //playerTextures = Resources.LoadAll(texturePath, typeof(Texture2D));

    }

    private void OnEnable()
    {
        playermove.Enable();
        playerjump.Enable();
        JumpAudioPlay.Enable();
        pausing.Enable();
        
    }

    private void OnDisable()
    {
        playermove.Disable();
        playerjump.Disable();
        JumpAudioPlay.Disable();
        pausing.Disable();
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= legCrunchTimer){
            playerCanMove = true;
            anim.SetBool("legCrunching", false);
        }

        if(!paused && !won && !lose && playerCanMove){
            StateHandler();
            move = playermove.ReadValue<Vector3>();


            if (charactercontroller.isGrounded)
            {
                jumping = playerjump.IsPressed();
                JumpPlay = JumpAudioPlay.WasPressedThisFrame();
                velocity.x = 0f;
                velocity.z = 0f;
            }
            
            LookAndMove();
            LineForRotate();
            if (jumping == true) {
                jumped = true;
                //anim.SetBool("isJumping", true);
            }
            if (JumpPlay == true)
            {
                PSS.PlayJumpSound();
                
            }
            if (jumped == true) {
                if (!isfalling) {
                    velocity.y = jumpspeed;
                    isfalling = true;
                    JumpPlay = false;
                }
                else {
                    if (charactercontroller.isGrounded == true) {
                    
                        isfalling = false;
                        jumped = false;
                        PSS.PlayLandingSound();

                    }
                }
            }
            
        }

        if (won) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene("VSLevelScene"); //this just needs to change a bit to either the current active scene or to something else
                
                Time.timeScale = 1.0f;
                won = false;
            }
        }
        
        
        if (lose) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //gets the current active scene
                
                Time.timeScale = 1.0f;
                lose = false;
            }
        }

        //Pause and unpause
        if(pausing.WasPressedThisFrame()){
            if(paused){
                //unpause
                winLoseScript.ResumeGame();
            } else{
                //pause
                winLoseScript.PauseGame();
            }
        }

        if (CompletionManagerScript.tutorialcomplete && CompletionManagerScript.level1complete && CompletionManagerScript.level2complete && CompletionManagerScript.level3complete) {
            CompletionManagerScript.allclear = true;
        }

        //Determining what levels have been completed
        if (Input.GetKeyDown(KeyCode.T)) {
            Debug.Log(CompletionManagerScript.tutorialcomplete.ToString());
            Debug.Log(CompletionManagerScript.level1complete.ToString());
            Debug.Log(CompletionManagerScript.level2complete.ToString());
            Debug.Log(CompletionManagerScript.level3complete.ToString());
            Debug.Log(CompletionManagerScript.allclear.ToString());
        }
        
        //Debug for opening the hub door
        /******************************************************
        if (Input.GetKeyDown(KeyCode.O)) {
            CompletionManagerScript.allclear = true;
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            CompletionManagerScript.tutorialcomplete = true;
            CompletionManagerScript.level1complete = true;
            CompletionManagerScript.level2complete = true;
            CompletionManagerScript.level3complete = true;
        }
        *******************************************************/

        //FOR DEBUG PURPOSES | DO NOT REMOVE
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
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

    public bool LineForRotate()
    {
        IsrotatingMirror = Physics.Raycast(transform.position, transform.forward, out RotatingMirror, DistanceCheck, RotateMirror);
        
        if (IsrotatingMirror)
        {
            CanInteractRotatingMirror = true;
            return true;
        }
        else 
        {
            CanInteractRotatingMirror = false;
            return false;
        }
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
        if ((wallrunning) && (!charactercontroller.isGrounded))
        {
            state = MovementState.wallrunning;
            if (!footsteps.isPlaying)
            {
                PSS.PlayWallrunningSound();
            }
            anim.SetBool("isRunning", false);

            if (isWallRunningOnLeftWall)
            {
                anim.SetBool("isWallRunning", true);
                anim.SetBool("isOnLeftWall", true);
                
            }
            if (isWallRunningOnRightWall)
            {
                anim.SetBool("isWallRunning", true);
                anim.SetBool("isOnRightWall", true);
                
            }
        }
       
        //mode-walking
        if (charactercontroller.isGrounded)
        {
            
            //If player is not moving, footsteps sound stops playing
            anim.SetBool("isJumping", false);
            isWallRunningOnRightWall = false;
            anim.SetBool("isOnRightWall", false);
            isWallRunningOnLeftWall = false;
            anim.SetBool("isOnLeftWall", false);
            anim.SetBool("isWallRunning", false);
            if (direction.magnitude >= 0.1f) {
                state = MovementState.walking;
                velocity.x = 0f;

                anim.SetBool("isRunning", true);

                if(!footsteps.isPlaying){
                    PSS.PlayRunSound();
                }
            }
            else {
                anim.SetBool("isRunning", false);
                footsteps.Stop();
            }
            
        }
        if ((!charactercontroller.isGrounded)&& (!wallrunning)) {
            anim.SetBool("isJumping", true);
            anim.SetBool("isOnRightWall", false);
            anim.SetBool("isOnLeftWall", false);
            anim.SetBool("isWallRunning", false);
            footsteps.Stop();
            PSS.StopLandingSound();           
        }
    }
    //this function is for adding force when wall jumping
    public void AddVelocity(Vector3 velocity)
    {
        this.velocity += velocity;
    }
    
    public void QuitGame() {
        Application.Quit();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "WinScreen") {
            WinGame();
        }
       if (other.gameObject.tag == "LoseScreen") {
            LoseGame();
        }
        if (other.gameObject.tag == "LoseScreenAxe")
        {
            LoseGameAxe();
        }
        if (other.gameObject.tag == "LoseScreenSpike")
        {
            LoseGameSpike();
        }
        if(other.gameObject.tag == "LegCrunchBox"){
            legCrunchTimer = Time.time + legCrunchTime;
            playerCanMove = false;
            anim.SetBool("legCrunching", true);
            //Debug.Log("leg crunching");
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "MagnetPowerup"){
            if(rupees>= 200){
                rupeeMagnetCollected = true;
                rupeeMagnetZone.SetActive(true);
                //Debug.Log("rupee magnet collected");
                Destroy(other.gameObject);
                AddRupees(-200);
            }
        }
        if(other.gameObject.tag == "BlackOutfit"){
            if(rupees>= 0){
                //this.gameObject.transform.GetChild(3).GetComponent<Renderer>().material = other.gameObject.GetComponent<Renderer>().material;
                //SetMaterials(other.gameObject.GetComponent<Renderer>().material);
                SetMaterials(0);
                Destroy(other.gameObject);
                AddRupees(-100);
            }
        }
        if(other.gameObject.tag == "BlueOutfit"){
            if(rupees>=0){
                //this.gameObject.transform.GetChild(3).GetComponent<Renderer>().material = other.gameObject.GetComponent<Renderer>().material;
                //SetMaterials(other.gameObject.GetComponent<Renderer>().material);
                SetMaterials(1);
                Destroy(other.gameObject);
                AddRupees(-150);
            }
        }
       
    }

    private void SetMaterials(int index){
        /*Animator anim = this.gameObject.transform.GetChild(3).GetComponent<Animator>();
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach(AnimationClip c in clips){
            //c.
        }*/
        Debug.Log("old material: " + playerMat.GetTexture("_MainTex"));
        //playerMat.SetTexture("_MainTex",mat.GetTexture("_MainTex")); 
        //Shader s = new Shader();
        //playerMat.shader = playerMat.shader;
        //Debug.Log("new material: " + mat.GetTexture("_MainTex"));

        //playerMat.SetTexture("_MainTex", (Texture2D)playerTextures[index]); 
        playerTextures[2] = playerTextures[index];

    }

    public void AddRupees(int amt){ 
        rupeeHUD.UpdateHUD(rupees, amt);
        rupees += amt;
        //Debug.Log(rupees + " rupees");
    }

    public void Die(){
        //these statements are commented out until the death animation is added. then maybe re-add

        Time.timeScale = 0;
        lose = true;
        //losescreen.SetActive(true); 

        anim.SetBool("isDying", true);
    }

    private void WinGame(){
        winLoseScript.WinGame();
    }

    private void LoseGame(){
        winLoseScript.LoseGame();
    }

    private void LoseGameAxe()
    {
        winLoseScript.LoseGameAxe();
    }
    private void LoseGameSpike()
    {
        winLoseScript.LoseGameSpike();
    }
    public Animator GetAnimator(){
        return anim;
    }

    public void SetPaused(bool b){
        paused = b;
    }

    public void SetWon(bool b){
        won = b;
    }
    
}
