using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawnManager : MonoBehaviour { 

    //Types of enemy to spawn
	private Transform[] enemyTypes = new Transform[1];
    public Transform enemyBasicPrefab;
    public Transform enemyFastPrefab;
    public Transform enemySlowPrefab;
  
    public float timeBetweenWaves = 5.5f;
    private float countdown = 2f;
    private int waveIndex = 3;
    
    public Transform spawnPoint;
    
    void Start() {
        //Load all enemy types into array
        enemyTypes[0] = enemyBasicPrefab;
        //enemyTypes[1] = enemyFastPrefab;
        //enemyTypes[2] = enemySlowPrefab;        
    }
    
    void Update() {
        //Start spawning when countdown done
        if(countdown <= 0f) {
            //Calling a Co-routine (using IEnumerator to wait)
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        
        //deltaTime: time since last frame
        countdown -= Time.deltaTime;
    }
    
    //IEnumerator: allows this code to be paused
    //Prevents enemies being created on top of one another
    IEnumerator SpawnWave() {
        //waveIndex++;
        waveIndex = 3;
        for(int i=0 ; i<waveIndex ; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    void SpawnEnemy() {
        //Get a random num corresponding to an enemy type, and spawn
        var index = Random.Range(0, enemyTypes.Length);
        Instantiate(enemyTypes[index], spawnPoint.position, spawnPoint.rotation);
    }
}
