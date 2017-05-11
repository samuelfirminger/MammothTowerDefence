using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
    public Color colour;

	private bool isEnemy;
	public bool briefingEnemy;
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

	//particle effect
	public GameObject deathEffect ;

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
		isEnemy = true; 
	}
	
	void Update () {
		//Get which way to point
        Vector3 dir = waypointTarget.position - transform.position;
         
        //Space.world: move relative to world
        //Time.deltaTime: keep speed independent of framerate   
        //Normalized: keeps speed fixed by movementSpeed variable
        transform.Translate(dir.normalized * movementSpeed * Time.deltaTime, Space.World);
		transform.LookAt(waypointTarget);

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

	public void setIsEnemy(bool isEnemy){
		this.isEnemy = isEnemy;
	}

	public bool getIsEnemy(){
		return isEnemy;
	}
    
    void GetNextWaypoint() {
		//What to do at final waypoint
        //Inflict damage if code is "bad"
		//Reward player if code is "good"
        if(waypointIndex >= Waypoints.points.Length - 1) {
			if (briefingEnemy) {
				PlayerStats.instance.decreaseHealth(attackDamage);     
				Debug.Log("HP = " + PlayerStats.Health);
                Sound.instance.healthlossSound();
			} else {
				PlayerStats.instance.adjustCash(reward);
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
           
            if (briefingEnemy) {
                PlayerStats.instance.adjustCash(10);
                
            }
			else {
				PlayerStats.instance.decreaseHealth(1);
			}
			GameObject particleEffect = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation); 
			Destroy(particleEffect, 0.5f); 
            Destroy(gameObject);
            Sound.instance.deathSound();
        }
    }
    
    public void SetHealthBar(float myHealth) {
        //myHealth value 0-1 , calculated by maxhealth/Current health
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
