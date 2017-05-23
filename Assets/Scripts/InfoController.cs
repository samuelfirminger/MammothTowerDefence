using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script used to display and hide information panels in the briefing scene
public class InfoController : MonoBehaviour {

    public GameObject info1;
    public GameObject info2;
    public GameObject info3;
    public GameObject info4;
    public GameObject info5;
 
    public void showInfo(int enemyType) {
        switch(enemyType) {
            //ANTI-VIRUS
            case(1) : info1.SetActive(true); break;
            //MALWARE
            case(2) : info2.SetActive(true); break;
            //MACRO
            case(3) : info3.SetActive(true); break;
            //SPYWARE
            case(4) : info4.SetActive(true); break;
            //TROJAN
            case(5) : info5.SetActive(true); break;
        }
    }
    
    public void closeInfo(int enemyType) {
        switch(enemyType) {
            //ANTI-VIRUS
            case(1) : info1.SetActive(false); break;
            //MALWARE
            case(2) : info2.SetActive(false); break;
            //MACRO
            case(3) : info3.SetActive(false); break;
            //SPYWARE
            case(4) : info4.SetActive(false); break;
            //TROJAN
            case(5) : info5.SetActive(false); break;
        }
    }
   
}
