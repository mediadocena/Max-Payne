using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Rigidbody bullet;
    public float speed = 30f;
    public void shoot() {

        Rigidbody Instanciaproyectil = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;
        Instanciaproyectil.velocity = transform.TransformDirection(new Vector3(0, 0, speed * 2));
        
    }
}
