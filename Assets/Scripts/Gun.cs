using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Rigidbody bullet;
    public float speed = 30f;
    public LayerMask layerMask;
    public GameObject EnemyCamera;
    public GameObject thirdCamera;
    public GameObject firstCamera;
    public GameObject BulletTime;
    RaycastHit hit;
    int contador;
    int segundos = 200;
   public bool cinematica = false;

    void Start()
    {
        EnemyCamera.SetActive(false);
        thirdCamera.SetActive(true);
        firstCamera.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //Disparamos
        if (Input.GetButtonDown("Fire1")) {
            //Predice si la el disparo va a impactar en el enemigo con el bullet time
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask) && BulletTime.GetComponent<bulletTime>().isSlowed) {
                //Si impacta, se activa la cámara cinemática
                Debug.Log("Entra");
                cinematica = true;
                EnemyCamera.SetActive(true);
                thirdCamera.SetActive(false);
                firstCamera.SetActive(false);
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
                EnemyCamera.SetActive(false);
                thirdCamera.SetActive(true);
                firstCamera.SetActive(false);
                contador = 0;
                cinematica = false;
            }
        }
    }
}
