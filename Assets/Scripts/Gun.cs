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
    RaycastHit hit;
    int contador;
    int segundos = 100;
    bool cinematica = false;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1")) {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask)) {
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
            Physics.IgnoreCollision(Instanciaproyectil.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
        }
        if (cinematica == true)
        {
            Debug.Log("Cinematica " + contador);
            contador++;
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
