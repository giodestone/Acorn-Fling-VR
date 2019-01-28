using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class planetColorScript : MonoBehaviour
{
    public Color[] colourless;
    public Color[] colourfull;
    private Renderer rend;
    private Animator anim;

    private float exposure = 1;
    private bool expose;
    public bool house;

    public bool startInfected;

    [SerializeField]
    public GameObject planetPull;

    private bool infected;

    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        anim = gameObject.GetComponent<Animator>();
        rend.material.color = colourless[Random.Range(0, colourless.Length)];
        RenderSettings.skybox.SetFloat("_Exposure", 1);

        if(startInfected)
        {
            rend.material.color = colourfull[Random.Range(0, colourless.Length)];
            anim.SetTrigger("bloom");
            infected = true;
        }
    }

    void Update()
    {
        if (expose)
        {
            if (exposure < 6)
            {
                exposure += 2.5f * Time.deltaTime;
                RenderSettings.skybox.SetFloat("_Exposure", exposure);
            }
            else
                SceneManager.LoadScene(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Seedling") && !infected)
        {
            rend.material.color = colourfull[Random.Range(0, colourless.Length)];
            anim.SetTrigger("bloom");

            if (!house)
            {
                planetPull.GetComponent<SphereCollider>().enabled = false; //disable gravity
                GetComponent<SeedlingFlinger>().enabled = true; //enable input
                GetComponent<OwnPlanetSeedlingDestroyer>().enabled = true; //enable input
                GetComponent<PlanetRotationInputter>().enabled = true; //enable input
            }
            var obj = GameObject.FindGameObjectWithTag("GameController");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().HitPlanet(this.gameObject);
            infected = true;

            if(house) 
                expose = true;
        }
    }
}
