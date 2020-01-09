using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Rigidbody bullet;
    public float speed = 30f;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {          //Objeto   //posicion 3d       //Rotacion          //Conversion a Rigidbody
            Rigidbody Instanciaproyectil = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;

            Instanciaproyectil.velocity = transform.TransformDirection(new Vector3(0, 0, speed*2));
            //Colisiones off
            Physics.IgnoreCollision(Instanciaproyectil.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
        }    
    }
}
