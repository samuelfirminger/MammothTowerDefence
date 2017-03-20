using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TURRETMANAGER: used to set the buildable turret.
//Will allow extra turret types to be added easily in the future.
public class TurretManager : MonoBehaviour { 
    //Make TurretManager a "Singleton Class" : only 1 instance can ever exist
    //See: http://answers.unity3d.com/questions/753488/
    public static TurretManager Instance;
    
    void Awake() {
        Instance = this;
    }
       
    //Turret types: future examples commented out
    public GameObject turretBasicPrefab;
    //public GameObject turretSlowPrefab;
    //public GameObject turretSniperPrefab;
    //public GameObject turretFirePrefab;
    private GameObject turretToBuild;
    
    void Start() {
        turretToBuild = turretBasicPrefab;
    }
    
    public GameObject getTurretToBuild() {
        return turretToBuild;
    }
    
    //Will be used later to add multiple different turret types
    public void setTurretToBuild(GameObject turret) {
        turretToBuild = turret;       
    }
}
