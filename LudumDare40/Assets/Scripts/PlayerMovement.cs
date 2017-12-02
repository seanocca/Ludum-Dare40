using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static Vector3 currPlayerPosition;

    public float pharmAmount = 100f;
    private float playerSpeed = 0.5f;

    private int speedLoop;

    private bool isMoving = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (isMoving == false)
        {
            Debug.Log("Player Coroutine ON");
            StartCoroutine("PlayerMove");
            //StartCoroutine("HitDetection");
        }else if (isMoving == false && pharmAmount <= 0f)
        {
            StopCoroutine("PlayerMove");
        }
	}

    IEnumerator PlayerMove()
    {
        isMoving = true;
        while (pharmAmount > 0f)
        {
            Debug.Log("Player is Moving");
            currPlayerPosition = this.transform.position;
            if (playerSpeed == 0.25f)
            {
                speedLoop++;
                if (speedLoop == 3)
                {
                    playerSpeed = 0.5f;
                }
            }
            currPlayerPosition.z += playerSpeed;
            this.transform.position = currPlayerPosition;
            yield return new WaitForSecondsRealtime(0.5f);
        }
        isMoving = false;
        yield return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathObstacle")
        {
            StopCoroutine("Moving");
        }else if (other.gameObject.tag == "SlowObstacle")
        {
            //Slow player down
            playerSpeed = 0.25f;
        }
    }

}
