using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality;
using Microsoft.MixedReality.Toolkit.Core;
using Microsoft.MixedReality.Toolkit.SDK;
using UnityEngine.XR;
using UnityEngine.XR.WSA.Input;

public class PlanetRotationInputter : MonoBehaviour
{
    Quaternion previousLeftControllerFrameRotation;

    bool wasLeftSelectDown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Left Controller Select") > 0.1)
        {
            //if left or right controller is raycasting towards it start changing its rotation?
            var leftControllerPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
            var leftControllerRotation = InputTracking.GetLocalRotation(XRNode.LeftHand);

            Quaternion deltaAngle = QuaternionHelper.DeltaRotation(leftControllerRotation, previousLeftControllerFrameRotation);

            if (deltaAngle.eulerAngles.magnitude > 5.0f) //stop micro rotations
            {
                //apply delta angle to the planet
                GetComponent<Rigidbody>().angularVelocity += deltaAngle.eulerAngles;
                //GetComponent<Rigidbody>().angularVelocity += new Vector3(1.0f, 10.0f, 1.0f);
            }
            else //set it to be zero if there is little change
            {
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }

            previousLeftControllerFrameRotation = leftControllerRotation;
        }
    }

    //private void LookAtApproach()
    //{
    //    // Do a raycast into the world based on the user's  

    //    RaycastHit hitInfo;

    //    if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
    //    {
    //        // If the raycast hit a hologram...
    //        // Display the cursor mesh.
    //        GetComponent<MeshRenderer>().material.color = new Color(1f, 0f, 0f);

    //        // Move the cursor to the point where the raycast hit.
    //        this.transform.position = hitInfo.point;

    //        // Rotate the cursor to hug the surface of the hologram.
    //        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
    //    }
    //    else
    //    {
    //        GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f);
    //    }
    //}
}
