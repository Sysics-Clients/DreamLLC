using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentControler : MonoBehaviour
{
    public CharacterController characterController;

    public float speed=12;
    public float gravity=-9.8f;
    public Transform groundCheck;
    public float groundDistance=0.4f;
    public LayerMask mask;
    Vector3 velocity;
    bool isGrounded;

    public Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, mask);
        if (isGrounded && velocity.y<0)
        {
            velocity.y = -2;
        }

        float x = -joystick.Vertical;
        float y = joystick.Horizontal;

        Vector3 move = transform.right * x + transform.forward * y;
        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
