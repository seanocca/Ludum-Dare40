using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player {

    public static float playerSpeed = 2f;
    private float horizVel = 0;

    public KeyCode moveL;
    public KeyCode moveLL;
    public KeyCode moveR;
    public KeyCode moveRR;

    private int currLane = 3;

    private bool isMovingSideways = false;

	// Update is called once per frame
	void Update () {

        GetComponent<Rigidbody>().velocity = new Vector3(horizVel, 0, playerSpeed);

        if ((Input.GetKey(moveL) || Input.GetKey(moveLL)) && isMovingSideways == false && currLane > 1) 
        {
            Debug.Log("Player Moving Left");
            horizVel = -4;
            isMovingSideways = true;
            currLane--;
            StartCoroutine("PlayerMove");
        }

        if ((Input.GetKey(moveR) || Input.GetKey(moveRR))&& isMovingSideways == false && currLane < 5)
        {
            Debug.Log("Player Moving Right");
            horizVel = 4;
            isMovingSideways = true;
            currLane++;
            StartCoroutine("PlayerMove");
        }

        if (losingPharm == false && pharmAmount > 0f)
        {
            StartCoroutine("LosingPharm");
            StartCoroutine("IncreasingSpeed");
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
        yield return new WaitForSecondsRealtime(0.25f);
        horizVel = 0;
        isMovingSideways = false;
    }

    /// <summary>
    /// Increases or decreases speed of player depending on the pharmacuetical amount
    /// </summary>
    IEnumerator IncreasingSpeed()
    {
        increaseSpeed = true;
        while (pharmAmount > 0f)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            if (playerSpeed > 0.5f)
            {
                if (pharmAmount > 100)
                {
                    playerSpeed -= 0.08f;
                }
                else if (pharmAmount > 75)
                {
                    playerSpeed -= 0.04f;
                }
            }
            if (playerSpeed < 20)
            {
                if (pharmAmount > 50)
                {
                    playerSpeed += 0.04f;
                }
                else
                {
                    playerSpeed += 0.12f;
                }
            }
        }
        increaseSpeed = false;
        yield return null;
    }
}
