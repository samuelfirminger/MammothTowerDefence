using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColor;
    private Renderer rend;
    private Color startColor;
    
    //Turret Building Variables:
	[Header("Optional parameter")]
    public GameObject builtTurret;  
    public Vector3 offset;

	TurretManager turretManager ;
    
    void Start() {
        //Get the renderer of node, which holds info about material
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

		turretManager = TurretManager.instance;
    }

	public Vector3 getBuildPosition() {
		return transform.position + offset;
	}
       
    void OnMouseEnter() {
		if (!turretManager.canBuild) {
			return;
		}

        //Change colour when mouse over
        rend.material.color = hoverColor;
    }
    
    void OnMouseExit() {
        //Change colour back once mouse leaves
        rend.material.color = startColor;
    }
    
    void OnMouseDown() {

		if (!turretManager.canBuild) {
			return;
		}

        //Check for pre-existing built turret
        if(builtTurret != null) {
            Debug.Log("Cannot build over pre-existing turret!");
            return;
        }

		turretManager.createTurretOn (this);
    }
}
