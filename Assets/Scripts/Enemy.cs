using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
    public Color colour;

	//Testing variables for turret targetting
	public bool isEnemy;
	public CodeProperties properties;
    
    private float healthPoints;
    private float maxHealthPoints;
	public GameObject healthBar;

	private int reward; 
	private int attackDamage;

    private float distanceCheck = 0.5f;
  
    //Moevement fields
	private float movementSpeed;
    private bool slowState;
	private float slowCooldown;
	private float slowStartTime;
	private float tempSpeed;
    
    //Variables for storing target waypoint
    private Transform waypointTarget;
    private int waypointIndex;

	void Start () {
		//Get game attributes from parser attributes
		movementSpeed = (float)properties.speed;
		healthPoints = (float)properties.size;
		attackDamage = properties.size;
		reward = properties.size;

		//Set target to 1st Waypoint
		waypointTarget = Waypoints.points[0];

		maxHealthPoints = healthPoints;
        slowState = false;
	}
	
	void Update () {
		//Get which way to point
        Vector3 dir = waypointTarget.position - transform.position;
         
        //Space.world: move relative to world
        //Time.deltaTime: keep speed independent of framerate   
        //Normalized: keeps speed fixed by movementSpeed variable
        transform.Translate(dir.normalized * movementSpeed * Time.deltaTime, Space.World);
        
        //Check if enemy is within distance 
        if(Vector3.Distance(transform.position, waypointTarget.position) <= distanceCheck) {
            GetNextWaypoint();
        }

		//Reset speed after slowCooldown
		if (Time.time > slowStartTime + slowCooldown && slowState) {
			slowState = false;
			movementSpeed = tempSpeed;
		}
	}
    
    void GetNextWaypoint() {
		//What to do at final waypoint
        //Inflict damage if code is "bad"
		//Reward player if code is "good"
        if(waypointIndex >= Waypoints.points.Length - 1) {
			if (isEnemy) {
				PlayerStats.instance.decreaseHealth (attackDamage);     
				Debug.Log ("HP = " + PlayerStats.Health);
                Sound.instance.healthlossSound();
			} else {
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
        
    float calc_Health = healthPoints / maxHealthPoints;
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
		if (!slowState) {
			tempSpeed = movementSpeed;
			slowState = true;
			movementSpeed *= slowFactor;
		}
	}   
}
