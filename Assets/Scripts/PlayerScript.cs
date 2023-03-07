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
    public float speed;
    private Vector3 move;
    private Vector3 movement;
    private Vector3 velocity;
    private float gravity;

    //Looking variables
    public InputAction playerrotate;
    public float sensitivity;
    private Vector2 look;
    private float lookx;
    private float looky;
    private float rotationx;
    private float rotationy;
    public Transform orientation;

    
    // Start is called before the first frame update
    void Start()
    {
        charactercontroller = gameObject.GetComponent<CharacterController>();
        gravity = -9.81f;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        playermove.Enable();
        playerrotate.Enable();
    }

    private void OnDisable()
    {
        playermove.Disable();
        playerrotate.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        move = playermove.ReadValue<Vector3>();
        Look();
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
        looky = look.y * sensitivity * Time.deltaTime;

        rotationx -= looky;
        rotationy += lookx;

        rotationx = Mathf.Clamp(rotationx,-90.0f,90.0f);
        transform.rotation = Quaternion.Euler(-rotationx,-rotationy,0);
        orientation.transform.Rotate(Vector3.up * lookx);
    }
}
