using System.Collections;
using UnityEngine;
using UnityEngine.UI; 

public class Shop : MonoBehaviour {

	public TurretSpec[] turrets; 
	private TurretManager turretManager;

	void Start() { 
		turretManager = TurretManager.instance; 
	}

	public void chooseTurret(int i) {
		resetSellButton ();
		turretManager.chooseTurretToBuild(turrets[i]); 
	}

	//if currently in sell mode and shop button is clicked,
	//return to buy mode, alternatively, user can click on shop turret button
	private void resetSellButton() {
		TurretManager.instance.setSellState(false);
		PhaseManager.instance.intoSellMode(); 
	}
}
