using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnPlanetSeedlingDestroyer : MonoBehaviour
{
    GameController gameControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameControllerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (gameControllerScript == null)
            Debug.LogError("Unable to find gameControllerScript for the OwnPlaetSeedlingDestroyer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Seedling"))
            Destroy(other);
    }
}
