using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject[] roadSections;

	public GameObject player; 

	public List<GameObject> spawnedRoadSections;

	public float lastPositionCheck = 1f;
	private float roadLength = 10f;

	[SerializeField]
	public Text alcohol_text;
	public Text pill_bottle_text;
	public Text pill_text;
	public Text capsule_blue_text;
	public Text capsule_red_text;
	public Text capsule_blue_red_text;


	// Use this for initialization
	void Start () {
		// Grab the road objects already in scene at start and store them in a list
		spawnedRoadSections.AddRange (GameObject.FindGameObjectsWithTag ("Road"));
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.z >= lastPositionCheck && player.transform.position.z <= lastPositionCheck + 1f) {
			SpawnRoad ();
			DestroyRoad ();
		}
	}

    private void PlayGame()
    {
        //Setup Player
            //GameObject player = Instantiate(playerPrefab, new Vector3(Maze.GetPlayerStartingX(), 0.5f, Maze.GetPlayerStartingZ()), Quaternion.identity);

        //Setup Camera and other game neccessities
            //gameCamera.transform.position = new Vector3();
            //menu.SetActive(false);
    }

	private void SpawnRoad() {
		// Get a random road section from the array of stored sections
		int rng = Random.Range (0, roadSections.Length);

		// Calculate the next position to spawn road at, based on the position we checked the player at plus the road length
		Vector3 nextPosition = new Vector3 (0,0, lastPositionCheck + (roadLength * 2) - 1);	

		// increase the position to check to the next position
		lastPositionCheck += roadLength;

		// Instantiate the random section
		GameObject road_section = Instantiate (roadSections [rng], nextPosition, Quaternion.identity);
		// Add to list of road_sections in the game scene
		spawnedRoadSections.Add (road_section);

		DestroyRoad ();
	}

	private void DestroyRoad() {
		// Check to make sure that more than 3 road section exits
		if (spawnedRoadSections.Count > 3) {
			// If so remove the oldest
			GameObject roadToDestroy = spawnedRoadSections[0];
			spawnedRoadSections.RemoveAt (0);
			Destroy (roadToDestroy);
		}
	}

}
