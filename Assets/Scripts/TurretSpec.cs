using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for use by the shop and nodes to store the appropriate turret prefab
//and its cost
[System.Serializable]
public class TurretSpec : MonoBehaviour {
	public GameObject prefab;
	public int cost;
}
