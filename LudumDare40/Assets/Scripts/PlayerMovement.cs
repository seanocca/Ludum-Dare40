using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player {

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

        GetComponent<Rigidbody>().velocity = new Vector3(horizVel, 0, playerSpeed);

        if ((Input.GetKeyDown(moveL) || Input.GetKeyDown(moveLL)) && currLane > 1) 
        {
            Debug.Log("Player Moving Left");

            Vector3 newPos = GetPlayerPosition();

            newPos.x = newPos.x - 1;

			this.transform.position = Vector3.MoveTowards(GetPlayerPosition(), newPos , 1f);

            currLane--;

        }

        if ((Input.GetKeyDown(moveR) || Input.GetKeyDown(moveRR)) && currLane < 5)
        {
            Debug.Log("Player Moving Right");

            Vector3 newPos = GetPlayerPosition();

            newPos.x = newPos.x + 1;

			this.transform.position = Vector3.MoveTowards(GetPlayerPosition(), newPos, 1f);

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
            if (playerSpeed > 1f && pharmAmount < 85f)
            {
                if (pharmAmount > 50f)
                {
                    playerSpeed -= (waitTime / pharmAmount) / playerSpeed;
                }
                else
                {
                    playerSpeed -= (waitTime / (pharmAmount * 2)) / playerSpeed;
                }
            } else if (playerSpeed < 20f && pharmAmount > 85f)
            {
                if (pharmAmount > 85f)
                {
                    playerSpeed += 0.04f;
                }
                else if (pharmAmount > 100f)
                {
                    playerSpeed += 0.12f;
                }
            }
        }
        increaseSpeed = false;
        yield return null;
    }
}
