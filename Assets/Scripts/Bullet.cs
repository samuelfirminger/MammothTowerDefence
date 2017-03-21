using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;
    public float speed = 70f;
	
    //Give bullet a target
    public void Seek(Transform newTarget) {
        target = newTarget;
        if(target != null){
            Debug.Log("Bullet acquired target");
        }
    }
    
	void Update () {
		//Destroy bullet if target is lost (reaches the end)
        if(target == null) {
            Destroy(gameObject);
            Debug.Log("NoTarget bullet destroyed");
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
    
    void HitTarget() {
        //Destroy bullet on impact
        Destroy(gameObject);
        
        //Will need to check health here and destroy enemy if necessary
    }
}
