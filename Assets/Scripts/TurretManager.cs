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
	private bool sell; 
    
    void Awake() {
		if (instance != null) {
			Debug.Log ("More than one turretManager in scene."); 
			return; 
		}
               
        classification = new int[1];
        setClassification(1); //This to be moved somewhere else: classification of enemy changes between rounds (shoot red enemies one round, shoot blue and speed=2 enemies another round etc...)
        instance = this;
		sell = false; 
    }
	
    public void setClassification(int property) {
        classification[0] = property;
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

	public bool canBuild { 
		get { 
			return turretToBuild != null; 
		} 
	}

	public void createTurretOn(Node node) {

		if (PlayerStats.Cash < turretToBuild.cost) {
			Debug.Log ("Not enough cash to build");
			return;
		}
        
        //Store the value returned if the turret on this node is sold
        node.setSellValue(turretToBuild.cost/2);
		PlayerStats.instance.adjustCash(-(turretToBuild.cost));

		GameObject turret = (GameObject)Instantiate (turretToBuild.prefab, node.getBuildPosition (), Quaternion.identity);
		node.builtTurret = turret;
        Sound.instance.placeTurretSound();

		Debug.Log ("Turret built. Cash left = " + PlayerStats.Cash);
	}
    
	public void chooseTurretToBuild(TurretSpec turret) {
		turretToBuild = turret;
	} 
		
	//revert the sell button and update text on button
	public void sellMode() {
		sell = !sell; 
		PhaseManager.instance.intoSellMode(); 
		Debug.Log ("Sell mode activated"); 
	}
		
	public void sellTurret(Node node) {
		Destroy (node.builtTurret); 
		PlayerStats.instance.adjustCash (+(node.getSellValue()));
        Sound.instance.sellTurretSound();
		Debug.Log ("Turret sold."); 
	}

	public bool getSellState() {
		return sell;
	}

	public void setSellState(bool state) {
		sell = state; 
	}


}
