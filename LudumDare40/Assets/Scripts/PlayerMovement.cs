using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static Vector3 currPlayerPosition;

    public float pharmAmount = 100f;

    private bool isMoving = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine("Moving");
	}

    IEnumerator PlayerMove()
    {
        isMoving = true;
        while (pharmAmount <= 0f)
        {
            currPlayerPosition.z += 1f;
            this.transform.position.z = currPlayerPosition;
        }
        isMoving = false;
        yield return null;
    }
}
