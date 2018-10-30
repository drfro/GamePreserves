using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour
{

    public Transform bottomLeftCorner;
    public Transform topRightCorner;

    public List<Transform> spawnPrefabs;
    private List<Spawnee> spawned;

    public float moveSpeed;
    public float moveVariance;

    public float spawnDelay = .2f;
    private float timeTilSpawn;

    public int maxSpawn;
    private System.Random rand;


	// Use this for initialization
	void Start () {
		spawned = new List<Spawnee>();
        rand = new System.Random();
	}
	
	// Update is called once per frame
	void Update () {

	    if (timeTilSpawn <= 0 && spawned.Count < maxSpawn)
	    {
	        var thing = Instantiate(spawnPrefabs[rand.Next(spawnPrefabs.Count )]).gameObject;
	        var spawnee = new Spawnee {spawned = thing, speed = moveSpeed + (float)rand.NextDouble() * moveVariance};
            spawned.Add(spawnee);
	        timeTilSpawn = spawnDelay;

	        setToLeftEdge(thing);
	    }

	    if (timeTilSpawn > 0)
	    {
	        timeTilSpawn -= Time.deltaTime;
	    }

	    foreach (var spawnee in spawned)
	    {
	        if (spawnee.spawned.transform.position.x > topRightCorner.position.x + 3)
	        {
                setToLeftEdge(spawnee.spawned);
	        }

            spawnee.spawned.transform.position = new Vector3(spawnee.spawned.transform.position.x + spawnee.speed * Time.deltaTime, spawnee.spawned.transform.position.y);
	    }

	}

    private void setToLeftEdge(GameObject thing)
    {
        var verticalArea = topRightCorner.position.y - bottomLeftCorner.position.y;
        var verticalPos = bottomLeftCorner.position.y + (float) rand.NextDouble() * verticalArea;
        thing.transform.position = new Vector3(bottomLeftCorner.transform.position.x - 3, verticalPos);
    }

    public class Spawnee
    {
        public float speed;
        public GameObject spawned;
    }
}
