using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int score = 0;

    [SerializeField]
    GameObject defaultPlanet;

    GameObject currentPlanet;

    [SerializeField]
    GameObject AllSceneObjectsRoot;

    // Start is called before the first frame update
    void Start()
    {
        if (defaultPlanet == null)
            Debug.LogError("Default planet on GameController not set!");

        currentPlanet = defaultPlanet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitPlanet(GameObject planet)
    {
        //disable all player interaction from current planet
        currentPlanet.GetComponent<SeedlingFlinger>().enabled = false; //disable input
        currentPlanet.GetComponent<OwnPlanetSeedlingDestroyer>().enabled = false; //disable input
        currentPlanet.GetComponent<PlanetRotationInputter>().enabled = false; //disable input

        //disable gravity to current planet
        currentPlanet.GetComponent<planetColorScript>().planetPull.GetComponent<SphereCollider>().enabled = false;

        //disable detecting whether the seeds have collided to current planet
        currentPlanet.GetComponent<SphereCollider>().enabled = false;

        //increment score
        score += 1;

        //set the current planet to be the hit planet.
        currentPlanet = planet;

        //change the posistion of the player.
        //transform by the inverse of planet.pos
        AllSceneObjectsRoot.gameObject.transform.position = -planet.transform.position /*+ new Vector3(-2.5f, 2.0f, 0.0f)*/;
        AllSceneObjectsRoot.gameObject.transform.position = new Vector3(AllSceneObjectsRoot.gameObject.transform.position.x, planet.transform.position.y, AllSceneObjectsRoot.gameObject.transform.position.z);
    }
}
