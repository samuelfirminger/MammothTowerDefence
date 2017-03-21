using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public float range = 15f;
    public float fireRate = 1f;
    private float cooldown = 0f;
    private Transform target;
   
    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;
    public Transform firePoint;
    
	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
        //Check if cooldown time has passed, and shoot if so.
		if(cooldown <= 0f) {
            Shoot();
            cooldown = 1f/ fireRate;
        }
        
        cooldown -= Time.deltaTime;
	}
    
    void Shoot() {
        //Requires bullet script;
    }
    
    void updateTarget() {
        //Fill array of GameObjects: all enemies in scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        //Find the nearest enemy
        foreach(GameObject enemy in enemies) {
            //Get distance to each enemy
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        
        //Check if found an enemy with our range, and set target
        if(nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform;
        } else {
            target = null;
        }
        
    }
}
