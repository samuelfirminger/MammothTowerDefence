using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawnManager : MonoBehaviour { 

    //Type of enemy to spawn
	public Transform enemyPrefab;
    public float timeBetweenWaves = 5.5f;
    private float countdown = 2f;
    private int waveIndex = 0;
    
    public Transform spawnPoint;
    
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
        waveIndex++;
        for(int i=0 ; i<waveIndex ; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    void SpawnEnemy() {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
