using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
	public TextMeshProUGUI scoreText;
	private int currentScore;

	// Use this for initialization
	void Start () {
		EventManager.StartListening("ENEMY_DIED", ScorePoint);
		currentScore = 0;
		scoreText.text = currentScore.ToString();
	}
	
	public void ScorePoint()
	{
		currentScore++;
		scoreText.text = currentScore.ToString();
	}
	
}
