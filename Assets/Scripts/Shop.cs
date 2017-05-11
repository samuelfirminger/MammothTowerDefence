using System.Collections;
using UnityEngine;
using UnityEngine.UI; 

public class Shop : MonoBehaviour {

	public TurretSpec turretBasic;
	public TurretSpec turretSlow;
	public TurretSpec turretSplash;
	public TurretSpec turretSniper; 


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

	public void buySniperTurret() { 
		Debug.Log ("Sniper Turret Purchased"); 
		resetSellButton (); 
		turretManager.chooseTurretToBuild (turretSniper);
	}

	//if currently in sell mode and shop button is clicked,
	//return to buy mode, alternatively, user can click on shop turret button
	private void resetSellButton() {
		TurretManager.instance.setSellState(false);
		PhaseManager.instance.intoSellMode (); 
	}
}