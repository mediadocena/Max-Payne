using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawn : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,10f);
    }
}
