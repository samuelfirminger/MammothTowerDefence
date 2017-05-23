using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingTurretsScript : MonoBehaviour {
    public GameObject Node;
    private GameObject tempNode;
    private Node nodeScript;
    private GameObject[] NodeArr;
    public TurretSpec[] turretSpec;
    /*
    public GameObject enemyPrefab;
    private GameObject testEnemy;
    private Enemy testEnemyScript;
    private int MAX_WAYPOINTS = 5;
    private float OFFSET = 5f;
    public GameObject waypointPrefab;
    */

    // Use this for initialization
    void Start () {
        testBuild();

        testSell();

        int errCnt = 0;
        for (int i = 0; i < turretSpec.Length; ++i)
        {
            Destroy(NodeArr[i].GetComponent<Node>().builtTurret);

            if (string.IsNullOrEmpty(NodeArr[i].GetComponent<Node>().builtTurret.name))
            {
                Debug.Log("Turret cannot be sold:" + turretSpec[i].name);
                ++errCnt;
            }
        }
        Debug.Log("Errors found Selling turrets: " + errCnt);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    /*
        void generateWaypoints()
        {
            Vector3 pos;
            Waypoints.points = new Transform[MAX_WAYPOINTS];

            for (int i = 0; i < MAX_WAYPOINTS; i++)
            {
                if (i % 2 == 0) pos = new Vector3(OFFSET, 0f, 0f);
                else pos = new Vector3(OFFSET * -1f, 0f, 0f);
                GameObject temp = Instantiate(waypointPrefab, pos, Quaternion.identity);
                Waypoints.points[i] = temp.transform;
            }

            Debug.Assert(Waypoints.points.Length == MAX_WAYPOINTS,
                         "Failed to create waypoint array...");
        }

        void createEnemy(Vector3 pos)
        {
            testEnemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
            testEnemyScript = testEnemy.GetComponent<Enemy>();
            testEnemyScript.setLevelOne(true);
            testEnemyScript.setIsEnemy(true);

            Debug.Assert(testEnemy != null, "Failed to spawn enemy...");
        }
    */

    private void testSell()
    {


    }

    private void testBuild()
    {
        int errCnt = 0;
        NodeArr = new GameObject[turretSpec.Length];

        for (int i = 0; i < turretSpec.Length; ++i)
        {
            tempNode = Instantiate(Node, new Vector3(i * 5.0f, 0, 0), Quaternion.identity);
            NodeArr[i] = tempNode;
            nodeScript = tempNode.GetComponent<Node>();
            nodeScript.buildTurret(turretSpec[i]);

            if (string.IsNullOrEmpty(NodeArr[i].GetComponent<Node>().builtTurret.name))
            {
                Debug.Log("Turret cannot be built:" + turretSpec[i].name);
                ++errCnt;
            }
        }
        Debug.Log("Errors found building turrets: " + errCnt);

    }
}
