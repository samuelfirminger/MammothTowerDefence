using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;
    private float damage;
    public float speed = 70f;
	public float slowFactor;
	public float slowTime;

	//Splash fields
	public float splashRadius;
	public float splashDamage;
	private string enemyTag = "Code";
	
    //Give bullet a target
    public void Seek(Transform newTarget) {
        target = newTarget;
    }
    
	void Update () {
		//Destroy bullet if target is lost (reaches the end)
        if(target == null) {
            Destroy(gameObject);
            return;
        }
        
        //Calculate bullet direction
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        
        //Check if hit something
        if(dir.magnitude <= distanceThisFrame) {
            HitTarget();
            return;
        }
        
        //Havent hit yet: normalize to obtain constant speed
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}
    
    public void setDamage(float damageValue) {
        damage = damageValue;
    }
       
    void HitTarget() {
        //Deal damage to the target enemy
        //target.dealDamage(damage); <- Subtracts damage value from enemy health bar
        Enemy targetEnemy = target.GetComponent<Enemy>();
        DealDamage.dealDamageToEnemy(targetEnemy, damage);

		//Slow down enemy
        if (slowFactor != 0) {
            slowTarget(targetEnemy);
        }
       
		//Deal splash damage
        if (splashDamage != 0) {
            splashNearby(targetEnemy);
        }
                    
        //Destroy bullet on impact
        Destroy(gameObject);       
    }

	void slowTarget(Enemy targetEnemy) {		
        targetEnemy.setSlow(slowFactor, slowTime);
	}

	void splashNearby(Enemy targetEnemy) {
		//Fill array of GameObjects: all enemies in scene
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		foreach(GameObject enemy in enemies) {
			//Get distance to each enemy
			float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, enemy.transform.position);
			Enemy enemyToCheck = enemy.GetComponent<Enemy>();
			if(distanceToEnemy <= splashRadius) {
				DealDamage.dealDamageToEnemy(enemyToCheck, splashDamage);
			}
		}
	}
}
