﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 12f;
    public CharacterController controller;
    // Start is called before the first frame update
    public float gravity = -12f;
    Vector3 Velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    bool sprint = false;
    public float jumpHeight = 3.5f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left shift"))
        {
            sprint = true;
        }
        else
        {
            sprint = false;
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && Velocity.y < 0) {
            Velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
      
        if (sprint == true)
        {
            print("Sprint");
            controller.Move(move * (speed * 1.5f) * Time.deltaTime);
        }
        else { 
            controller.Move(move * speed * Time.deltaTime);
        }
        if (Input.GetButtonDown("Jump") && isGrounded) {
            Debug.Log("entra");
            if (Velocity.x>0) {
                print("MAYOOOOOOOOOOR");
            }
            Velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        Velocity.y += gravity * Time.deltaTime;

        controller.Move(Velocity * Time.deltaTime);
    }
}
