using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    private GameObject player;

	public float mapRadius = 3;
	public int spawnDistance = 3;
    public int maxEnemies = 5;
    public float spawnDelay = 4f;
    public float spawnDelayWiggle = .2f;

    private Vector2 center;
    private Vector2 pos;

    private float startTime;
    private float nextDelayWiggle;

    private int tally = 0;

    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        EventManager.StartListening("ENEMY_DIED", SpawnNewEnemy);
	    center = new Vector2(0, 0);

        startTime = Time.time;
	}

    private void Update()
    {
        if(Time.time > startTime + spawnDelay + nextDelayWiggle)
        {
            SpawnNewEnemy();
            startTime = Time.time;
            nextDelayWiggle = Random.value * spawnDelayWiggle;
        }
    }

    public void SpawnNewEnemy()
    {
	    //Check if Player is still alive
	    if (!player.GetComponent<PlayerStatus>().IsDead && tally < maxEnemies)
	    {
		    Vector2 playerPos = player.transform.position;
//	        do
//	        {
	            pos = Random.insideUnitCircle * mapRadius + center;
//	        } while (Vector2.Distance(pos, playerPos) < spawnDistance);


            var spawned = Instantiate(enemyPrefab, pos, Quaternion.identity);
	        var spawnedController = spawned.GetComponent<EnemyTopDownMovement>();
	        if (spawnedController != null)
	        {
	            tally++;
                spawnedController.registerDeathNotifier(strikeFromLedger);
	        }
	    }
    }

    private void strikeFromLedger(EnemyTopDownMovement dead)
    {
        tally--;
    }
	

}