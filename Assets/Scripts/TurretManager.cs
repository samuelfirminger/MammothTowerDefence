using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TURRETMANAGER: used to set the buildable turret.
//Will allow extra turret types to be added easily in the future.
public class TurretManager : MonoBehaviour { 
    //Make TurretManager a "Singleton Class" : only 1 instance can ever exist
    //See: http://answers.unity3d.com/questions/753488/
    public static TurretManager instance;
    
    void Awake() {
		if (instance != null) {
			Debug.Log ("More than one turretManager in scene."); 
			return; 
		}

        instance = this;
    }
		
    //Turret types: future examples commented out
    public GameObject turretBasicPrefab;
	public GameObject turretSlowPrefab; 
	//public GameObject turretSlowPrefab;
    //public GameObject turretSniperPrefab;
    //public GameObject turretFirePrefab;
    private GameObject turretToBuild;
    
	//set turret to build from the Shop script
	public void setTurretToBuild(GameObject turret) { 
		turretToBuild = turret; 
	}
    
    public GameObject getTurretToBuild() {
        return turretToBuild;
    }
    
   
}
