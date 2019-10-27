using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;

[Serializable]
public class Task
{
    public int count;
    public string color;
}

[Serializable]
public class TaskHolder
{
    public List<Task> levelTasks;
}

[Serializable]
public class Level
{
    public List<TaskHolder> allLevels;
}


public class TaskManager : MonoBehaviour {

    static List<TaskHolder> allTasksOfLevels;

    // Use this for initialization
    void Awake() {

        TextAsset levelDataText = Resources.Load("Levels") as TextAsset;
        string jsonRaw = levelDataText.ToString();

        Level level = JsonUtility.FromJson<Level>(jsonRaw);
        if(level.allLevels.Count <= 1)
        {
            //Debug.Log("Levels could not be gathered!");
            //Call Game Manager to Change Level, an error occured
        }
        else
        {
            allTasksOfLevels = level.allLevels;
        }

        //Debug.Log("level.allLevels[0].levelTasks[0]." + allTasksOfLevels[0].levelTasks[0].);

    }

    public static TaskHolder GetLevelTasks(int levelIndex)
    {
        return allTasksOfLevels[levelIndex];
    }

}
