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
	public int waveIndex;
	//How many groups in a wave
	private int waveSize = 2;
	//How many spawns in a group
	private int groupSize = 5;
	//How many enemies have we spawned
	private int enemyCnt;
	private string enemyTag = "Code";

	private float timeBetweenSpawns = 0.2f;
	private float timeBetweenGroups = 3f ; 
	private float waveCooldown;
	private bool waveStart = true ;
    
    public Transform spawnPoint;

	   
    void Start() {
		PhaseManager.instance.enableBuildPhase ();
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
		//start wave start of game/new wave
		if (waveStart && waveIndex < gameLength) {
			waveStart = false; 
			StartCoroutine (spawnWave ());
			PlayerStats.instance.updateWave (waveIndex); 
			waveIndex++; 
		}

		//if all enemies of wave have spawned
		if (enemyCnt == groupSize * waveSize) {
			//if all enemies destroyed
			if (!enemiesRemaining ()) {
				//go into build if not end of game, else game over prompt
				if (waveIndex >= gameLength) {
					PhaseManager.instance.gameOverPrompt ();
				}
				else {
					PhaseManager.instance.enableBuildPhase ();
					TurretManager.instance.sell = false;
					PhaseManager.instance.intoSellMode (); 
					waveStart = true ; 
				}
			}
		}
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
			if (enemyCnt % groupSize == 0) {
				StartCoroutine (spawnGroup ());
			}
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
