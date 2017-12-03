﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Vector3 currPlayerPosition;

    public static float pharmAmount = 100f;    

    public static float waitTime = 0.5f;
    public static float lostPharm = 1f;
    public static float incPharm = 0.5f;
    public static float pillBottlePharm = 2f;
    public static float pillPharm = 0.5f;

    public static bool losingPharm = false;
    public static bool increaseSpeed = false;
    public static bool increasePharm = false;

    public Text pharmaText;
    public Text speedText;

	public Text alcohol_text;
	public Text pill_bottle_text;
	public Text pill_text;
	public Text capsule_blue_text;
	public Text capsule_red_text;
	public Text capsule_blue_red_text;

	public int alcohol_count = 0;
	public int pill_bottle_count = 0;
	public int pill_count = 0;
	public int capsule_blue_count = 0;
	public int capsule_red_count = 0;
	public int capsule_blue_red_count = 0;

    /// <summary>
    /// Updates text on screen with the pharmAmount
    /// Checks to see if the game is over if the pharmAmount has run out
    /// </summary>
    void FixedUpdate()
    {       
        pharmaText.text = "Medic: " + pharmAmount;

        if (pharmAmount <= 0f)
        {
            //Game Over
        }
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
            PlayerMovement.playerSpeed = 0f;
            StopAllCoroutines();
            Time.timeScale = 0;

            //Player showing death
            //Game Over
        }
        else if (other.gameObject.tag == "SlowObstacle")
        {
            //Slow player down
            PlayerMovement.playerSpeed = 0.5f;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Pill")
        {
            //Increase PharmAmount
			pill_count++;
			pill_text.text = pill_count.ToString ();
            pharmAmount += pillPharm;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "PillBottle")
        {
            //Increase PharmAmount
			pill_bottle_count++;
			pill_bottle_text.text = pill_bottle_count.ToString ();
            pharmAmount += pillBottlePharm;
            Destroy(other.gameObject);
        }        

        if (other.gameObject.tag == "Alcohol")
        {
            //Slowly Increase PharmAmount for three seconds
			alcohol_count++;
			alcohol_text.text = alcohol_count.ToString ();
            StartCoroutine("IncreasingPharm");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "CapsuleBlue")
        {
            //Blurr Screen and reset to the best amount
			capsule_blue_count++;
			capsule_blue_text.text = capsule_blue_count.ToString ();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "CapsuleRed")
        {
            //Fisheye Screen and Increase or Decrease to the best amount
			capsule_red_count++;
			capsule_red_text.text = capsule_blue_count.ToString ();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "CapsuleRedBlue")
        {
            //Make Skybox go nuts
			capsule_blue_red_count++;
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
        while (x < 20)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            pharmAmount += incPharm;
            x++;
        }
        increasePharm = false;
        yield return null;
    }
}