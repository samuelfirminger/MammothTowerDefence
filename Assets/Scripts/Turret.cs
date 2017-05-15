using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public float minRange;
    public float maxRange;
    public float fireRate;
	public float turnSpeed; 
    private float cooldown;

    private Transform target;

	public Transform partToRotate; 
   
    public GameObject bulletPrefab;

//	TODO: Add bullet switching

//	Variables for switching ammunition
//	Experimental, for basic turrets only
//	[Header("Ammo Switching")]
//	public bool canSwitch;
//	public int switchCooldown;
//	public GameObject[] bulletPrefabs;
//	private int enemiesInRange;
    
	void Start () {
        //Need to constantly update to allow turrets to change targets frequently
        //Parameters: function name, when to run first, how often to repeat
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
	void Update () {

		if (target == null) {
			return; 
		}

		//rotate the turret 
		Vector3 dir = target.position - transform.position; 
		Quaternion lookRotation = Quaternion.LookRotation (dir); 
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; 

		partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f); 

        //Check if cooldown time has passed then shoot
		if(cooldown <= 0f) {
            Shoot();
            cooldown = 1f/ fireRate;
        }
        cooldown -= Time.deltaTime;
	}
    
    void Shoot() {
        //Get bullet script access
		if (PhaseManager.instance.getStartState() == true) {
			GameObject firedBullet = (GameObject)Instantiate (bulletPrefab, transform.position, transform.rotation);        
			Bullet bullet = firedBullet.GetComponent<Bullet> ();

			if(bullet != null) {
				bullet.Seek(target);
			}
		}  
    }
    
    void UpdateTarget() {
        //Fill array of GameObjects: all enemies in scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Code");

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        //Find the nearest enemy
        foreach(GameObject enemy in enemies) {
            //Get distance to each enemy
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            Enemy enemyToCheck = enemy.GetComponent<Enemy>();
                       
            if(distanceToEnemy < shortestDistance && distanceToEnemy > minRange && distanceToEnemy < maxRange) {
                if (checkIfEnemy(enemyToCheck)) {                
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }
        
        //Check if found an enemy with our range, and set target
		if(nearestEnemy != null) {
            target = nearestEnemy.transform;
        } else {
            target = null;
        }   
    }
    
    //Use to display turret range when turret is selected
    void OnDrawGizmosSelected() {
        //Draw at turret position, radius of size range;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }
    
    //Check the enemy properties against what the user has programmed the turret to fire at
    bool checkIfEnemy(Enemy enemy) {
		if(enemy.getIsEnemy()) return true;
		else return false;
    }

}
