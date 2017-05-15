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
		//start of wave conditions
		if (waveStart && waveIndex < roundSize) {
			atStartOfWave();
		}

		//end of wave conditions -> 3 branches
		//    end of wave
		//	  end of round
		//    gameover
		if (enemyCnt == groupSize * waveSize && !enemiesRemaining() && PlayerStats.Health > 0) {
			if(waveIndex >= roundSize) {
				atEndOfRound();
			}
			if (roundIndex >= gameLength) {
				PhaseManager.instance.phaseUI.SetActive(false);
				PhaseManager.instance.gameOverPrompt();
			}
			//go into build if not end of game, else game over prompt
			else {
				atEndOfWave();
			}
		}
	}

	void atStartOfWave() {
		initialiseRound();
		waveStart = false; 
		StartCoroutine(spawnWave());
		waveIndex++;
	}

	void atEndOfWave() {
		PhaseManager.instance.enableBuildPhase ();
		TurretManager.instance.setSellState(false);
		PhaseManager.instance.intoSellMode (); 
	}

	void atEndOfRound() {
		waveIndex = 0;
		groupIndex = 0;
		roundIndex++;
		BetweenScenes.CurrentRound = roundIndex;
		BetweenScenes.setPlayerCash(PlayerStats.instance.getCash());
		BetweenScenes.setPlayerHealth(PlayerStats.Health);
		Time.timeScale = 1;
		if(roundIndex < gameLength) {
			SceneManager.LoadScene("Briefing");
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
        //Spawn next enemy from premade array
		Instantiate(enemyTypes[roundEnemies[groupIndex, j]], spawnPoint.position, spawnPoint.rotation);
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
			roundSize = 2;
			waveSize = 3;
			groupSize = 5;
			roundEnemies = new int[,]
			{
				{1, 0, 0, 0, 1},
				{1, 0, 1, 0, 1},
			    {1, 0, 0, 0, 1},
				{1, 0, 1, 0, 1},
				{1, 1, 1, 1, 1},
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
			roundSize = 4;
			waveSize = 4;
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
				{1, 4, 1, 4, 5}
			};
			break;
		default:
			return;
		}
	}

}
