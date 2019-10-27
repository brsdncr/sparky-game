using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : MonoBehaviour
{
    void Awake()
    {

        if (Input.touchCount == 3)
        {
            int currentLevel = PlayerPrefs.GetInt("PlayerCurrentLevel", 0);
            if (currentLevel > 0)
            {
                PlayerPrefs.SetInt("PlayerCurrentLevel", 0);
            }
        }

    }
}
