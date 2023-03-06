using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public InputAction playermove;
    private CharacterController charactercontroller;
    public float speed;
    private Vector3 move;
    private Vector3 movement;
    private Vector3 velocity;
    private float gravity;

    // Start is called before the first frame update
    void Start()
    {
        charactercontroller = gameObject.GetComponent<CharacterController>();
        gravity = -9.81f;
    }
    private void OnEnable()
    {
        playermove.Enable();
    }

    private void OnDisable()
    {
        playermove.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        move = playermove.ReadValue<Vector3>();
    }

    private void FixedUpdate()
    {
        movement = (move.z * transform.forward) + (move.x * transform.right);
        movement.y = 0.0f;
        charactercontroller.Move(movement * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        charactercontroller.Move(velocity * Time.deltaTime);
        
    }
}
