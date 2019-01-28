using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructOutsideOfBoundry : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Seedling"))
            Destroy(other, 1.0f);
    }
}
