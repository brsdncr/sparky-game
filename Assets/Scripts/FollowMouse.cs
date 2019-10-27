using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowMouse : MonoBehaviour
{
    [HideInInspector] [SerializeField] new Renderer renderer;
    private float speed = 8.0f;
	private float distanceFromCamera = 10.0f;

    AudioManager audioManager;

    Color color;
	//GameObject sc;

    ScoreHolder scoreHolder;

    private void Awake()
	{
        Time.timeScale = 1;
	}

	private void Start()
	{
        color = Color.white;
        renderer = GetComponent<Renderer>();

        scoreHolder = GameObject.Find("ScoreHolder").GetComponent<ScoreHolder>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
	void Update()
	{
		CheckMouseMovement();
		//CheckTouches();
	}

	private void CheckTouches()
	{
		if (Input.touchCount > 1)
		{
			Touch touch = Input.GetTouch(0);

			Vector3 touchInworldPoints = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, distanceFromCamera));

			Vector3 position = Vector3.Lerp(transform.position, touchInworldPoints, 1.0f - Mathf.Exp(-speed * Time.deltaTime));
			transform.position = position;
		}
	}

	private void CheckMouseMovement()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = distanceFromCamera;

		Vector3 mouseScreenToWorld = Camera.main.ScreenToWorldPoint(mousePosition);

		Vector3 position = Vector3.Lerp(transform.position, mouseScreenToWorld, 1.0f - Mathf.Exp(-speed * Time.deltaTime));
		transform.position = position;
	}

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("YellowChanger"))
		{
			 color = Constants.Yellow;
		}
		else if (other.transform.CompareTag("GreenChanger"))
		{
			 color = Constants.Green;
		}
		else if (other.transform.CompareTag("BlueChanger"))
		{
			 color = Constants.Blue;
		}
		else if (other.transform.CompareTag("RedChanger"))
		{
			 color = Constants.Red;
		}
        renderer.material.color = color;

        ColorChanger.Change(color);


	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.CompareTag("Cube"))
		{
			if (!collision.transform.GetComponent<CubeScript>().GetColor().Equals(renderer.material.color))
			{
                scoreHolder.EndGame();
                audioManager.PlayPlayerDeath();
				//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

			}
		}
	}

	public Color GetPlayerColor()
	{
		return renderer.material.color;
	}
}
