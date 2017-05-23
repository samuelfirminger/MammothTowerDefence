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
   	
	private bool sell; 
    
    void Awake() {       
		if (instance != null) {

			return; 
		}

        instance = this;
		sell = false; 
        
        loadTurretData();
    }
	
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
   
        for(int i=0 ; i<n ; i++) {
            tempTurret = BetweenScenes.getTurretData(i);
            if(tempTurret != null) {
                tempNode = GameObject.Find(Convert.ToString(i+1));
                //turretToBuild = tempTurret.GetComponent<TurretSpec>();
                //buildTurret(tempNode.GetComponent<Node>());
                buildLoadedTurret(tempNode.GetComponent<Node>(), tempTurret);
            }
        }
    }
    
    //Turret types: future examples commented out
    public GameObject turretBasicPrefab;
	public GameObject turretSlowPrefab; 
	public GameObject turretSplashPrefab;
    public GameObject turretSniperPrefab;
    //public GameObject turretFirePrefab;
	private TurretSpec turretToBuild;

	public bool canBuild { 
		get { 
			return turretToBuild != null; 
		} 
	}

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
    
    private void buildTurret(Node node) {
        GameObject turret = (GameObject)Instantiate (turretToBuild.prefab, node.getBuildPosition(), Quaternion.identity);
		node.builtTurret = turret;
    }
    
    private void buildLoadedTurret(Node node, GameObject tempTurret) {
        GameObject turret = (GameObject)Instantiate(tempTurret, node.getBuildPosition(), Quaternion.identity);
        node.builtTurret = turret;
    }
    
    private void storeTurretData(string nodeName) {
        int nodeId = Int32.Parse(nodeName);
        BetweenScenes.storeTurretData(nodeId,turretToBuild.prefab);
    }
    
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
