using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player {

    private float horizVel = 0;

    public KeyCode moveL;
    public KeyCode moveR;

    private int currLane = 3;

    private bool isMovingSideways = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<Rigidbody>().velocity = new Vector3(horizVel, 0, playerSpeed);

        if (Input.GetKeyDown(moveL) && isMovingSideways == false && currLane > 1) 
        {
            Debug.Log("Player Moving Left");
            horizVel = -2;
            isMovingSideways = true;
            currLane--;
            StartCoroutine("PlayerMove");
        }

        if (Input.GetKeyDown(moveR) && isMovingSideways == false && currLane < 5)
        {
            Debug.Log("Player Moving Right");
            horizVel = 2;
            isMovingSideways = true;
            currLane++;
            StartCoroutine("PlayerMove");
        }

	}

    /// <summary>
    /// Moves player forward.
    /// Gets players current Vector 3
    /// Checks if the player hit something to slow her down.
    /// Then the player is moved forward
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerMove()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        horizVel = 0;
        isMovingSideways = false;
    }

 }
