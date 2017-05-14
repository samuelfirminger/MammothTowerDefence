using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedData {
    //Set equal to the size of the level (node grid)
    //Level 1: 96 nodes (8x12 : some obscured by path and scenery)
    //         Top-Left node     = (0, 0, 0)
    //         Bottom-Right node = (33, 0, -19)
    // Add Vector3 to move between nodes:
    //    Moving right 1 node: add (3, 0, 0);
    //    Moving down  1 node: add (0, 0, -21);
    
    //Divide any (x,y,z) of a node by 3 to get it's co-ordinates
    //relative to the top left, where top left is (0,0,0)
    
    private SavedTurret[][] savedTurrets;
    private Vector3 offset = new Vector3(3,0,3);
    
    private int player_cash;
    private int player_health;
    private int level;
    private int round;
    
    public void setArraySize(int level) {
        //Set the arraySize = to size of level 1
        if(level == 1) {
            //savedTurrets = new SavedTurret[8][12];    
        } else if(level == 2) {
            //How big is level 2?
            //savedTurrets = new SavedTurret[0][0];
        }
    }
    
    public void saveTurret(GameObject turretToSave, Transform position) {
        
    }
}