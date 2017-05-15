using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BriefingManager : MonoBehaviour {
    public static BriefingManager instance;
    
    //How data will enter this briefingManager
    //BetweenScenes.CurrentLevel = level1
    //BetweenScenes.CurrentRound = 0
     
    private int activeLevel = 1;
    private int activeRound  = 1;
    
    //Naming convention of arrays: 1_1 corresponds to level1, round1
    private GameObject[] cards1_1, cards1_2, cards1_3, cards1_4;
    private GameObject[] cards2_1, cards2_2, cards2_3;
    
    void Awake() {
		if (instance != null) {
			Debug.Log ("More than one briefingManager in scene."); 
			return; 
		}    
        
        instance = this;
        
        //Collect Cards from scene, then hide them
        findCards();
        activeRound = BetweenScenes.CurrentRound + 1;
        getCards();
    }
    
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void getLevelAndRound() {
        //Comment out when BetweenScenes has been implemented
        /*if(BetweenScenes.CurrentLevel.Equals("Level 1")) {
            activeLevel = 1;
        } else if(BetweenScenes.CurrentLevel.Equals("Level 2")) {
            activeLevel = 2;
        }
        
        activeRound = BetweenScenes.CurrentRound;*/
    }
    
    public void findCards() {
        if(activeLevel == 1) {
            cards1_1 = GameObject.FindGameObjectsWithTag("card1_1"); hideCards(cards1_1);
            cards1_2 = GameObject.FindGameObjectsWithTag("card1_2"); hideCards(cards1_2);
            cards1_3 = GameObject.FindGameObjectsWithTag("card1_3"); hideCards(cards1_3);
            cards1_4 = GameObject.FindGameObjectsWithTag("card1_4"); hideCards(cards1_4);
        } else if (activeLevel == 2) {
            cards2_1 = GameObject.FindGameObjectsWithTag("card2_1"); hideCards(cards2_1);
            cards2_2 = GameObject.FindGameObjectsWithTag("card2_2"); hideCards(cards2_2);
            cards2_3 = GameObject.FindGameObjectsWithTag("card2_3"); hideCards(cards2_3);
        }
    }
    
    public void getCards() {
        switch(activeLevel) {
            case 1 :
                switch(activeRound) {               
                    case 1 : getCards1_1(); break;
                    case 2 : getCards1_2(); break;
                    case 3 : getCards1_3(); break;
                    case 4 : getCards1_4(); break;
                } break;      
            case 2 : 
                switch(activeRound) {               
                    case 1 : getCards2_1(); break;
                    case 2 : getCards2_2(); break;
                    case 3 : getCards2_3(); break;
                } break;          
        }
    }
    
    //~~~~ W E T   C O D E ~~~~
    public void getCards1_1() {
        hideCards(cards1_2); hideCards(cards1_4);
        hideCards(cards1_3); showCards(cards1_1);
    }   
    
    public void getCards1_2() {
        hideCards(cards1_1); hideCards(cards1_4);
        hideCards(cards1_3); showCards(cards1_2);
    }
    
    public void getCards1_3() {
        hideCards(cards1_1); hideCards(cards1_4);
        hideCards(cards1_2); showCards(cards1_3);
    }  
    
    public void getCards1_4() {
        hideCards(cards1_1); hideCards(cards1_3);
        hideCards(cards1_2); showCards(cards1_4);
    }  
    
    public void getCards2_1() {
        hideCards(cards2_2);
        hideCards(cards2_3);
        showCards(cards2_1);
    }
    
    public void getCards2_2() {
        hideCards(cards2_1);
        hideCards(cards2_3);
        showCards(cards2_2);
    }
    
    public void getCards2_3() {
        hideCards(cards2_1);
        hideCards(cards2_2);
        showCards(cards2_3);
    }
    
    
    private void hideCards(GameObject[] cards) {
        foreach (GameObject card in cards) {
            card.SetActive(false);
        }
    }
    
    private void showCards(GameObject[] cards) {
        foreach (GameObject card in cards) {
            card.SetActive(true);
        }
    }
}
