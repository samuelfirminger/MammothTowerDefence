using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TURRETMANAGER: used to set the buildable turret.
//Will allow extra turret types to be added easily in the future.
public class TurretManager : MonoBehaviour { 
    //Make TurretManager a "Singleton Class" : only 1 instance can ever exist
    //See: http://answers.unity3d.com/questions/753488/
    public static TurretManager instance;
    
    public int[] classification;
    
    void Awake() {
		if (instance != null) {
			Debug.Log ("More than one turretManager in scene."); 
			return; 
		}
               
        classification = new int[2];
        setClassification(3,4); //This to be moved somewhere else: classification of enemy changes between rounds (shoot red enemies one round, shoot blue and speed=2 enemies another round etc...)
        instance = this;
    }
	
    public void setClassification(int property0, int property1) {
        classification[0] = property0;
        classification[1] = property1;
    }
    
    //Set the properties that define an enemy: will change between rounds
    //Should this be in turret manager? Probably not.   
    public int getClassification(int index) {
        return classification[index];
    }
    
    //Turret types: future examples commented out
    public GameObject turretBasicPrefab;
	public GameObject turretSlowPrefab; 
	public GameObject turretSplashPrefab;
    //public GameObject turretSniperPrefab;
    //public GameObject turretFirePrefab;
	private TurretSpec turretToBuild;

	public bool canBuild { get { return turretToBuild != null; } }

	public void createTurretOn(Node node) {

		if (PlayerStats.Cash < turretToBuild.cost) {
			Debug.Log ("Not enough cash to build");
			return;
		}

		PlayerStats.instance.adjustCash(-(turretToBuild.cost));

		GameObject turret = (GameObject)Instantiate (turretToBuild.prefab, node.getBuildPosition (), Quaternion.identity);
		node.builtTurret = turret;

		Debug.Log ("Turret built. Cash left = " + PlayerStats.Cash);
	}
    
	public void chooseTurretToBuild(TurretSpec turret) {
		turretToBuild = turret;
	} 
}
