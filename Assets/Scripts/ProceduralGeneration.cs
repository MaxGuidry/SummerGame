using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ProceduralGeneration : MonoBehaviour
{
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public List<GameObject> obstaclesPrefabs = new List<GameObject>();
    public GameObject Player;
    private float lastGeneratedXPos;
    private float lastKnownYPos;
    private Dictionary<int, Dictionary<int, List<GameObject>>> Generations = new Dictionary<int, Dictionary<int, List<GameObject>>>();
    // Use this for initialization
    void Start()
    {
        
        lastGeneratedXPos = -40;
        GenerateTerrainX();

    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x - lastGeneratedXPos > -10f)
            GenerateTerrainX();
        if (Player.transform.position.y - Mathf.Ceil(Player.transform.position.y / 20f) * 20f > -10f && !Generations[(int)lastGeneratedXPos].ContainsKey((int)Mathf.Ceil(Player.transform.position.y / 20f) * 20))
            GenerateTerrainY();
    }
    public void GenerateTerrainX()
    {
        lastGeneratedXPos += 20f;
        List<GameObject> generatedObjects = new List<GameObject>();
        bool enemy = Random.Range(0, 101) < 20;
        if (enemy)
        {
            int numEnemies = Random.Range(1, 4);
            for (int i = 0; i < numEnemies; i++)
            {
                GameObject g = GameObject.Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
                g.transform.position = new Vector3(lastGeneratedXPos + Random.Range(0, 21), Random.Range(2, 21), 0);
                generatedObjects.Add(g);
            }
        }
        bool obstacles = Random.Range(0, 101) < 80;
        if (obstacles)
        {
            int numObsticles = Random.Range(1, 5);
            for (int i = 0; i < numObsticles; i++)
            {
                GameObject g = GameObject.Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Count)]);
                //switch (g.GetComponent<ObstacleBehaviour>().type)
                //{
                //}

                g.transform.position = new Vector3(lastGeneratedXPos + Random.Range(0, 21), Random.Range(2, 21), 0);
                generatedObjects.Add(g);
            }
        }
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.transform.localScale = new Vector3(20, 1, 1);
        ground.transform.position = new Vector3(lastGeneratedXPos + 20, 0, 0);
        generatedObjects.Add(ground);
        Generations.Add((int)lastGeneratedXPos, new Dictionary<int, List<GameObject>>());
        Generations[(int)lastGeneratedXPos].Add(0, generatedObjects);
        RemoveTerrainSection();
        
    }
    public void GenerateTerrainY()
    {
        List<GameObject> generatedObjects = new List<GameObject>();
        bool enemy = Random.Range(0, 101) < 20;
        if (enemy)
        {
            int numEnemies = Random.Range(1, 4);
            for (int i = 0; i < numEnemies; i++)
            {
                GameObject g = GameObject.Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
                g.transform.position = new Vector3(lastGeneratedXPos - 20f + Random.Range(0, 21), Mathf.Ceil(Player.transform.position.y / 20f) * 20f - (float)(Random.Range(0,21)), 0);
                generatedObjects.Add(g);
            }
        }
        bool obstacles = Random.Range(0, 101) < 80;
        if (obstacles)
        {
            int numObsticles = Random.Range(1, 5);
            for (int i = 0; i < numObsticles; i++)
            {
                GameObject g = GameObject.Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Count)]);
                //switch (g.GetComponent<ObstacleBehaviour>().type)
                //{
                //}

                g.transform.position = new Vector3(lastGeneratedXPos - 20f + Random.Range(0, 21), Mathf.Ceil(Player.transform.position.y / 20f) * 20f - (float)(Random.Range(0, 21)), 0);
                generatedObjects.Add(g);
            }
        }

        
        Generations[(int)lastGeneratedXPos].Add((int)Mathf.Ceil(Player.transform.position.y / 20f) * 20, generatedObjects);

    }
    public void RemoveTerrainSection()
    {

        if (Generations.ContainsKey((int)lastGeneratedXPos - 100))
        {
            foreach (List<GameObject> L in Generations[(int)lastGeneratedXPos - 100].Values)
            {

                foreach (GameObject g in L)
                {
                    Destroy(g);
                }
            }
        }

    }
}
