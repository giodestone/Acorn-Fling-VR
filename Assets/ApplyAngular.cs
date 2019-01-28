using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAngular : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0.0f, 0.25f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
