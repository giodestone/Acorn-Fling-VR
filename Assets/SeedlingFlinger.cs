using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class SeedlingFlinger : MonoBehaviour
{
    [SerializeField]
    GameObject origin;

    [SerializeField]
    float flingStrength = 1f;

    [SerializeField]
    GameObject seed;

    Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (origin == null)
            Debug.LogError("Origin to where to fling seeds from is null at " + this.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //var interactionSourceStates = InteractionManager.GetCurrentReading();
        //foreach (var interactionSourceState in interactionSourceStates)
        //{
        //    // ...
        //    if (interactionSourceState.touchpadPressed && !(Input.GetAxis("Left Controller Select") > 0.1f)
        //    {
        //        var newSeed = GameObject.Instantiate(seed);
        //        newSeed.transform.position = origin.transform.position;
        //        newSeed.transform.rotation = origin.transform.rotation;
        //        newSeed.GetComponent<Rigidbody>().useGravity = false;
        //        newSeed.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().angularVelocity * flingStrength);
        //    }
        //}

        var currentPosition = origin.transform.position;

        var deltaPosition = lastPosition - currentPosition;

        if (Input.GetButtonDown("Fling Seeds") && Input.GetAxis("Left Controller Select") < 0.1f && GetComponent<Rigidbody>().angularVelocity.magnitude > 0.5f) //only fire when not spinning
        {
            for (int i = 0; i < 1; i++)
            {
                var newSeed = GameObject.Instantiate(seed);
                newSeed.transform.position = origin.transform.position;
                newSeed.transform.rotation = origin.transform.rotation;
                newSeed.GetComponent<Rigidbody>().useGravity = false;
                newSeed.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().GetPointVelocity(origin.transform.position) * flingStrength;
                //newSeed.GetComponent<Rigidbody>().AddForce(deltaPosition * flingStrength * Time.deltaTime);
            }

        }

        lastPosition = currentPosition;
    }
}
