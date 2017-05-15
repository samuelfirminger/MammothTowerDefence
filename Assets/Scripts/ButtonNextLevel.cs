using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonNextLevel : MonoBehaviour

{

    public void NextLevelButton(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void NextLevelButton(string levelName)
    {   
		if(levelName == "StartScreen") {
			BetweenScenes.clearAllData();
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