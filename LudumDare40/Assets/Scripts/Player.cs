﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Vector3 currPlayerPosition;

    public GameObject blurr;
    public Shader shaderBlur;
    public static Renderer rend;
    private float incSize = 0f;

    public static float pharmAmount = 100f;
    public static float playerSpeed = 3f;

    public static float waitTime = 0.5f;
    public static float lostPharm = 1f;
    public static float incPharm = 0.25f;
    public static float pillBottlePharm = 2f;
    public static float pillPharm = 0.5f;

    public static bool losingPharm = false;
    public static bool increaseSpeed = false;
    public static bool increasePharm = false;



	public Text alcohol_text;
	public Text pill_bottle_text;
	public Text pill_text;
	public Text capsule_blue_red_text;

	public int alcohol_count = 0;
	public int pill_bottle_count = 0;
	public int pill_count = 0;
	public int capsule_blue_red_count = 0;

    public int totalScore = 0;

	void Awake() {

	}

    void Start()
    {
		alcohol_text = GameObject.Find("alcohol_text").GetComponent<Text>();
		pill_bottle_text = GameObject.Find ("pill_bottle_text").GetComponent<Text>();
		pill_text = GameObject.Find ("pill_text").GetComponent<Text>();
		capsule_blue_red_text = GameObject.Find ("capsule_blue_red_text").GetComponent<Text>();

		blurr = GameObject.Find("Blur");
    }

    /// <summary>
    /// Updates text on screen with the pharmAmount
    /// Checks to see if the game is over if the pharmAmount has run out
    /// </summary>
    void FixedUpdate()
    {       

        if (pharmAmount <= 0f)
        {
            //Game Over
        }

        totalScore = (int)Mathf.Round((alcohol_count / 2) + (pill_bottle_count / 4) + (pill_count) + (capsule_blue_red_count / 6));
    }

    /// <summary>
    /// Has the Player hit an obstacle or artefact.
    /// It Either Kills her or slows her down.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathObstacle")
        {
            pharmAmount = 0f;
            playerSpeed = 0f;
            StopAllCoroutines();
            Time.timeScale = 0;

            //Player showing death
            //Game Over
        }

        if (other.gameObject.tag == "SlowObstacle")
        {
            //Slow player down
            if (playerSpeed > 1.5f)
            {
                playerSpeed -= 1f;
            } else
            {
                playerSpeed = 0.75f;
            }
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Pill")
        {
            //Increase PharmAmount
			pill_count++;
            if (incSize > 0.25f)
            {
                incSize -= 0.25f;
            }
			pill_text.text = pill_count.ToString ();
            pharmAmount += pillPharm;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "PillBottle")
        {
            //Increase PharmAmount
			pill_bottle_count++;
            if (incSize > 0.5f)
            {
                incSize -= 0.5f;
            }
			pill_bottle_text.text = pill_bottle_count.ToString ();
            pharmAmount += pillBottlePharm;
            Destroy(other.gameObject);
        }        

        if (other.gameObject.tag == "Alcohol")
        {
            //Slowly Increase PharmAmount for three seconds
			alcohol_count++;
            StartCoroutine("BlurrScreen");
            incSize += 0.25f;
            playerSpeed -= 0.2f;
			alcohol_text.text = alcohol_count.ToString ();
            StartCoroutine("IncreasingPharm");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "CapsuleRedBlue")
        {
            capsule_blue_red_count++;
            if (capsule_blue_red_count % 5 == 0)
            {
                StartCoroutine("BlurrScreen");
                incSize += 0.5f;
            }
            playerSpeed += 0.2f;
			capsule_blue_red_text.text = capsule_blue_red_count.ToString ();
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// Gets this player position
    /// </summary>
    /// <returns> Returns player position</returns>
    public Vector3 GetPlayerPosition()
    {
        return this.transform.position;
    }

    /// <summary>
    /// Decreases the pharmaceutical amount the the mum has
    /// </summary>
    IEnumerator LosingPharm()
    {
        losingPharm = true;
        while (pharmAmount > 0f)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            pharmAmount -= lostPharm;
        }
        losingPharm = false;
        yield return null;
    }

    /// <summary>
    /// Increases the pharma amount to
    /// stop the effects of losing pharma
    /// for 10 seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator IncreasingPharm()
    {
        int x = 0;
        increasePharm = true;
        while (x < 10)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            pharmAmount += incPharm;
            x++;
        }
        increasePharm = false;
        yield return null;
    }

    IEnumerator BlurrScreen()
    {
        if (blurr.activeSelf == false)
        {
            blurr.SetActive(true);
        }
        rend = blurr.GetComponent<Renderer>();
        rend.material.SetFloat("_Size", incSize);
        yield return null;
        
    }
}