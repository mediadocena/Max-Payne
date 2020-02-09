using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    public bulletTime btime;
    bool slow;
    public GameObject gunControl;
    public GameObject PlayeraudioSource;
    public AudioSource audio;
    public AudioClip StartSlowmo;
    public AudioClip EndSlowmo;
    private bool soundSlow;
    public float bTimeCD = 5f;
    private float timeToBT;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        timeToBT = bTimeCD;
    }

    void Update()
    {
        Debug.Log(timeToBT);
        if (gunControl.GetComponent<Gun>().cinematica==false) { 
        //Movimiento del raton:
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        BulletTimeControl();
        }
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
        if (slow == true && timeToBT>0f)
        {
            timeToBT = timeToBT -10 * Time.deltaTime;
            if (!soundSlow) {
                audio.PlayOneShot(StartSlowmo, 0.05f);
                soundSlow = true;
            }
            btime.DoSlowmotion();
        }
        else
        {
            if (timeToBT<bTimeCD) {
                slow = false;
                timeToBT = timeToBT + 1 * Time.deltaTime;
            }

            if (soundSlow) { 
            audio.PlayOneShot(EndSlowmo, 0.05f);
                soundSlow = false;
            }
            btime.StopSlowmotion();
        }
    }

}
