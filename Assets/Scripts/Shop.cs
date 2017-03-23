using System.Collections;
using UnityEngine;

public class Shop : MonoBehaviour {

	public TurretSpec turretBasic;
	public TurretSpec turretSlow;
	public TurretSpec turretSplash;

	TurretManager turretManager ; 

	void Start() { 
		turretManager = TurretManager.instance; 
	}

	public void chooseStandardTurret () {
		Debug.Log("Standard Turret purchased"); 
		turretManager.chooseTurretToBuild (turretBasic); 
	}


	public void chooseSlowTurret() {
		Debug.Log("Slowing Turret purchased"); 
		turretManager.chooseTurretToBuild (turretSlow); 
    }
    
    public void buySplashTurret() {
        Debug.Log("Splash Turret purchased");
		turretManager.chooseTurretToBuild (turretSplash);
    }
		
}