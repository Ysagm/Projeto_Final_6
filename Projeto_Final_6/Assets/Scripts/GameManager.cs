using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	//Pads
	public GameObject padPrefab;
	public GameObject superPadPrefab;
	//number of pads
	public int numPadsToMake = 10;
	int indexToCheck = 5;
	int indexToTanslate = 0;
	//calculates the width of the game level based on the screen width and the width of the padPrefab
	private float levelWidth;
	//spacing between the pads
	public float minVerticalDistance, maxVerticalDistance;
	// List that stores references to the created pad objects
	List<GameObject> pads = new List<GameObject>();
	//spawned pads
	Vector2 spawnPosition;
	bool superCharge = false;

	// Use this for initialization
	void Start () 
	{
		spawnPosition = transform.position;
		//screen dimensions
		levelWidth = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height)).x - padPrefab.GetComponent<MeshRenderer>().bounds.extents.x / 2f;
		//Creat pads at the start of the game
		makePads();
	}

	void Update()
	{
		//It checks if indexToCheck is less than the number of pads in the list
		if(indexToCheck < pads.Count)
		{
			//compares the script's Y position with the Y position of the pad
			if(transform.position.y >= pads[indexToCheck].transform.position.y)
			{
				translatePad(indexToTanslate);
			}
		}
	}

	//Loops to create a specified number of pads
	void makePads()
	{
		for(int i=0; i< numPadsToMake; i++)
		{
			makePad();
		}
	}

	void makePad()
	{
		//Calculates a new spawn position for the pad
		//determine where the new pad will be placed
		spawnPosition = new Vector2(0f,spawnPosition.y);
		//spawnPosition is modified by adding random values
		//Random.Range(-levelWidth, levelWidth) generates a random horizontal position within the range of -levelWidth to levelWidth (left-right placement of the pad)
		//Random.Range(minVerticalDistance, maxVerticalDistance) generates a random vertical position within the range of minVerticalDistance to maxVerticalDistance (up-down placement of the pad)
		spawnPosition += new Vector2(Random.Range(-levelWidth,levelWidth), Random.Range(minVerticalDistance,maxVerticalDistance));
		//temporary variable to store the newly created pad
		//ensure that it's always declared and has a value, even if the code inside the conditional blocks doesn't execute (debug)
		GameObject padTemp = null;

		//Checks super charge
		if(!superCharge)
		{
			//This has a 1 in 3 chance (33%) of being true
			if (Random.Range(0, 3) == 0)
			{
				//next pad created will be a "super" pad
				superCharge = true;
				padTemp = Instantiate(superPadPrefab, spawnPosition, Quaternion.identity);
			}
			else
			{
				padTemp = Instantiate(padPrefab, spawnPosition, Quaternion.identity);
			}
		}
		else{
			//In this case, it always creates a regular padPrefab at the calculated spawnPosition.
			padTemp = Instantiate(padPrefab, spawnPosition , Quaternion.identity);
		}

		Debug.Log(padTemp);
		//created pad (either regular or super)
		pads.Add(padTemp);
	}

	//Translate (move) a specific game pad identified by its index in the pads list
	void translatePad(int padIndex)
	{
		//sets the X-coordinate of the game pad's position to 0, keep Y-coordinate unchanged 
		//This centers the pad horizontally in the game world
		pads[padIndex].transform.position = new Vector2(0f,pads[padIndex].transform.position.y);
		//resets the spawnPosition
		spawnPosition = new Vector2(0f,spawnPosition.y);
		// calculates a new spawn position for the pad
		spawnPosition += new Vector2(Random.Range(-levelWidth,levelWidth), Random.Range(minVerticalDistance,maxVerticalDistance));
		//the game pad's position is updated to the newly calculated spawnPosition
		pads[padIndex].transform.position = spawnPosition;
		// gradually grow the size of the pad over time
		StartCoroutine(growPad(pads[padIndex]));

		//verifies if the indexToTanslate is less than the number of pads in the pads list minus one
		if (indexToTanslate < pads.Count - 1)
		{
			//If this condition is true, it means there are more pads in the list to translate.
			indexToTanslate++;
		}
		else
		{
			indexToTanslate = 0;
		}
		//calculates a value for indexToCheck to check if it's time to translate another pad
		//It adds 5 to indexToTanslate and takes the remainder when divided by (pads.Count - 1)
		indexToCheck = (indexToTanslate+5) % (pads.Count-1);
	}

	IEnumerator growPad(GameObject obj)
	{
		//Gradually scales up a pad over ten steps, creating a growth effect
		//iterates ten times, with i ranging from 0 to 10
		for(int i=0; i <=10; i++)
		{
			//k that represents the current step of the growth
			float k = (float) i /10;
			//sets the local scale of the pad uniformly in all three dimensions (X, Y, and Z)
			//As k increases from 0 to 1, the pad gradually grows in size.
			obj.transform.localScale = new Vector3(k,k,k);
			yield return new WaitForSeconds(0.01f);
		}
	}
	
}
