using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColor;
    
    private Renderer rend;
    private Color startColor;
    
    //Turret Building Variables:
    private GameObject builtTurret;  
    public Vector3 offset;
    
    void Start() {
        //Get the renderer of node, which holds info about material
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
       
    void OnMouseEnter() {
        //Change colour when mouse over
        rend.material.color = hoverColor;
    }
    
    void OnMouseExit() {
        //Change colour back once mouse leaves
        rend.material.color = startColor;
    }
    
    void OnMouseDown() {
        //Check for pre-existing built turret
        if(builtTurret != null) {
            Debug.Log("Cannot build over pre-existing turret!");
            return;
        }
        
        //Build a turret - TurretManager will let more turret types be used in the future
        //GameObject turretToBuild = TurretManager.GetTurretToBuild();
        //turret = (GameObject)Instantiate(turretToBuild,transform.position + positionOffset,transform.rotation);
        
    }
}
