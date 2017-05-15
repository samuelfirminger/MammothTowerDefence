using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public bool levelOne;
	public bool briefingEnemy;
	public bool isEnemy;
	public CodeProperties properties;
    
    private float healthPoints;
    private float maxHealthPoints;
	public GameObject healthBar;
//	public TextMesh healthText;

	private int reward; 
	private int attackDamage;

    private float distanceCheck = 0.5f;
  
    //Movement fields
	private float movementSpeed;
    private bool slowState;
	private float slowCooldown;
	private float slowStartTime;
	private float tempSpeed;
    
    //Variables for storing target waypoint
    private Transform waypointTarget;
    private int waypointIndex;

	//Death particle effect
	public GameObject deathEffect;

	void Start () {
		//Get game attributes from parser attributes
		movementSpeed = (float)properties.speed;
		healthPoints = (float)properties.size;
		attackDamage = properties.size;
		reward = properties.size;

		levelOne = true; 
		if(Waypoints.points.Length > 12)
		{
			levelOne = false;
		}

		//Set target to 1st Waypoint
		waypointTarget = Waypoints.points[0];
     

        if (levelOne == false)
		{
			int rand = Random.Range(0, 2);
			if (rand == 1)
			{   
				waypointTarget = Waypoints.points[19];
				waypointIndex = 19;
			}
		}

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
        if(waypointIndex == 19)
        {
            waypointTarget = Waypoints.points[4];
            waypointIndex = 4;
            waypointTarget = Waypoints.points[waypointIndex];
            return;
        }

        if(waypointIndex == 10 && levelOne == false)
        {
            int rand = Random.Range(0, 2);
            if (rand == 1)
            {
                waypointTarget = Waypoints.points[15];
                waypointIndex = 15;
                waypointTarget = Waypoints.points[waypointIndex];
                return;
            }
        }
    

        if(waypointIndex == 14 || waypointIndex == 18)
        {
            waypointTarget = Waypoints.points[3];
            waypointIndex = 2;
        }

        if((waypointIndex == 3 && levelOne == false) || (waypointIndex == 12 && levelOne == true)) { //length of level 1, and longest path.
			if (briefingEnemy) { 
				PlayerStats.instance.decreaseHealth (attackDamage);     
				Debug.Log ("HP = " + PlayerStats.Health);     
			} else {
				PlayerStats.instance.adjustCash(reward);
                BetweenScenes.setPlayerCash(PlayerStats.instance.getCash());
				Debug.Log ("Cash = " + PlayerStats.Cash);
             }
            Sound.instance.rewardSound();
               Effects.instance.EndPoint();
        Destroy(gameObject);
            return;
        }
        
        waypointIndex++;
		if (waypointIndex < Waypoints.points.Length)
		{
			waypointTarget = Waypoints.points[waypointIndex];
		}
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
                PlayerStats.instance.adjustCash(attackDamage);
                
            }
			else {
				PlayerStats.instance.decreaseHealth(attackDamage);
			}
			GameObject particleEffect = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation); 
			Destroy(particleEffect, 0.5f); 
            Destroy(gameObject);
            Sound.instance.deathSound();
        }
		
//		StartCoroutine(showHealthText());
//		healthText.text = "";

    }

//	IEnumerator showHealthText() {
//		float healthPercentage = (healthPoints / maxHealthPoints) * 100;
//		healthText.text = healthPercentage.ToString() + "%";
//		if (healthPercentage >= 50) {
//			healthText.color = Color.green;
//		}
//		else if (healthPercentage < 50 && healthPercentage >= 25) {
//			healthText.color = Color.yellow;
//		}
//		else {
//			healthText.color = Color.red;
//		}
//		healthText.transform.LookAt(Camera.main.transform);
//		yield return new WaitForSeconds(1f);
//	}
    
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

	public float getMovementSpeed() {
		return movementSpeed;
	}
}
