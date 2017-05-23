using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Script for any button that enables the user to go from a menu to a level
public class ButtonNextLevel : MonoBehaviour
{
    public void NextLevelButton(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void NextLevelButton(string levelName)
	{   try {
			if(levelName == "StartScreen") {
				BetweenScenes.clearAllData();
			}
		}
		catch (System.NullReferenceException e) {
			//do nothing 
		}
        SceneManager.LoadScene(levelName);
    }

    public void SetCurrentLevel(string levelName)
    {
        BetweenScenes.CurrentLevel = levelName;
        BetweenScenes.CurrentRound = 0;
        BetweenScenes.setArraySize(BetweenScenes.getCurrentLevelId());
    }
}