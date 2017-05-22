using UnityEngine;
using System.Collections;

public class Waypoints : MonoBehaviour {

    //Hold waypoints in an array of GameObjects 
    //Static: can be accessed from anywhere without reference.
	public static Transform[] points;
    
    //Load all children of "Waypoint" object into array
    void Awake() {
        points = new Transform[transform.childCount];
        for(int i = 0; i < points.Length; i++) {
            points[i] = transform.GetChild(i);
        }      
      
    }
}