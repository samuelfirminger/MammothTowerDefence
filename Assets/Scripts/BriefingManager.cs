using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BriefingManager : MonoBehaviour {
    public static BriefingManager instance;
    private GameObject[] cardsLevel1, cardsLevel2, cardsLevel3;
    
    //public RectTransform cardPrefab;
    //public RectTransform cardHolder;
    
    //public Sprite portrait0;
    //public Sprite portrait1;
    //public Sprite portrait2;   
    //private Image portraitImage;
    
    void Awake() {
		if (instance != null) {
			Debug.Log ("More than one briefingManager in scene."); 
			return; 
		}    
        Debug.Log("Set singleton briefingManager");
        instance = this;
        
        //Collect Cards from scene
        cardsLevel1 = GameObject.FindGameObjectsWithTag("CardLevel1");
        hideCards(cardsLevel1);
        cardsLevel2 = GameObject.FindGameObjectsWithTag("CardLevel2");
        hideCards(cardsLevel2);
        cardsLevel3 = GameObject.FindGameObjectsWithTag("CardLevel3"); 
        hideCards(cardsLevel3);
        
        Debug.Log("L1: " + cardsLevel1.Length);
        Debug.Log("L2: " + cardsLevel2.Length);
        Debug.Log("L3: " + cardsLevel3.Length);
    }
    
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    /*public void createCard(string title, Sprite portrait, float speed, 
                    float size, string extension, bool encryption, 
                    string source) {
        Debug.Log("Started creating a card");               
        //var newCard = Instantiate(cardPrefab, new Vector3(10,10,10), Quaternion.identity);

        //newCard.transform.parent = cardHolder;
        newCard.transform.SetParent(cardHolder.transform, false);
        Debug.Log("Finished creating a card");
    }*/
   
    public void getCards1() {
        hideCards(cardsLevel2);
        hideCards(cardsLevel3);
        showCards(cardsLevel1);
    }   
    
    public void getCards2() {
        hideCards(cardsLevel1);
        hideCards(cardsLevel3);
        showCards(cardsLevel2);
    }
    
    public void getCards3() {
        hideCards(cardsLevel1);
        hideCards(cardsLevel2);
        showCards(cardsLevel3);
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
