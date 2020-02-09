using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Rigidbody bullet;
    public float speed = 30f;
    public LayerMask layerMask;
    public GameObject thirdCamera;
    public GameObject firstCamera;
    public GameObject BulletTime;
    public AudioSource GunAudio;
    public AudioClip Shoot;
    public AudioClip SlowShoot;
    RaycastHit hit;
    private Vector3 posThirdCam;
    private Vector3 posFirstCam;
    private Vector3 poslocFirstCam;
    private Vector3 poslocThirdCam;
    private Quaternion rotThirdCam;
    private Quaternion rotFirstCam;
    private Quaternion rotlocThirdCam;
    private Quaternion rotlocFirstCam;
    private Vector3 posEnemy;
    int contador;
    int segundos = 200;
    public bool cinematica = false;
    private int prob;
    private float contDisparo = 0;
    public float cdDisparo;
    void Start()
    {
        contDisparo = cdDisparo;
    }
    // Update is called once per frame
    void Update()
    {
        if (BulletTime.GetComponent<bulletTime>().isSlowed == false)
        {
            contDisparo += Time.deltaTime;
        }
        else {
            contDisparo += Time.deltaTime*5f;
        }
        prob = Random.Range(0, 100);
        //Disparamos
        if (Input.GetButton("Fire1") && contDisparo > cdDisparo) {
            contDisparo = 0;
            if (BulletTime.GetComponent<bulletTime>().isSlowed==false) { 
            GunAudio.PlayOneShot(Shoot, 1f);
            }
            else
            {
                GunAudio.PlayOneShot(SlowShoot, 1f);
            }
            //Predice si la el disparo va a impactar en el enemigo con el bullet time
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask) && BulletTime.GetComponent<bulletTime>().isSlowed && prob < 50) {
                //Si no estaba ya activada la camara cinematica, entra
                
                if (!cinematica) { 
                SaveCamPosition();
                contador = 0;
                cinematica = true;
                 
                posEnemy = hit.collider.gameObject.transform.position;
                if (thirdCamera.active == true) {
                    thirdCamera.transform.position = new Vector3(posEnemy.x, posEnemy.y+1, posEnemy.z + 6);
                    thirdCamera.transform.LookAt(posThirdCam);
                } else if (firstCamera.active == true) { 
                    firstCamera.transform.position = new Vector3(posEnemy.x, posEnemy.y+1, posEnemy.z + 6);
                    firstCamera.transform.LookAt(posFirstCam);
                }
               }
            }
           
            //Objeto   //posicion 3d       //Rotacion          //Conversion a Rigidbody
            Rigidbody Instanciaproyectil = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;

            Instanciaproyectil.velocity = transform.TransformDirection(new Vector3(0, 0, speed*2));
            //Colisiones off
            //Physics.IgnoreCollision(Instanciaproyectil.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
        }
        //Si esta la cinematica activa entra
        if (cinematica == true)
        {
            Debug.Log("Cinematica " + contador);
            //contador para determinar el tiempo de duración de la cámara
            contador++;
            //Cuando llegue al límite de tiempo se cambian las camaras a la normalidad
            if (contador > segundos)
            {
                    //firstCamera.transform.SetPositionAndRotation(posFirstCam,rotFirstCam);
                    firstCamera.transform.localPosition = poslocFirstCam;
                    firstCamera.transform.localRotation = rotlocFirstCam;
                
                
                    // thirdCamera.transform.SetPositionAndRotation(posThirdCam, rotThirdCam);
                    thirdCamera.transform.localPosition = poslocThirdCam;
                    thirdCamera.transform.localRotation = rotlocThirdCam;
                
                    cinematica = false;
            }
        }

        //FIN DISPARO - CAMARA CINEMATICA

        //APUNTAR
        if (Input.GetButtonDown("Fire2"))
        {
            if (firstCamera.active == true)
            {
                firstCamera.GetComponent<Camera>().fieldOfView = 50f;
            }
            else if (thirdCamera.active == true)
            {
                thirdCamera.GetComponent<Camera>().fieldOfView = 40f;
            }
        }
        else if(Input.GetButtonUp("Fire2")){
            firstCamera.GetComponent<Camera>().fieldOfView = 60f;
            thirdCamera.GetComponent<Camera>().fieldOfView = 60f;
        }

    }
    public void SaveCamPosition() {
        posFirstCam = firstCamera.transform.position;
        posThirdCam = thirdCamera.transform.position;
        rotFirstCam = firstCamera.transform.rotation;
        rotThirdCam = thirdCamera.transform.rotation;
        poslocThirdCam = thirdCamera.transform.localPosition;
        poslocFirstCam = firstCamera.transform.localPosition;
        rotlocThirdCam = thirdCamera.transform.localRotation;
        rotlocFirstCam = firstCamera.transform.localRotation;
    }
}
