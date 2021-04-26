using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public CharacterController controller;
    public float movSpeed = 20f;
    public float gravity = 9.81f;
    public float jmpHeight = 1;
   // public AudioSource audioSrc;
    //public AudioClip footStep1;
    //public AudioClip footStep2;
    float timeSinceLastStep = 1;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isOnGnd;


    void Update()
    {
        isOnGnd = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isOnGnd && velocity.y < 0)
        {
            velocity.y = -2;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (isOnGnd && timeSinceLastStep >= 1)
        {
          //  audioSrc.PlayOneShot(footStep1);
        }
        Vector3 mov = transform.right * x + transform.forward * z;
        if (HUD.canMove)
        {
            controller.Move(mov * movSpeed * Time.deltaTime);
        }
        timeSinceLastStep = Time.time;

        if (isOnGnd && Input.GetButtonDown("Jump")&& HUD.canMove)
        {
            velocity.y = Mathf.Sqrt(jmpHeight * 2 * gravity);
            timeSinceLastStep = 0;
        }

        velocity.y -= gravity * Time.deltaTime;
        if (isOnGnd && timeSinceLastStep >= 1)
        {
          //  audioSrc.PlayOneShot(footStep2);
          
        }
        controller.Move(velocity * Time.deltaTime);

        timeSinceLastStep =0;


    }
}
