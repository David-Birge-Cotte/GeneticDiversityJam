using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldBehaviour : MonoBehaviour {

    //----------------------------------------------------------------
    // Variables
    //----------------------------------------------------------------
    // Time
    public float spawnTime = 1;
    public float spawnTimeAleaVariation = 1;
    private float _delay;

    // Grid
    public int xSize = 20;
    public int ySize = 20;

    // Objects
    public int nbAgentToSpawn = 50;
    public int maxFoodInWorld = 20;
    public GameObject foodGO;
	public static List<GameObject> foodObjects;
    public GameObject agent;

    public static List<Agent> individus;

    // Logic
    public static bool bSpawnsFood = true;
    //----------------------------------------------------------------

    void Start ()
    {
        if(!foodGO)
        {
            Debug.Log("No Gameobject Setup");
        }

        // List Initialisation
        foodObjects = new List<GameObject>();
        individus = new List<Agent>();

        StartCoroutine("SpawnLife");

        for(int i = 0; i<maxFoodInWorld; i++)
        {
            SpawnFoodAtRandomLocation();
        }

	}

    IEnumerator SpawnLife()
    {
        for (int i = 0; i < nbAgentToSpawn; i++)
        {
            GameObject agentTemp = Instantiate(agent, new Vector3(i*2 - nbAgentToSpawn / 2, i*2 - nbAgentToSpawn / 2, 0), Quaternion.identity) as GameObject;
            individus.Add(agentTemp.GetComponent<Agent>());
        }
        yield return null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("SpawnLife");
        }


        // Spawns Food and then Wait
        if (_delay <= 0)
        {
            if (foodObjects.Count < maxFoodInWorld)
            {
                SpawnFoodAtRandomLocation();
            }
            _delay = spawnTime + Random.Range(0, spawnTimeAleaVariation);
        }

        if(bSpawnsFood)
        {
            _delay -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Spawns A unit of Food on the map
    /// </summary>
    void SpawnFoodAtRandomLocation()
    {
        // Random position
        int _x = Random.Range(-xSize / 2, xSize / 2);
        int _y = Random.Range(-ySize / 2, ySize / 2);
        Vector2 _pos = new Vector2(_x, _y);

        // Spawn du Gameobject
        GameObject _spawnedFood = Instantiate<GameObject>(foodGO);
        _spawnedFood.transform.position = _pos;

        // Ajout à la List
        foodObjects.Add(_spawnedFood.gameObject);
    }

    public void Mutate()
    {
        foreach(Agent a in GameObject.FindObjectsOfType<Agent>())
        {
            a.Mutate();
        }
    }
}