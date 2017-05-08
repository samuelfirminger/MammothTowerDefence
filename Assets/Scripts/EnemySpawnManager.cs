using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawnManager : MonoBehaviour { 
    //Make EnemySpawnManager a Singleton class
    public static EnemySpawnManager instance;
    
    //Types of enemy to spawn
	[Header("Turret Prefabs")]
	private Transform[] enemyTypes = new Transform[3];
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
 
    //For simplified programming of turrets: this variable holds
    //the value corresponding to the enemy type for this wave and the
    //last spawned enemy type (used in enemy class)
    //(will be visually shown in briefing)
    public int briefingEnemy = 0;
    public int lastSpawned   = 0;
    
    void Awake() {
		if (instance != null) {
			Debug.Log ("More than one EnemySpawnManager in scene."); 
			return; 
		}
  
        instance = this;
    }
 
    void Start() {
		PhaseManager.instance.enableBuildPhase ();
        //Load all enemy types into array
        enemyTypes[0] = enemyBasicPrefab;
        enemyTypes[1] = enemyFastPrefab;
        enemyTypes[2] = enemySlowPrefab;

		//Initial time at newgame before enemies begin to spawn
		//TODO replace this with an unlimited "build phase" -> already done?
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
					TurretManager.instance.setSellState(false);
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
        briefingEnemy = Random.Range(0, enemyTypes.Length);
        Debug.Log("================ briefingEnemy = " + briefingEnemy + " ====================");
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
        lastSpawned = index;
        //Debug.Log("(0-"+enemyTypes.Length+") Enemy spawn index " + index);
        Instantiate(enemyTypes[index], spawnPoint.position, spawnPoint.rotation);
		enemyCnt++;
    }
}
