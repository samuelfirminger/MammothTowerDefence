//BetweenScenes serialises data for storage between scenes.
//This enables crucial player data to be stored despite the gameflow taking the player across scenes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BetweenScenes {

	//Set these to change starting statistics
	private static int INITIAL_CASH = 450;
	private static int INITIAL_HEALTH = 100;

	public static Draggable[] parsedInstructionSet = null;
    public static string CurrentLevel = "Level 1";
    public static int CurrentRound = 0;
    public static int CurrentWave  = 0;
	public static int playerCash = INITIAL_CASH;
    public static int playerHealth = INITIAL_HEALTH;

	// For drag and drop scene
	public static string transName = null;
    
    //52 Nodes in level 1, 175 in level 2
    private static int nodeNum1 = 52;
    private static int nodeNum2 = 175;
    
    //Arrays for storing turrets between scenes:
    //TurretMarkers stores a 1 if a node has a turret, or 0 if not
    public static GameObject[] TurretMarkers;
    
    public static GameObject DragNDrop;
    public static GameObject LevelScene;   
    
    static void Awake() {
        initialiseArrays();
    }
    
    public static void setArraySize(int level) {
        //Use to change size of TurretMarkers when switching to a level with a different number of nodes
        switch(level) {
            case(1) : TurretMarkers = new GameObject[nodeNum1];  break;
            case(2) : TurretMarkers = new GameObject[nodeNum2];  break;
            default : break;
        }
    }
    
    public static void initialiseArrays() {
        //Sets all entries of TurretMarker array to null : null is used to check for data or not in TurretManager
        for(int i=0 ; i<nodeNum1; i++) {
            TurretMarkers[i] = null;
        }
    }
    
    public static void storeTurretData(int n, GameObject turret) {
        //Stores a turret in TurretMarkers; n-1 because the name of the node (1, 2, 3...) is 1 more than index in array
        TurretMarkers[n-1] = turret;
    }
    
    public static void removeTurretData(int n) {
        //Remove a turret from TurretMarkers: use when selling turrets
        TurretMarkers[n-1] = null;
    }
    
    public static void clearAllData() {
        //Clear all stored turrets: use when moving away permanently from a level (game over, back to menu, to new level...)
        for(int i=0 ; i<TurretMarkers.Length ; i++) {
            TurretMarkers[i] = null;
        }
        playerCash   = INITIAL_CASH;
        playerHealth = INITIAL_HEALTH;
    }
    
    public static GameObject getTurretData(int n) {
        //Return the type of turret stored in a particular index: used for rebuilding turrets in TurretManager once scene is reloaded
        return TurretMarkers[n];
    }
    
    public static int getNodeNum(int level) {
        //Get the number of nodes based on the current level;
        switch(level) {
            case(1) : return nodeNum1;
            case(2) : return nodeNum2;
            default : return 0;
        }
    }
    
    public static int getCurrentLevelId() {
        //Returns an integer represting the current level
        if(CurrentLevel.Equals("Level 1")) {
            return 1;
        } else if(CurrentLevel.Equals("Level 2")) {
            return 2;
        }
        
        return 0;
    }
    
    //Setters & Getters for player statistics: call whenever leaving a level temporarily
    public static void setPlayerCash(int c) {
        playerCash = c;
    }
    
    public static int getPlayerCash() {
        return playerCash;
    }
    
    public static void setPlayerHealth(int h) {
        playerHealth = h;
    }
    
    public static int getPlayerHealth() {
        return playerHealth;
    }
}
