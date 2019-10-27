using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

    //Level Tasks
    bool gameStarted = false;
    int redCount = 0;
    int greenCount = 0;
    int blueCount = 0;
    int yellowCount = 0;

    List<Task> levelTasks;


    Text yellowCounterText;
    Text redCounterText;
    Text blueCounterText;
    Text greenCounterText;

    [SerializeField] LevelChanger lc;
    [SerializeField] Dictionary<int, CubeBuilderForLevel> cubeBuilders;
    List<CubeBuilderForLevel> availableCubeBuilders;

    

    private void Awake()
    {
        Time.timeScale = 1;
    }

    // Use this for initialization
    void Start () {
        SetLevelTasks();
        InitUI();
        SetRemainings();
		InitCubeBuilders();
        InitAvailableBuilders();
        CreateAllCubesInLevel();
        gameStarted = true;
        lc.FadeLevelIn();
    }

    private void CreateAllCubesInLevel()
    {
        for (int i = 0; i < IncreaseCubeNumberByPercentage(redCount); i++)
        {
            CreateNewCubeWithColor(Constants.Red);
        }

        for (int i = 0; i < IncreaseCubeNumberByPercentage(blueCount); i++)
        {
            CreateNewCubeWithColor(Constants.Blue);
        }

        for (int i = 0; i < IncreaseCubeNumberByPercentage(greenCount); i++)
        {
            CreateNewCubeWithColor(Constants.Green);
        }

        for (int i = 0; i < IncreaseCubeNumberByPercentage(yellowCount); i++)
        {
            CreateNewCubeWithColor(Constants.Yellow);
        }
    }

    private int IncreaseCubeNumberByPercentage(int count)
    {
        int totalNumberOfCubes = redCount + greenCount + blueCount + yellowCount;
        //float percOfRed = redCount/totalNumberOfCubes

        if(count > 0) {
            return count + (totalNumberOfCubes / count);
        }

        int lvlNo = lc.GetLevelNumber();
        //Debug.Log("GetLevelNumber: " + lvlNo);
        return lvlNo - 2;
    }




    //For Level
    private void SetLevelTasks()
    {
        levelTasks = TaskManager.GetLevelTasks(SceneManager.GetActiveScene().buildIndex - Constants.level1BuildIndex).levelTasks;
        string taskMsg = "This level's task is: ";
        foreach (var task in levelTasks)
        {
            string taskcolor = task.color;
            int count = task.count;
            switch (taskcolor)
            {
                case "red":
                    redCount += Convert.ToInt32(count);
                    break;
                case "green":
                    greenCount += Convert.ToInt32(count);
                    break;
                case "blue":
                    blueCount += Convert.ToInt32(count);
                    break;
                case "yellow":
                    yellowCount += Convert.ToInt32(count);
                    break;
                default:
                    blueCount += Convert.ToInt32(count);
                    break;
            }
            taskMsg += " " + count + " x " + taskcolor + " ";
        }

        //Debug.Log("levelTask:" + taskMsg);
    }



    

    //For Player
    public void LoseGame()
    {
        //Time.timeScale = 0;
        lc.StartGameOver();
    }

    private void WinLevel()
    {
        lc.FadeLevelOut();
        //Time.timeScale = 0;
    }

    void InitUI()
    {
        yellowCounterText = GameObject.Find("YellowChanger").GetComponentInChildren<Text>();
        redCounterText = GameObject.Find("RedChanger").GetComponentInChildren<Text>();
        blueCounterText = GameObject.Find("BlueChanger").GetComponentInChildren<Text>();
        greenCounterText = GameObject.Find("GreenChanger").GetComponentInChildren<Text>();
    }

    void SetRemainings()
    {
        if(yellowCount > -1)
        {
            yellowCounterText.text = yellowCount.ToString();
        }
        else if(yellowCount == 0)
        {
            yellowCounterText.text = "";
        }

        if(redCount > -1)
        {
            redCounterText.text = redCount.ToString();
        }
        else if (redCount == 0)
        {
            redCounterText.text = "";
        }

        if (blueCount > -1)
        {
            blueCounterText.text = blueCount.ToString();
        }
        else if (blueCount == 0)
        {
            blueCounterText.text = "";
        }

        if (greenCount > -1)
        {
            greenCounterText.text = greenCount.ToString();
        }
        else if (greenCount == 0)
        {
            greenCounterText.text = "";
        }
    }

    //For Cubes
    private void InitCubeBuilders()
    {
        cubeBuilders = new Dictionary<int, CubeBuilderForLevel>();

        int indexTracker = 0;
        foreach (CubeBuilderForLevel builder in GameObject.Find("Builders").GetComponentsInChildren<CubeBuilderForLevel>())
        {
            cubeBuilders.Add(indexTracker, builder);
            builder.SetId(indexTracker);
            indexTracker++;
        }
    }

    private void InitAvailableBuilders()
    {
        availableCubeBuilders = new List<CubeBuilderForLevel>();
        foreach (int key in cubeBuilders.Keys)
        {
            CubeBuilderForLevel cubeBuilder = cubeBuilders[key];
            availableCubeBuilders.Add(cubeBuilder);
        }
    }

    public void InformExplosion(Color color, bool explodedOnTrigger)
    {
        if (explodedOnTrigger)
        {
            CreateNewCubeWithColor(color);

        }
        else
        {
            if (color.Equals(Constants.Blue))
            {
                if (blueCount > 0)
                {
                    blueCount -= 1;
                }
            }
            else if (color.Equals(Constants.Red))
            {
                if (redCount > 0)
                {
                    redCount -= 1;
                }
            }
            else if (color.Equals(Constants.Yellow))
            {
                if (yellowCount > 0)
                {
                    yellowCount -= 1;
                }
            }
            else if (color.Equals(Constants.Green))
            {
                if (greenCount > 0)
                {
                    greenCount -= 1;
                }
            }
        }



        if (redCount + greenCount + blueCount + yellowCount == 0 && gameStarted)
        {
            WinLevel();
        }
        SetRemainings();

    }

    private void CreateNewCubeWithColor(Color color)
    {
        int randomBuilder = UnityEngine.Random.Range(0, availableCubeBuilders.Count);
        availableCubeBuilders[randomBuilder].BuildCube(color);
    }
}
