using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TURRETMANAGER: used to set the buildable turret.
//Will allow extra turret types to be added easily in the future.
public class TurretManager : MonoBehaviour { 
    //Make TurretManager a "Singleton Class" : only 1 instance can ever exist
    //See: http://answers.unity3d.com/questions/753488/
    public static TurretManager instance;
   	
	//Turret types
	public GameObject turretBasicPrefab;
	public GameObject turretSlowPrefab; 
	public GameObject turretSplashPrefab;
	public GameObject turretSniperPrefab;
	private TurretSpec turretToBuild;

	private bool sell; 
    
    void Awake() {       
		if (instance != null) {

			return; 
		}
        instance = this;
		sell = false; 
        
        loadTurretData();
    }
		
	//checks for, if any, turrets saved between scenes
    private void loadTurretData() {
        int n, currentLevel = BetweenScenes.getCurrentLevelId();
        GameObject tempTurret = null;
        GameObject tempNode = null;
        
        //Determine the number of nodes to check, dependant on level
        switch(currentLevel) {
            case(1) : n = BetweenScenes.getNodeNum(1); break;
            case(2) : n = BetweenScenes.getNodeNum(2); break;
            default : n = 0; break;
        }
        
		//if a turret needs to be built, build the same turret 
		//as the same old scene
        for(int i=0 ; i<n ; i++) {
            tempTurret = BetweenScenes.getTurretData(i);
            if(tempTurret != null) {
                tempNode = GameObject.Find(Convert.ToString(i+1));
                buildLoadedTurret(tempNode.GetComponent<Node>(), tempTurret);
            }
        }
    }
    

	public bool canBuild { 
		get { 
			return turretToBuild != null; 
		} 
	}


	//create a turret on the appropriate node
	public void createTurretOn(Node node, string nodeName) {

		if (PlayerStats.Cash < turretToBuild.cost) {
			return;
		}
        
        //Store the value returned if the turret on this node is sold
        node.setSellValue(turretToBuild.cost/2);
		PlayerStats.instance.adjustCash(-(turretToBuild.cost));

        buildTurret(node);
        
        storeTurretData(nodeName);       
        Sound.instance.placeTurretSound();
        
        BetweenScenes.setPlayerCash(PlayerStats.instance.getCash());
	}
    
	//build a turret
    private void buildTurret(Node node) {
        GameObject turret = (GameObject)Instantiate (turretToBuild.prefab, node.getBuildPosition(), Quaternion.identity);
		node.builtTurret = turret;
    }
    
	//build a loaded turret
    private void buildLoadedTurret(Node node, GameObject tempTurret) {
        GameObject turret = (GameObject)Instantiate(tempTurret, node.getBuildPosition(), Quaternion.identity);
        node.builtTurret = turret;
    }
    
	//store the turret data
    private void storeTurretData(string nodeName) {
        int nodeId = Int32.Parse(nodeName);
        BetweenScenes.storeTurretData(nodeId,turretToBuild.prefab);
    }
    
	//for use with the turret shop to store what type of turret to build
	public void chooseTurretToBuild(TurretSpec turret) {
		turretToBuild = turret;
	} 
		
	//revert the sell button and update text on button
	public void sellMode() {
		sell = !sell; 
		PhaseManager.instance.intoSellMode(); 
	}
		
	public void sellTurret(Node node, string nodeName) {
        int nodeId = Int32.Parse(nodeName);
        BetweenScenes.removeTurretData(nodeId);
        //Player is refunded half the cost of the turret
		PlayerStats.instance.adjustCash(node.builtTurret.GetComponent<Turret>().cost / 2);
		Destroy(node.builtTurret); 
        Sound.instance.sellTurretSound();
        BetweenScenes.setPlayerCash(PlayerStats.instance.getCash());
	}

	public bool getSellState() {
		return sell;
	}

	public void setSellState(bool state) {
		sell = state; 
	}


}
