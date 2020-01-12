﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    public bulletTime btime;
    bool slow;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Movimiento del raton:

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        BulletTimeControl();    
    }
    void BulletTimeControl() {

        if (Input.GetButtonDown("Slowmo"))
        {
            slow = true;
        }
        if (Input.GetButtonDown("NoSlowmo"))
        {
            slow = false;
        }
        if (slow == true)
        {
            btime.DoSlowmotion();
        }
        else
        {
            btime.StopSlowmotion();
        }
    }

}
