using UnityEngine;
using UnityEngine.UI;

public class ScoreHolder : MonoBehaviour {

    int topScore;
    int currentScore;
    int pointsPerKill = 100;

    //To Display Current Score
    Text scoreText;

    //For Lose Screen Fade In
    GameObject loseScreen;
    //Text topScoreTextField;
    //Text currentScoreTextField;
    Image image;

    private void Awake()
    {
        currentScore = 0;
        scoreText = GameObject.Find("UI").GetComponentInChildren<Text>();
        //loseScreen = GameObject.Find("LoseScreen");
        //loseScreen.SetActive(false);
        image = GameObject.Find("UI").GetComponentInChildren<Image>();
        image.GetComponent<CanvasGroup>().alpha = 0f;

        try
        {
            //Get Top Score From Player Preferences
            topScore = PlayerPrefs.GetInt("TopScore", 0);
        }
        catch (System.Exception ex)
        {
            Debug.Log("Exception:" + ex);
        }
    }

    private void Start()
    {
        //Debug.Log("Top Score:" + topScore);
    }

    public void IncreasePoints()
    {
        currentScore += pointsPerKill;
        scoreText.text = "Score: " + currentScore.ToString();
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void EndGame()
    {

        Time.timeScale = 0;
        //Debug.Log("Game Over");
        //loseScreen.SetActive(true);

        /*topScoreTextField = loseScreen.GetComponentInChildren<Text>();
        topScoreTextField.text = "Top Score: " + topScore.ToString();

        currentScoreTextField = loseScreen.GetComponentInChildren<Text>();
        currentScoreTextField.text = "Top Score: " + currentScore.ToString();*/
        
        if (currentScore > topScore)
        {
            PlayerPrefs.SetInt("TopScore", currentScore);
            topScore = currentScore;
        }

        Text[] scoreTexts = image.GetComponentsInChildren<Text>();
        // ScoreTexts[0] --> Top Score
        // ScoreTexts[1] --> Top Score
        scoreTexts[0].text = "Top Score: " + topScore.ToString();
        scoreTexts[1].text = "Current Score: " + currentScore.ToString();
        image.GetComponent<CanvasGroup>().alpha = 1f;

    }

}
