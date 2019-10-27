using System.Collections.Generic;
using UnityEngine;

public class LevelCubeScript : MonoBehaviour
{

    [HideInInspector] [SerializeField] new Renderer renderer;

    [SerializeField] GameObject blueExplosion;
    [SerializeField] GameObject redExplosion;
    [SerializeField] GameObject yellowExplosion;
    [SerializeField] GameObject greenExplosion;

    AudioManager audioManager;
    GameManager gameManager;

    Vector3 newCubeRotateAmount;

    GameObject explosions;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        explosions = GameObject.Find("Explosions");

        newCubeRotateAmount = new Vector3(Random.Range(0f, 90f), Random.Range(0f, 90f));
        //Debug.Log("My init : " + renderer.material.color);
    }

    private void Update()
    {
        transform.Rotate(newCubeRotateAmount * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (collision.transform.GetComponent<PlayerInputForLevels>().GetPlayerColor().Equals(renderer.material.color))
            {
                Explode();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Border"))
        {
            Destroy(gameObject);
            gameManager.InformExplosion(GetColor(), true);
        }
    }

    private void SetMyColor(Color color)
    {
        renderer.material.color = color;
    }


    private void Explode()
    {
        GameObject explosion;
        if (GetColor().Equals(Constants.Blue))
        {
            explosion = Instantiate(blueExplosion, transform.position, Quaternion.identity);
        }
        else if (GetColor().Equals(Constants.Red))
        {
            explosion = Instantiate(redExplosion, transform.position, Quaternion.identity);
        }
        else if (GetColor().Equals(Constants.Yellow))
        {
            explosion = Instantiate(yellowExplosion, transform.position, Quaternion.identity);
        }
        else if (GetColor().Equals(Constants.Green))
        {
            explosion = Instantiate(greenExplosion, transform.position, Quaternion.identity);
        }
        else
        {
            explosion = Instantiate(blueExplosion, transform.position, Quaternion.identity);
        }
        explosion.transform.parent = explosions.transform;
        Destroy(gameObject);
        gameManager.InformExplosion(GetColor(), false);
        audioManager.PlayCubeDeath();
    }

    public Color GetColor()
    {
        return renderer.material.color;
    }
}
