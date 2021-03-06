﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {

	public GameObject[] roadSections;

	public GameObject player; 

	public GameObject camera;

	public List<GameObject> spawnedRoadSections;

	public float lastPositionCheck = 1f;
	private float roadLength = 10f;

	public bool inPlay = false;
	public bool isDead = false;

	public GameObject playerStart;

	public GameObject restart_button;

	public static GameObject title;
    public static GameObject enter;
    public static GameObject credits;
    public static GameObject alcohol;
    public static GameObject alcohol_text;
    public static GameObject pill_bottle;
    public static GameObject pill_bottle_text;
    public static GameObject pill;
    public static GameObject pill_text;
    public static GameObject capsule_blue_red;
    public static GameObject capsule_blue_red_text;

	void Awake() {
		if (camera == null) {
			GameObject.Find ("Main Camera");
		}

		title = GameObject.Find ("Title");
		enter = GameObject.Find ("Enter");
		credits = GameObject.Find ("Credits");
		alcohol = GameObject.Find ("Alcohol");
		alcohol_text = GameObject.Find ("alcohol_text");
		pill_bottle = GameObject.Find ("Pill_Bottle");
		pill_bottle_text = GameObject.Find ("pill_bottle_text");
		pill = GameObject.Find ("pill");
		pill_text = GameObject.Find ("pill_text");
		capsule_blue_red = GameObject.Find ("capsule_blue_red");
		capsule_blue_red_text = GameObject.Find ("capsule_blue_red_text");
		restart_button = GameObject.Find("Restart_Button");
    }

	// Use this for initialization
	void Start () {
		spawnedRoadSections.AddRange(GameObject.FindGameObjectsWithTag("Road").OrderBy(go => go.name).ToList());

		inPlay = false;
		isDead = false;
		alcohol.SetActive(false);
		alcohol_text.SetActive(false);
		pill_bottle.SetActive(false);
		pill_bottle_text.SetActive(false);
		pill.SetActive(false);
		pill_text.SetActive(false);
		capsule_blue_red.SetActive(false);
		capsule_blue_red_text.SetActive(false);
		restart_button.SetActive(false);	
	}

	private static int SortByName(GameObject o1, GameObject o2) {
		return o1.name.CompareTo(o2.name);
	}

	// Update is called once per frame
	void Update () {
		if (inPlay)
        {
			if (player.transform.position.z >= lastPositionCheck && player.transform.position.z <= lastPositionCheck + 1f) {
				SpawnRoad ();
				DestroyRoad ();
			}
		} else
        {
			if (Input.GetKeyDown ("return")) {
				Debug.Log ("Enter");
				Destroy (playerStart);
				GameObject gameplayer = Instantiate (player, new Vector3 (0.0f, 0.2f, 1.0f), Quaternion.identity);
				player = gameplayer;
				camera.GetComponent<CameraFollow> ().inPlay = true;
				title.SetActive(false);
				enter.SetActive(false);
				credits.SetActive(false);
				alcohol.SetActive(true);
				alcohol_text.SetActive(true);
				pill_bottle.SetActive(true);
				pill_bottle_text.SetActive(true);
				pill.SetActive(true);
				pill_text.SetActive(true);
				capsule_blue_red.SetActive(true);
				capsule_blue_red_text.SetActive(true);
                inPlay = true;
            }
		}
		if (isDead) {
			restart_button.SetActive (true);
			if (Input.GetKeyDown("return"))
			{
				//GameStart();
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Player.playerSpeed = 3f;
			}
		}
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
