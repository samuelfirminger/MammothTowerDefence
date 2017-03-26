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
		if (!turretManager.canBuild && turretManager.sell == false) {
			Debug.Log ("Whaddup"); 
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

        //Check for pre-existing built turret
		if (builtTurret != null && turretManager.sell == true) {
			turretManager.sellTurret (this); 
			return;
		} else if (builtTurret == null && turretManager.sell == true) {
			return;
		}
		else if (!turretManager.canBuild) {
			return; 
		}
		else if(builtTurret != null) {
			return ;
		}
			
		turretManager.createTurretOn (this);
    }
}
