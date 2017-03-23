using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawnManager : MonoBehaviour { 

    //Types of enemy to spawn
	private Transform[] enemyTypes = new Transform[1];
    public Transform enemyBasicPrefab;
    public Transform enemyFastPrefab;
    public Transform enemySlowPrefab;
  
    private float timeBetweenWaves = 45f;
	private float waveCooldown;
	private float timeBetweenGroups = 6f;
	private float timeBetweenSpawns = 0.2f;
	//How many waves in a game
	private int gameLength = 3;
	//Which wave you are on
	private int waveIndex;
	//How many groups in a wave
	private int waveSize = 5;
	//How many spawns in a group
	private int groupSize = 5;
    
    public Transform spawnPoint;
    
    void Start() {
        //Load all enemy types into array
        enemyTypes[0] = enemyBasicPrefab;
        //enemyTypes[1] = enemyFastPrefab;
        //enemyTypes[2] = enemySlowPrefab;

		//Initial time at newgame before enemies begin to spawn
		//TODO replace this with an unlimited "build phase"
		waveCooldown = 10f;
		waveIndex = 0;
    }
    
    void Update() {
		if(waveCooldown <= 0f && waveIndex < gameLength) {
			StartCoroutine(spawnWave());
			waveCooldown = timeBetweenWaves;
			waveIndex++;
		}
		waveCooldown -= Time.deltaTime;
    }

	IEnumerator spawnWave() {
		for (int i = 0; i < waveSize; i++) {
			StartCoroutine(spawnGroup());
			yield return new WaitForSeconds(timeBetweenGroups);
		}
	}
		
    IEnumerator spawnGroup() {
        for(int i = 0 ; i < groupSize ; i++) {
            spawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
    
    void spawnEnemy() {
        //Spawn random enemy from list of enemy types
        var index = Random.Range(0, enemyTypes.Length);
        Instantiate(enemyTypes[index], spawnPoint.position, spawnPoint.rotation);
    }
}
