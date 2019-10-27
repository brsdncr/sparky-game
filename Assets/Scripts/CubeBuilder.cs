using System.Collections;
using UnityEngine;

public class CubeBuilder : MonoBehaviour
{
    float timePassed = 0f;
    float timeToPassToGenerateNewCube;
    float minPower = 100f;
    float maxPower = 200f;

    float yTransform = 20f;

    string direction;

    GameObject cubes;
    [SerializeField] GameObject cube;

    // Use this for initialization
    void Start()
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
        cubes = GameObject.Find("Cubes");
        timeToPassToGenerateNewCube = Random.Range(3f, 10f);
    }

    private void Update()
    {
        GenerateBlocks();
    }

    private void GenerateBlocks()
    {
        timePassed += Time.deltaTime;
        if (timePassed > timeToPassToGenerateNewCube)
        {
            StartCoroutine(CreateNewCube());
            timePassed = 0f;
        }
    }


    IEnumerator CreateNewCube()
    {
        GameObject newCube = Instantiate(cube, transform.position, Quaternion.identity) as GameObject;
        if(direction == "down")
        {
            newCube.GetComponent<Rigidbody>().velocity = new Vector3(Time.deltaTime * Random.Range(-yTransform, yTransform), -1 * (Time.deltaTime * Random.Range(minPower, maxPower)), 0);
        }
        else
        {
            newCube.GetComponent<Rigidbody>().velocity = new Vector3(Time.deltaTime * Random.Range(minPower, maxPower), Time.deltaTime * Random.Range(-yTransform, yTransform), 0);
        }
        
        newCube.transform.parent = cubes.transform;
        yield return new WaitForSeconds(1f);
    }
}
