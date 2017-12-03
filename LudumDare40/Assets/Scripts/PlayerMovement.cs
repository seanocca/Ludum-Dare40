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
  

	private Animator animator;

	void Start() {
		animator = GetComponent<Animator> ();
	}

    // Update is called once per frame
    void Update () {

        speedText.text = "Speed: " + playerSpeed;

        GetComponent<Rigidbody>().velocity = new Vector3(horizVel, 0, playerSpeed);

        if ((Input.GetKeyDown(moveL) || Input.GetKeyDown(moveLL)) && currLane > 1) 
        {
            Debug.Log("Player Moving Left");

            Vector3 newPos = GetPlayerPosition();

            newPos.x = newPos.x - 1;

            this.transform.position = Vector3.Slerp(GetPlayerPosition(), newPos ,1f);

            currLane--;

        }

        if ((Input.GetKeyDown(moveR) || Input.GetKeyDown(moveRR))&& currLane < 5)
        {
            Debug.Log("Player Moving Right");

            Vector3 newPos = GetPlayerPosition();

            newPos.x = newPos.x + 1;

            this.transform.position = Vector3.Slerp(GetPlayerPosition(), newPos,1f);

            currLane++;
        }

        if (losingPharm == false && pharmAmount > 0f)
        {
            StartCoroutine("LosingPharm");
            StartCoroutine("IncreasingSpeed");
        }

		animator.SetFloat ("playerSpeed", playerSpeed);

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
