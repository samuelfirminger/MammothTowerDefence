using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;
    public float speed = 70f;
	
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
	}
}
