using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilePhysics : MonoBehaviour
{
    public GameObject otherObject;
    private Rigidbody rg;


    // Start is called before the first frame update
    void Start()
    {
        rg = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (otherObject)
        {
            float distance = Vector3.Distance(otherObject.transform.position, transform.position);
            rg.AddForce((otherObject.transform.position - transform.position).normalized * distance * otherObject.transform.localScale.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gravity")
            otherObject = other.transform.parent.gameObject;

        if (other.gameObject.tag == "Planet")
            Destroy(gameObject);


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Gravity")
            otherObject = null;
    }
}
