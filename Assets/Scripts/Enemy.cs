using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    EnemySpawnManager enemySpawnManager;
    
    //Enemy colour:
    private Renderer rend;
    public Color badColour;
    public Color goodColour;

    //Flag variable: is this code object an Enemy?
    private bool isEnemy = false;
    
    public float movementSpeed;
    public int attackDamage;
    public float healthPoints;
    public float max_healthPoints;
    private float distanceCheck = 0.5f;
    public GameObject healthBar;
  
    //Slow fields
    private int slowState;
	private float slowCooldown;
	private float slowStartTime;
	private float tempSpeed;
    
    //Testing variables for turret targetting
	public CodeProperties properties;
    //Variables for storing target waypoint
    private Transform waypointTarget;
    private int waypointIndex;

	void Start () {
        //Get spawnManager instance
        enemySpawnManager = EnemySpawnManager.instance;
        
        //Set target to 1st waypoint in Waypoints array
        max_healthPoints = healthPoints;
		waypointTarget = Waypoints.points[0];
        rend = GetComponent<Renderer>();
        slowState = 0;
	}
	
	void Update () {
		//Get which way to point
        Vector3 dir = waypointTarget.position - transform.position;
        
        //Translate: moves enemy    
        //Space.word: move relative to world world
        //Time.deltaTime: keep speed indepenant of framerate.   
        //Normalized: keeps speed fixed by movementSpeed variable
        transform.Translate(dir.normalized * movementSpeed * Time.deltaTime, Space.World);
        
        //Check if enemy is within distance 
        if(Vector3.Distance(transform.position, waypointTarget.position) <= distanceCheck) {
            GetNextWaypoint();
        }

		//Reset speed after slowCooldown
		if (Time.time > slowStartTime + slowCooldown && slowState == 1) {
			slowState = 0;
			movementSpeed = tempSpeed;
		}
	}
    
    void GetNextWaypoint() {
        //Check if at the end of waypoint path, and destroy enemy if so 
        //Inflict damage if code is "bad" code (an enemy)
        if(waypointIndex >= Waypoints.points.Length - 1) {
			if (isEnemy) {
				PlayerStats.instance.decreaseHealth (attackDamage);     
				Debug.Log ("HP = " + PlayerStats.Health);
                Sound.instance.healthlossSound();
			} else { 
				//hard code reward at this stage, can be replaced later if 
				//different types of good code
				int reward = 100; 
				PlayerStats.instance.adjustCash (reward);
				Debug.Log ("Cash = " + PlayerStats.Cash);
                Sound.instance.rewardSound(); 
			}
            Destroy(gameObject);
            return;
        }
        
        waypointIndex++;
        waypointTarget = Waypoints.points[waypointIndex];
    }
    
    public float getHealth() {
        return healthPoints;
    }
    
    public void setHealth(float newHealth) {
        healthPoints = newHealth;
        
    float calc_Health = healthPoints / max_healthPoints;
        SetHealthBar(calc_Health);

        if(healthPoints <= 0) {
           
            if (isEnemy) {
                PlayerStats.instance.adjustCash(10);
                
            }
            Destroy(gameObject);
            Sound.instance.deathSound();
        }
    }
    
    public void SetHealthBar(float myHealth) {
        //myHealth value 0-1 , calculated by maxhealth/Curent health
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
    
	public void setSlow(float slowFactor, float slowTime) {
		slowStartTime = Time.time;
		slowCooldown = slowTime;
		if (slowState == 0) {
			tempSpeed = movementSpeed;
			slowState = 1;
			movementSpeed *= slowFactor;
		}
	}   
}
