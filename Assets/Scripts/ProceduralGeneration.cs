using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour {
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> obsticles = new List<GameObject>();
    public GameObject Player;
    private float lastGeneratedXPos;
    private Dictionary<int,List<GameObject>> Generations = new Dictionary<int,List<GameObject>>();
	// Use this for initialization
	void Start () {
        lastGeneratedXPos = -20;
        GenerateTerrainX();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.x  - lastGeneratedXPos > 20f)
            GenerateTerrainX();
	}
    public void GenerateTerrainX()
    {
        List<GameObject> generatedObjects = new List<GameObject>();
        bool enemy = Random.Range(0, 101) < 20;
        if(enemy)
        {
            int numEnemies = Random.Range(1,4);
            for(int i = 0;i<numEnemies;i++)
            {
                GameObject g = GameObject.Instantiate(enemies[Random.Range(0, enemies.Count)]);
                g.transform.position = new Vector3(lastGeneratedXPos + Random.Range(0, 21), Random.Range(2, 21), 0);
                generatedObjects.Add(g);
            }
        }
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.transform.localScale = new Vector3(20,1,1);
        ground.transform.position = new Vector3(lastGeneratedXPos + 20,0,0);
        generatedObjects.Add(ground);
        Generations.Add((int)lastGeneratedXPos, generatedObjects);
        RemoveTerrainSection();
        lastGeneratedXPos +=20f;
    }
    public void RemoveTerrainSection()
    {
        
        if(Generations.ContainsKey((int)lastGeneratedXPos - 100))
        {
            List<GameObject> gos = Generations[(int)lastGeneratedXPos - 100];
            foreach(GameObject g in gos)
            {
                Destroy(g);
            }
        }

    }
}
