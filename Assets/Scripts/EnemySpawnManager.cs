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
	private int[,] roundEnemies;

	//How many rounds in a game
	private int gameLength;
	//Which round you are on
	private int roundIndex;
	//How many waves in a round
	private int roundSize;
	//Which wave you are on
	private int waveIndex;
	//How many groups in a wave
	private int waveSize;
	//Which group you are on
	private int groupIndex;
	//How many spawns in a group
	private int groupSize;
	//How many enemies have we spawned
	private int enemyCnt;

	private float timeBetweenSpawns = 0.5f;
	private float timeBetweenGroups = 5f; 
	private bool waveStart = false ;
    
    public Transform spawnPoint;
 
    //For simplified programming of turrets: this variable holds
    //the value corresponding to the enemy type for this wave and the
    //last spawned enemy type (used in enemy class)
    //(will be visually shown in briefing)
    private int briefingEnemy = 0;
    private int lastSpawned   = 0;

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

		gameLength = 4;

		groupIndex = 0;
		waveIndex = 0;
		roundIndex = 0;

		initialiseRound();
    }

    void Update() {
		updateUI();
		//start wave start of game/new wave
		if (waveStart && waveIndex < roundSize) {
			initialiseRound();
			waveStart = false; 
			StartCoroutine(spawnWave());
			waveIndex++;
		}

		//if all enemies of wave have spawned
		if (enemyCnt == groupSize * waveSize) {
			//if all enemies destroyed
			if (!enemiesRemaining ()) {
				if(waveIndex >= roundSize) {
					waveIndex = 0;
					groupIndex = 0;
					roundIndex++;
					BetweenScenes.CurrentRound = roundIndex;
					BetweenScenes.setPlayerCash(PlayerStats.instance.getCash());
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
//        briefingEnemy = Random.Range(0, enemyTypes.Length);
//        Debug.Log("================ briefingEnemy = " + briefingEnemy + " ====================");
		for (int i = 0; i < waveSize; i++) {
			if (enemyCnt % groupSize == 0) {
				StartCoroutine(spawnGroup());
			}
			yield return new WaitForSeconds(timeBetweenGroups);
		}
	}
		
	IEnumerator spawnGroup() {
        for(int j = 0 ; j < groupSize; j++) {
			spawnEnemy(j);
            Effects.instance.Wave();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
		groupIndex++;
    }
    
	void spawnEnemy(int j) {
        //Spawn next enemy from premade
		//list according to round and wave
		int index = roundEnemies[groupIndex, j];
        lastSpawned = index;
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

	void initialiseRound() {
		roundIndex = BetweenScenes.CurrentRound;
		switch(roundIndex)
		{
		case 0:
			roundSize = 1;
			waveSize = 5;
			groupSize = 5;
			roundEnemies = new int[,]
			{
				{1, 0, 0, 0, 1},
				{1, 0, 1, 0, 1},
			    {1, 0, 0, 0, 1},
				{1, 0, 1, 0, 1},
				{1, 1, 1, 1, 1}
			};
			break;
		case 1:
			roundSize = 3;
			waveSize = 3;
			groupSize = 5;
			roundEnemies = new int[,]
			{
				{0, 1, 1, 1, 0},
				{0, 1, 2, 1, 0},
				{2, 1, 1, 1, 2},
				{0, 1, 0, 1, 0},
				{2, 1, 0, 1, 2},
				{0, 1, 0, 1, 0},
				{2, 1, 0, 1, 2},
				{0, 1, 0, 1, 0},
				{2, 1, 2, 1, 2}
			};
			break;
		case 2:
			roundSize = 3;
			waveSize = 4;
			groupSize = 5;
			roundEnemies = new int[,]
			{
				{1, 0, 0, 0, 1},
				{1, 0, 3, 0, 1},
				{4, 0, 3, 0, 4},
				{1, 0, 3, 0, 1},
				{1, 0, 3, 0, 1},
				{3, 4, 1, 4, 3},
				{4, 0, 3, 0, 1},
				{4, 0, 3, 0, 1},
				{1, 4, 1, 4, 1},
				{1, 4, 3, 4, 1},
				{1, 4, 3, 4, 1},
				{4, 1, 4, 1, 4}
			};
			break;
		case 3:
			roundSize = 17;
			waveSize = 1;
			groupSize = 5;
			roundEnemies = new int[,]
			{
				{2, 0, 1, 0, 2},
				{1, 2, 1, 2, 1},
				{2, 0, 2, 0, 2},
				{1, 2, 1, 2, 1},
				{2, 2, 4, 2, 2},
				{1, 4, 1, 4, 1},
				{2, 0, 2, 0, 2},
				{1, 4, 1, 4, 1},
				{4, 2, 2, 2, 4},
				{1, 4, 1, 4, 1},
				{2, 0, 2, 0, 2},
				{1, 4, 1, 4, 1},
				{2, 0, 2, 0, 2},
				{1, 4, 1, 4, 1},
				{2, 4, 2, 4, 2},
				{1, 4, 1, 4, 1},
				{4, 2, 4, 2, 4}
			};
			break;
		default:
			return;
		}
	}

}
