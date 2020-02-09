using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMap : MonoBehaviour
{
    public MeshRenderer cubo1;
    public MeshRenderer cubo2;

    void Start()
    {
        cubo1.enabled = true;
        cubo2.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
