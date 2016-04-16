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
    public int maxFoodInWorld = 20;
    public GameObject foodGO;
	public static List<GameObject> foodObjects;

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
        // Starts the delay calculations
        StartCoroutine("delayCalculation");
	}
	
	void Update ()
    {
        // Spawns Food and then Wait
	    if(_delay <= 0)
        {
            if(foodObjects.Count < maxFoodInWorld)
            {
                SpawnFoodAtRandomLocation();
            }            
            _delay = spawnTime + Random.Range(0, spawnTimeAleaVariation);
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

    IEnumerator delayCalculation()
    {
        while (bSpawnsFood)
        {
            _delay -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}