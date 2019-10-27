using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeBuilderForLevel : MonoBehaviour
{
    float timePassed = 0f;
    float timeToPassToGenerateNewCube;
    float firstWaitingTimeToGenerateCube;
    float minPower = 100f;
    float maxPower = 200f;

    float yTransform = 20f;

    string direction;

    GameObject cubes;

    [SerializeField] GameObject redCube;
    [SerializeField] GameObject greenCube;
    [SerializeField] GameObject blueCube;
    [SerializeField] GameObject yellowCube;

    int builderId;

    Queue<Color> cubeQueue;

    // Use this for initialization
    void Start()
    {
        SetBuilderType();
        SetBuilderVariables();
        
        InvokeRepeating("CreateNewCube", firstWaitingTimeToGenerateCube, timeToPassToGenerateNewCube);
    }

    private void SetBuilderVariables()
    {
        cubes = GameObject.Find("Cubes");
        timeToPassToGenerateNewCube = Random.Range(1f, 3f);
        firstWaitingTimeToGenerateCube = Random.Range(2f, 8f);
        //Debug.Log("timeToPassToGenerateNewCube for " + builderId + ": " + timeToPassToGenerateNewCube);
        cubeQueue = new Queue<Color>();
    }

    private void SetBuilderType()
    {
        if (transform.CompareTag("DownBuilder"))
        {
            direction = "down";
        }
        else if (transform.CompareTag("RightBuilder"))
        {
            direction = "right";
        }
        else
        {
            direction = "right";
        }
    }

    public void SetId(int id)
    {
        builderId = id;
    }


    void CreateNewCube()
    {
        if (cubeQueue.Count > 0)
        {
            Color color = cubeQueue.Dequeue();
            GameObject newCube;
            if (color.Equals(Constants.Red))
            {
                newCube = Instantiate(redCube, transform.position, Quaternion.identity) as GameObject;
            }
            else if (color.Equals(Constants.Green))
            {
                newCube = Instantiate(greenCube, transform.position, Quaternion.identity) as GameObject;
            }
            else if (color.Equals(Constants.Blue))
            {
                newCube = Instantiate(blueCube, transform.position, Quaternion.identity) as GameObject;
            }
            else if (color.Equals(Constants.Yellow))
            {
                newCube = Instantiate(yellowCube, transform.position, Quaternion.identity) as GameObject;
            }
            else
            {
                newCube = Instantiate(blueCube, transform.position, Quaternion.identity) as GameObject;
            }


            if (direction == "down")
            {
                newCube.GetComponent<Rigidbody>().velocity = new Vector3(Time.deltaTime * Random.Range(-yTransform, yTransform), -1 * (Time.deltaTime * Random.Range(minPower, maxPower)), 0);
            }
            else
            {
                newCube.GetComponent<Rigidbody>().velocity = new Vector3(Time.deltaTime * Random.Range(minPower, maxPower), Time.deltaTime * Random.Range(-yTransform, yTransform), 0);
            }

            newCube.transform.parent = cubes.transform;
            //yield return new WaitForSeconds(timeToPassToGenerateNewCube);
        }
    }



public void BuildCube(Color color)
{
    cubeQueue.Enqueue(color);
    //Debug.Log("I'm creating a " + color  + " cube!"  );
}
}
