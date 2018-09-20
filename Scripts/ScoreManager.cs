using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    public Text highScoreText;

    public float scoreCount;
    public float highScoreCount;

    public float pointsPerSecond;

    public bool scoreIncreasing;

	// Use this for initialization
	void Start () {
	if (PlayerPrefs.HasKey("HighScore"))
    {
        //za reset highscorea
        //highScoreCount = 0;
		//highScoreText.text = 0;
        
        highScoreCount = PlayerPrefs.GetFloat("HighScore");
    }
	}
	
	// Update is called once per frame
	void Update () 
    {

        if(scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }
        
        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }
        
        scoreText.text = "SCORE: " + Mathf.Round(scoreCount);
        highScoreText.text = "HIGH SCORE: " + Mathf.Round(highScoreCount);
				
	}

    public void AddScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
	}
        
		

}


