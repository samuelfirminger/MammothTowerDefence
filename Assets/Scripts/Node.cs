using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColor;
	private Color startColor;
    private Renderer rend;
    
	[Header("Turret Building Fields")]
    public GameObject builtTurret;
	//Y-axis offset for placing
	//turret above node
	private Vector3 offset = new Vector3(0f, 0.1f);
    private int sellValue = 0;
    
	TurretManager turretManager;
    
    void Start() {
        //Get the renderer of node, which holds info about material
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

		turretManager = TurretManager.instance;
    }

	public Vector3 getBuildPosition() {
		return transform.position + offset ;
	}
       
    void OnMouseEnter() {
		if (!turretManager.canBuild && turretManager.getSellState() == false) {			
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
		if (builtTurret != null && turretManager.getSellState() == true) {
			turretManager.sellTurret(this, this.name); 
			return;
		} else if (builtTurret == null && turretManager.getSellState() == true) {
			return;
		}
		else if (!turretManager.canBuild) {
			return; 
		}
		else if(builtTurret != null) {
			return ;
		}
			
		turretManager.createTurretOn(this, this.name);
    }
    
    public void setSellValue(int value) {
        sellValue = value;
    }
    
    public int getSellValue() {
        return sellValue;
    }
}
