using UnityEngine;

public class CubeScript : MonoBehaviour
{

    [HideInInspector] [SerializeField] new Renderer renderer;
    Color colorValue;

    [SerializeField] GameObject blueExplosion;
	[SerializeField] GameObject redExplosion;
	[SerializeField] GameObject yellowExplosion;
	[SerializeField] GameObject greenExplosion;

    AudioManager audioManager;


    ScoreHolder scoreHolder; 

    Vector3 newCubeRotateAmount;

	GameObject explosions;

	private void Start()
	{
		renderer = GetComponent<Renderer>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

		explosions = GameObject.Find("Explosions");

		newCubeRotateAmount = new Vector3(Random.Range(0f, 90f), Random.Range(0f, 90f));

		SetMyColor();

        scoreHolder = GameObject.Find("ScoreHolder").GetComponent<ScoreHolder>();
	}

	private void Update()
	{
		transform.Rotate(newCubeRotateAmount * Time.deltaTime);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
            if (collision.transform.GetComponent<FollowMouse>().GetPlayerColor().Equals(renderer.material.color))
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
        }
    }

    private void SetMyColor()
	{
		int Number = Random.Range(0, 4);
        //Debug.Log("Number: " + Number);
		switch (Number)
		{
			case 0:
				colorValue = Constants.Blue;
				break;
			case 1:
				colorValue = Constants.Red;
				break;
			case 2:
				colorValue = Constants.Yellow;
				break;
			case 3:
                colorValue = Constants.Green;
				break;
			default:
                colorValue = Constants.Blue;
				break;
		}

		renderer.material.color = colorValue;
	}


	private void Explode()
	{
		GameObject explosion;
		if (colorValue.Equals(Constants.Blue))
		{
			explosion = Instantiate(blueExplosion, transform.position, Quaternion.identity);
		}
		else if (colorValue.Equals(Constants.Red))
		{
			explosion = Instantiate(redExplosion, transform.position, Quaternion.identity);
		}
		else if (colorValue.Equals(Constants.Yellow))
		{
			explosion = Instantiate(yellowExplosion, transform.position, Quaternion.identity);
		}
		else if (colorValue.Equals(Constants.Green))
		{
			explosion = Instantiate(greenExplosion, transform.position, Quaternion.identity);
		}
		else
		{
			explosion = Instantiate(blueExplosion, transform.position, Quaternion.identity);
		}
		explosion.transform.parent = explosions.transform;
		Destroy(gameObject);
        scoreHolder.IncreasePoints();
        audioManager.PlayCubeDeath();
    }

    public Color GetColor()
    {
        return renderer.material.color;
    }
}
