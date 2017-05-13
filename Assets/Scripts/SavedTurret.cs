using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedTurret {
    private GameObject turretPrefab;
    private float x;
    private float y;
    private float z;      
    
    public SavedTurret(GameObject turret, float xPos, float yPos, float zPos) {
        this.turretPrefab = turret;
        this.x = xPos; this.y = yPos; this.z = zPos;
    }
    
    public GameObject getTurret() {
        return turretPrefab;
    }
    
    public float getX() {
        return x;
    }
    
    public float getY() {
        return y;
    }
    
    public float getZ() {
        return z;
    }
}