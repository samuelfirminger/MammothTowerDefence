using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Effects : MonoBehaviour
{
    public static Effects instance;
    public ParticleSystem end;
    public ParticleSystem lose;
    public ParticleSystem wave;


    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Effects activated");
            return;
        }
        instance = this;
    }

    public void EndPoint()
    {
        end.Emit(20);
    }

    public void GameLost()
    {
        lose.Play();

    }

    public void Wave()
    {
        wave.Emit(20);
    }



}