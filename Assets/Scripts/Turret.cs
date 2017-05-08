using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public float range = 15f;
    public float fireRate = 1f;
    public float baseDamage;
	public float turnSpeed = 10f; 
    private float cooldown = 0f;
    private Transform target;
	public Transform partToRotate ; 
   
    private string enemyTag = "Code";
    public GameObject bulletPrefab;
    
    private int[] userVariables = new int[1];
    
	// Use this for initialization
	void Start () {
        //This will be replaced by some interpreter function from the drag&drop:
        //setting = 1 here is as if the user says "if [enemy type in briefing] target"
        userVariables[0] = 1;
        
        //Need to constantly update to allow turrets to change targets frequently
        //Parameters: function name, when to run first, how often to repeat
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        enemyTag = "Code";
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

        //Check if cooldown time has passed, and shoot if so.
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
				bullet.setDamage(baseDamage);
				bullet.Seek(target);
			}
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
            Enemy enemyToCheck = enemy.GetComponent<Enemy>();
                       
            if(distanceToEnemy < shortestDistance) {
                if (checkIfEnemy(enemyToCheck.properties) == 1) {                
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
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
    
    //Check the enemy properties against what the user has programmed the turret to fire at
    //Return 1 if enemy meets the users specification, otherwise return 0
    int checkIfEnemy(int[] enemyProperties) {
        for(int i=0 ; i<userVariables.Length ; i++) {
            if(enemyProperties[i] != userVariables[i]) {
                return 0;
            }
        }
        
        return 1;
    }
}
