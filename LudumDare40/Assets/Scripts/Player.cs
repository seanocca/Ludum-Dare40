﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Vector3 currPlayerPosition;

    public static float pharmAmount = 100f;
    public static float playerSpeed = 1f;

    public static float waitTime = 0.5f;
    public static float lostPharm = 1f;
    public static float incPharm = 0.5f;
    public static float pillBottlePharm = 2f;
    public static float pillPharm = 0.5f;

    public static bool losingPharm = false;
    public static bool increaseSpeed = false;
    public static bool increasePharm = false;

    public Text pharmaText;

    void FixedUpdate()
    {
        if (losingPharm == false && pharmAmount > 0f)
        {
            StartCoroutine("LosingPharm");
            StartCoroutine("IncreasingSpeed");
        }

        pharmaText.text = "Medic: " + pharmAmount.ToString();

    }

    /// <summary>
    /// Has the Player hit an obstacle.
    /// It Either Kills her or slows her down.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathObstacle")
        {
            pharmAmount = 0f;
            //Topple over player showing death
            //End Current Game
        }
        else if (other.gameObject.tag == "SlowObstacle")
        {
            //Slow player down
            playerSpeed = 0.5f;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Pill")
        {
            //Increase PharmAmount
            pharmAmount += pillPharm;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "PillBottle")
        {
            //Increase PharmAmount
            pharmAmount += pillBottlePharm;
            Destroy(other.gameObject);
        }        

        if (other.gameObject.tag == "Alcohol")
        {
            //Slowly Increase PharmAmount for three seconds
            StartCoroutine("IncreasePharm");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "CapsuleBlue")
        {
            //Blurr Screen and reset to the best amount
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "CapsuleRed")
        {
            //Fisheye Screen and Increase or Decrease to the best amount
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "CapsuleRedBlue")
        {
            //Make Skybox go nuts
            Destroy(other.gameObject);
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return this.transform.position;
    }

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

    IEnumerator IncreasingSpeed()
    {
        increaseSpeed = true;
        while (pharmAmount > 0f)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            if (pharmAmount > 50)
            {
                playerSpeed += 0.08f;
            }
            else
            {
                playerSpeed += 0.16f;
            }
        }
        increaseSpeed = false;
        yield return null;
    }

    IEnumerator IncreasingPharm()
    {
        int x = 0;
        increasePharm = true;
        while (x < 80)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            pharmAmount += incPharm;
            x++;
        }
        increasePharm = false;
        yield return null;
    }
}