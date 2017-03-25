using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawnManager : MonoBehaviour { 

    //Types of enemy to spawn
	private Transform[] enemyTypes = new Transform[1];
    public Transform enemyBasicPrefab;
    public Transform enemyFastPrefab;
    public Transform enemySlowPrefab;

	//How many waves in a game
	private int gameLength = 2;
	//Which wave you are on
	private int waveIndex;
	//How many groups in a wave
	private int waveSize = 2;
	//How many spawns in a group
	private int groupSize = 5;
	//How many enemies have we spawned
	private int enemyCnt;
	private string enemyTag = "Code";

	private float timeBetweenSpawns = 0.2f;
	private float timeBetweenGroups = 6f;
	private float timeBetweenWaves = 30f;
	private float waveCooldown;
    
    public Transform spawnPoint;

	   
    void Start() {
		PhaseManager.instance.enablePhase ();
        //Load all enemy types into array
        enemyTypes[0] = enemyBasicPrefab;
        //enemyTypes[1] = enemyFastPrefab;
        //enemyTypes[2] = enemySlowPrefab;

		//Initial time at newgame before enemies begin to spawn
		//TODO replace this with an unlimited "build phase"
		waveCooldown = 0f;
		waveIndex = 0;
    }
    
    void Update() {
		if (waveCooldown <= 0f && waveIndex < gameLength) {
			StartCoroutine (spawnWave ());
			waveCooldown = timeBetweenWaves;
			waveIndex++; 
		}
		waveCooldown -= Time.deltaTime;
		if (enemyCnt == groupSize * waveSize) {
			if (!enemiesRemaining ()) {
				if (waveIndex >= gameLength) {
					PhaseManager.instance.gameOverPrompt ();
				}
				else {
					PhaseManager.instance.enablePhase ();
				}
			}
		}
		waveCooldown -= Time.deltaTime;
    }

	bool enemiesRemaining() {
		if (GameObject.FindGameObjectsWithTag (enemyTag).Length == 0) {
			enemyCnt = 0;
			return false;
		}
		else {
			return true;
		}
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
		enemyCnt++;
    }
}
