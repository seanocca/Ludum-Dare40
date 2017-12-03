using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player {

    public static float playerSpeed = 3f;
    private float horizVel = 0;

    public KeyCode moveL;
    public KeyCode moveLL;
    public KeyCode moveR;
    public KeyCode moveRR;

    private int currLane = 3;

    private bool isMovingSideways = false;    

    // Update is called once per frame
    void Update () {

        speedText.text = "Speed: " + playerSpeed;

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
            if (playerSpeed < 20 && pharmAmount < 85)
            {
                if (pharmAmount > 50)
                {
                    playerSpeed += playerSpeed / (waitTime * pharmAmount);
                }
                else
                {
                    playerSpeed += 0.12f;
                }
            } else if (playerSpeed > 1f && pharmAmount > 85)
            {
                if (pharmAmount > 100)
                {
                    playerSpeed -= (waitTime / pharmAmount ) / playerSpeed;
                }
                else if (pharmAmount > 85)
                {
                    playerSpeed -= (waitTime / (pharmAmount * 2)) / playerSpeed;
                }
            }
        }
        increaseSpeed = false;
        yield return null;
    }
}
