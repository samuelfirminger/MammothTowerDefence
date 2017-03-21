using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float movementSpeed;
    public float attackDamage;
    public float healthPoints;
    public float max_healthPoints;
    private float distanceCheck = 0.2f;
    public GameObject healthBar;
    
    //Variables for storing target waypoint
    private Transform waypointTarget;
    private int waypointIndex;

	void Start () {
        //Set target to 1st waypoint in Waypoints array
        max_healthPoints = healthPoints;
		waypointTarget = Waypoints.points[0];
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
	}
    
    void GetNextWaypoint() {
        //Check if at the end of waypoint path, and destroy enemy if so
        if(waypointIndex >= Waypoints.points.Length - 1) {
            //Future change: instead of just destroy, subtract from some sort of "player health" variable
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
            Destroy(gameObject);
        }
    }
    public void SetHealthBar(float myHealth)
    {
        //myHealth value 0-1 , calculated by maxhealth/Curent health
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
