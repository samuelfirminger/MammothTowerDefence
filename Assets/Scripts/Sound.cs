using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

    public static Sound instance;
   
    //sound
    public AudioClip deathClip;
    public AudioClip rewardClip;
    public AudioClip loseHealthClip;
    public AudioClip placeTurretClip;
    public AudioClip newWaveClip;
    public AudioClip sellTurretClip;
    public AudioClip gameOverClip;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Sound activated");
            return;
        }
        instance = this;
    }

    public void deathSound() {
        AudioSource.PlayClipAtPoint(deathClip, transform.position);
    }

    public void rewardSound()
    {
        AudioSource.PlayClipAtPoint(rewardClip, transform.position);
    }

    public void healthlossSound()
    {
        AudioSource.PlayClipAtPoint(loseHealthClip, transform.position);
    }

    public void placeTurretSound()
    {
        AudioSource.PlayClipAtPoint(placeTurretClip, transform.position);
    }

    public void newWaveSound()
    {
        AudioSource.PlayClipAtPoint(newWaveClip, transform.position);
    }

    public void sellTurretSound()
    {
        AudioSource.PlayClipAtPoint(sellTurretClip, transform.position);
    }

    public void gameOverSound()
    {
        AudioSource.PlayClipAtPoint(gameOverClip, transform.position);
    }

}
