using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Script used on buttons that enable the user to load a specific level
public class LevelLoader : MonoBehaviour
{
    public void NextLevelButton(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void NextLevelButton(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}