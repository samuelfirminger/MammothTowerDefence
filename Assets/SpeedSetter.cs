using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSetter : MonoBehaviour {

    public void setTime(int time)
    {
        Time.timeScale = time;
    }
}
