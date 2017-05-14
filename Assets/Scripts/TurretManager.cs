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
        
        loadTurretData();
    }
	
    private void loadTurretData() {
        int n, currentLevel = BetweenScenes.getCurrentLevelId();
        Debug.Log("ATTEMPTING TO LOAD TURRET DATA");
        GameObject tempTurret = null;
        GameObject tempNode = null;
        
        //Determine the number of nodes to check, dependant on level
        switch(currentLevel) {
            case(1) : n = BetweenScenes.getNodeNum(1); Debug.Log("GOT NODENUM1 = " + n); break;
            case(2) : n = BetweenScenes.getNodeNum(2); break;
            default : n = 0; break;
        }
   
        for(int i=0 ; i<n ; i++) {
            tempTurret = BetweenScenes.getTurretData(i);
            if(tempTurret != null) {
                Debug.Log("TURRET FOUND ON NODE " + (i+1));
                tempNode = GameObject.Find(Convert.ToString(i+1));
                //turretToBuild = tempTurret.GetComponent<TurretSpec>();
                //buildTurret(tempNode.GetComponent<Node>());
                buildLoadedTurret(tempNode.GetComponent<Node>(), tempTurret);
            }
        }
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
			Debug.Log ("Not enough cash to build");
			return;
		}
        
        //Store the value returned if the turret on this node is sold
        node.setSellValue(turretToBuild.cost/2);
		PlayerStats.instance.adjustCash(-(turretToBuild.cost));

        buildTurret(node);
		//GameObject turret = (GameObject)Instantiate (turretToBuild.prefab, node.getBuildPosition(), Quaternion.identity);
		//node.builtTurret = turret;
        
        storeTurretData(nodeName);       
        Sound.instance.placeTurretSound();

		Debug.Log ("Turret built. Cash left = " + PlayerStats.Cash);
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
		Debug.Log ("Sell mode activated"); 
	}
		
	public void sellTurret(Node node, string nodeName) {
        int nodeId = Int32.Parse(nodeName);
        BetweenScenes.removeTurretData(nodeId);
        
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
