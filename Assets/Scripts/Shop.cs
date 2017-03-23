using System.Collections;
using UnityEngine;

public class Shop : MonoBehaviour {


	TurretManager turretManager ; 

	void Start() { 
		turretManager = TurretManager.instance; 
	}

	public void buyStandardTurret () {
		Debug.Log("Standard Turret purchased"); 
		turretManager.setTurretToBuild (turretManager.turretBasicPrefab); 
	}


	public void buySlowTurret() {
		Debug.Log("Slowing Turret purchased"); 
		turretManager.setTurretToBuild (turretManager.turretSlowPrefab); 
    }
    
    public void buySplashTurret() {
        Debug.Log("Splash Turret purchased");
        turretManager.setTurretToBuild (turretManager.turretSplashPrefab);
    }
		
}