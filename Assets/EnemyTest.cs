using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Run this script to test:
//Enemy movement
//Turret targetting
//Turret shooting
//Bullet damage
public class EnemyTest : MonoBehaviour {

	//Use these to change testing parameters
	private static int MAX_WAYPOINTS = 5;
	private static Vector3 ORIGIN = new Vector3(0f, 0f, 0f);
	private static float OFFSET = 5f;

	private bool testsComplete = true;

	public GameObject waypointPrefab;

	public GameObject enemyPrefab;
	private GameObject testEnemy;
	private Enemy testEnemyScript;

	public GameObject turretPrefab;
	private GameObject testTurret;
	private Turret testTurretScript;

	// Use this for initialization
	void Start () {
		Debug.Log("Running EnemyTest script...");

		Debug.Log("Generating waypoints...");
		generateWaypoints();

		Debug.Log("Spawning enemy...");
		createEnemy(ORIGIN);

		Debug.Log("Spawning turret...");
		createTurret(new Vector3(0f, 0f, OFFSET));

		Debug.Log("Spawning complete...");
		testsComplete = false;
	}
	
	// Update is called once per frame
	void Update () {
		

		if(testEnemyScript.getWaypointIndex() >= MAX_WAYPOINTS && !testsComplete) {
			Debug.Log("Last waypoint reached...");

			Debug.Assert(testEnemyScript.getHealth() < testEnemyScript.getMaxHealth(),
						 "Failed to deal damage to enemy...");

			Debug.Log("Removing enemy...");
			Destroy(testEnemy);

			Debug.Log("Removing turret...");
			Destroy(testTurret);

			Debug.Log("Tests complete!");
			testsComplete = true;
		}
	}

	//Create a path for the enemy to follow
	void generateWaypoints() {
		Vector3 pos;
		Waypoints.points = new Transform[MAX_WAYPOINTS];

		for(int i = 0; i < MAX_WAYPOINTS; i++) {
			if(i % 2 == 0) pos = new Vector3(OFFSET, 0f, 0f);
			else pos = new Vector3(OFFSET * -1f, 0f, 0f);
			GameObject temp = Instantiate(waypointPrefab, pos, Quaternion.identity);
			Waypoints.points[i] = temp.transform;
		}

		Debug.Assert(Waypoints.points.Length == MAX_WAYPOINTS,
					 "Failed to create waypoint array...");
	}

	//Spawn in an enemy at the given position
	void createEnemy(Vector3 pos) {
		testEnemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
		testEnemyScript = testEnemy.GetComponent<Enemy>();
		testEnemyScript.setLevelOne(true);
		testEnemyScript.setIsEnemy(true);

		Debug.Assert(testEnemy != null, "Failed to spawn enemy...");
	}

	void createTurret(Vector3 pos) {
		testTurret = Instantiate(turretPrefab, pos, Quaternion.identity);

		Debug.Assert(testTurret != null, "Failed to spawn turret...");
	}
}
