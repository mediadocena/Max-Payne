using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    
    void Start()
    {
        
    }
    public SpringJoint joint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            StartGrapple();
        }
        if (Input.GetButtonDown("Fire2")) {
            StopGrapple();
        }
    }

    private void StopGrapple() {
        Destroy(joint);
    }

    private void StartGrapple() {
        print("entra");
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, 100f);
        if (hits.Length < 1) return;
        Vector3 grapplePoint = hits[0].point;
        joint = gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grapplePoint;
        joint.spring = 3.5f;
        joint.damper = 0.25f;
    }
}
