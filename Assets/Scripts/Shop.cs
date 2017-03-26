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
		resetSellButton ();
		turretManager.chooseTurretToBuild (turretBasic); 
	}


	public void chooseSlowTurret() {
		Debug.Log("Slowing Turret purchased"); 
		resetSellButton (); 
		turretManager.chooseTurretToBuild (turretSlow); 
    }
    
    public void buySplashTurret() {
        Debug.Log("Splash Turret purchased");
		resetSellButton (); 
		turretManager.chooseTurretToBuild (turretSplash);
    }

	//if currently in sell mode and shop button is clicked,
	//return to buy mode, alternatively, user can click on shop turret button
	private void resetSellButton() {
		TurretManager.instance.sell = false;
		PhaseManager.instance.intoSellMode (); 
	}
}