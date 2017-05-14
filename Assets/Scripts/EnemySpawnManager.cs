using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EnemySpawnManager : MonoBehaviour { 
    //Make EnemySpawnManager a Singleton class
    public static EnemySpawnManager instance;
    
    //Types of enemy to spawn
	[Header("Turret Prefabs")]
	public Transform[] enemyTypes;

	//How many rounds in a game
	public int gameLength;
	//Which round you are on
	private int roundIndex;
	//How many waves in a round
	public int roundSize;
	//Which wave you are on
	private int waveIndex;
	//How many groups in a wave
	private int waveSize = 2;
	//How many spawns in a group
	private int groupSize = 5;
	//How many enemies have we spawned
	private int enemyCnt;

	private float timeBetweenSpawns = 0.2f;
	private float timeBetweenGroups = 3f ; 
	private bool waveStart = false ;
    
    public Transform spawnPoint;
 
    //For simplified programming of turrets: this variable holds
    //the value corresponding to the enemy type for this wave and the
    //last spawned enemy type (used in enemy class)
    //(will be visually shown in briefing)
    public int briefingEnemy = 0;
    public int lastSpawned   = 0;

	//UI elements
	public GameObject waveUI; 
	public GameObject roundUI;
    
    void Awake() {
		if (instance != null) {
			Debug.Log ("More than one EnemySpawnManager in scene."); 
			return; 
		}
  
        instance = this;
    }
 
    void Start() {
		PhaseManager.instance.enableBuildPhase();

		Interpreter interpreter = new Interpreter ();
		for(int i = 0; i < enemyTypes.Length; i++) {
			interpreter.interpret(enemyTypes[i], BetweenScenes.parsedInstructionSet);
		}

		waveIndex = 0;
		roundIndex = 0;
    }

    void Update() {
		updateUI();
		//start wave start of game/new wave
		if (waveStart && waveIndex < roundSize) {
			waveStart = false; 
			StartCoroutine (spawnWave ());
			waveIndex++;
		}

		//if all enemies of wave have spawned
		if (enemyCnt == groupSize * waveSize) {
			//if all enemies destroyed
			if (!enemiesRemaining ()) {
				if(waveIndex >= roundSize) {
					waveIndex = 0;
					roundIndex++;
					BetweenScenes.CurrentRound = roundIndex;
					SceneManager.LoadScene("Briefing");
				}
				if (roundIndex >= gameLength) {
					PhaseManager.instance.gameOverPrompt ();
				}
				//go into build if not end of game, else game over prompt
				else {
					PhaseManager.instance.enableBuildPhase ();
					TurretManager.instance.setSellState(false);
					PhaseManager.instance.intoSellMode (); 
				}
			}
		}
    }

	bool enemiesRemaining() {
		if (GameObject.FindGameObjectsWithTag ("Code").Length == 0) {
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
            Effects.instance.Wave();
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

	public void setWaveStart(bool flag) {
		waveStart = flag;
	}

	//Updates Round and Wave text on top bar UI
	void updateUI() {
		if(roundIndex <= gameLength) {
			roundUI.GetComponent<Text>().text = "ROUND: " + (roundIndex+1).ToString() + " / " + gameLength.ToString();
		}
		waveUI.GetComponent<Text>().text =  "WAVE:  " + (waveIndex).ToString() + " / " + roundSize.ToString();
	}
}
