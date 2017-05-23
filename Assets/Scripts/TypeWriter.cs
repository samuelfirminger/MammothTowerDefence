using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script prints a section of the text to the canvas letter by letter
public class TypeWriter : MonoBehaviour {
	
    public Text txt;
    string story;
    public bool PlayOnAwake = true;
	public bool toRemove = false; 
	//float to detemine the speed of the text
	public float Delay;
    public float BetweenCharsDelay;

    void Awake()
    {
        txt = GetComponent<Text>();
        if (PlayOnAwake)
            twAnimation(txt.text, Delay);
    }

    //Update text and start typewriter effect
    public void twAnimation(string text, float delay= 0f)
    {
        StopCoroutine(animateText()); //stop Coroutime if exist
        story = text;
        txt.text = ""; //clean text
		Invoke("Start_AnimateText", delay); //Invoke effect
    }

    void Start_AnimateText()
    {
        StartCoroutine(animateText());
    }

	//print character by character, pause the typewrite rmusic when finished
    IEnumerator animateText()
    {
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(BetweenCharsDelay);
        }
		//if the text needs to be wiped from the screen
		if (toRemove) {
			txt.text = "";
		}

		try {
		    AudioSource typeSound = GameObject.FindGameObjectWithTag ("TypeWriter").GetComponent<AudioSource> (); 
			typeSound.Stop ();
		}
		catch (System.NullReferenceException e) {
			//do nothing
		}

    }


}
