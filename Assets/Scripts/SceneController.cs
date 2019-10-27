using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public void StartGameScene()
	{
		SceneManager.LoadScene("GameScene");
	}

    public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}


	public void StartNextScene()
	{
        //PlayerPrefs.SetInt("PlayerCurrentLevel", SceneManager.GetActiveScene().buildIndex - 2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadBestLevel()
    {
        //int currentLevel = PlayerPrefs.GetInt("PlayerCurrentLevel", 0);
        //SceneManager.LoadScene(Constants.level1BuildIndex + currentLevel);
        SceneManager.LoadScene(3);
    }

    public int GetCurrentLevelIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
