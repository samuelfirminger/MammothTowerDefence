using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script relating to bullets instantiated by turrets: handles special effects from slow and splash turrets
public class Bullet : MonoBehaviour {

	private Transform target;
    public float baseDamage;
    public float speed;

	//Slow fields
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
        
        //Calculate bullet direction and rotation
        Vector3 dir = target.position - transform.position;
		transform.rotation = Quaternion.LookRotation(dir);

        float distanceThisFrame = speed * Time.deltaTime;
        
        //Check if hit something
        if(dir.magnitude <= distanceThisFrame) {
            HitTarget();
            return;
        }
        
        //Haven't hit yet: normalize to obtain constant speed
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}
       
    void HitTarget() {
        Enemy targetEnemy = target.GetComponent<Enemy>();
        dealDamage(targetEnemy, baseDamage);

		//Slow down enemy
        if (slowFactor > 0) {
            slowTarget(targetEnemy);
        }

		//Deal splash damage
        if (splashDamage > 0) {
            splashNearby(targetEnemy);
        }
 
        //Destroy bullet on impact
        Destroy(gameObject);       
    }

	void slowTarget(Enemy target) {		
        target.setSlow(slowFactor, slowTime);
	}

	void splashNearby(Enemy targetEnemy) {
		//Fill array of GameObjects: all enemies in scene
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		foreach(GameObject enemy in enemies) {
			//Get distance to each enemy
			float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, enemy.transform.position);
			Enemy enemyToCheck = enemy.GetComponent<Enemy>();
			if(distanceToEnemy <= splashRadius) {
				dealDamage(enemyToCheck, splashDamage);
			}
		}
	}

	void dealDamage(Enemy target, float damage) {
		target.setHealth(target.getHealth() - damage);
	}

}
