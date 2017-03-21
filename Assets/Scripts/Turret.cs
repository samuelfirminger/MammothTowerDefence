﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public float range = 15f;
    public float fireRate = 1f;
    private float cooldown = 0f;
    private Transform target;
   
    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;
    
	// Use this for initialization
	void Start () {
        //Need to constantly update to allow turrets to change targets frequently
        //Parameters: function name, when to run first, how often to repeat
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
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
        //Get bullet script access
        GameObject firedBullet = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bullet = firedBullet.GetComponent<Bullet>();
        
        if(bullet != null) {
            bullet.Seek(target);
        }
    }
    
    void UpdateTarget() {
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
    
    //Use to display turret range when turret is selected
    void OnDrawGizmosSelected() {
        //Draw at turret position, radius of size range;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}