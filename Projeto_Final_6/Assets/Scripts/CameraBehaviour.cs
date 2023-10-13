using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraBehaviour : MonoBehaviour 
{
	//Score
	public Text scoreText;
	public Text loseText;
	public float score = 0f;
	//Modal lose
	public GameObject losePanel; 
	//Target -> character
	public Transform target;
	//velocidade da camera
	public float cameraSpeed = 5f;
	bool lostGame = false;

	
	//LateUpdate is called after all Update functions have been called
	void LateUpdate() 
	{
		if(!lostGame) //check if player lost the game
		{
			// This checks if the Y-position of an object referred to as "target" is greater than the Y-position of the object that this script is attached to
			if(target.transform.position.y > transform.position.y)
			{
				//gradually moves the position of the object towards a new position, specifically changing only the Y-coordinate to match the Y-coordinate of the "target" object
				transform.position = Vector3.Lerp(transform.position, 
					new Vector3(transform.position.x,target.transform.position.y,transform.position.z), cameraSpeed * Time.deltaTime);
				//Score is updated based on how much time has passed since the last frame. 
				//The score increases over time, with a rate of 15 points per second
				score += Time.deltaTime * 15f;

				scoreText.text = "Score: "+ (int) score;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		// checks if the collided object (other) has a tag "Player"
		if(other.gameObject.tag.Equals("Player"))
		{
			lostGame = true;
			//checks if the current score is greater than the best score stored in the player's preferences
			if (score > PlayerPrefs.GetInt("bestScore"))
			{
				//sets the "bestScore" in the player's preferences
				PlayerPrefs.SetInt("bestScore", (int)score);
			}
			//display game over screen
			loseGame();
		}
	}

	//Lose Game
	void loseGame()
	{
		//Show lose panel
		losePanel.SetActive(true);
		loseText.text = "Game over!\n Score: "+(int) score+ "\nBest score: "+PlayerPrefs.GetInt("bestScore");
		Destroy(target.gameObject);
	}

	//Quit
	public void quit()
	{
		Application.Quit();
	}

	//Restart
	public void startAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
