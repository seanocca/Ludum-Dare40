using System.Collections;
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


    private Text alcohol_texts;
    private Text pill_bottle_texts;
    private Text pill_texts;
    private Text capsule_blue_red_texts;

    public int alcohol_count = 0;
    public int pill_bottle_count = 0;
    public int pill_count = 0;
    public int capsule_blue_red_count = 0;

    public int totalScore = 0;

	[SerializeField]
	private List<AudioClip> soundEffects;

	private AudioSource audioSource;

    public GameObject gameMan;
    public bool inGame;

    public GameObject restart;

	[SerializeField]
	private ParticleSystem gibs;

    void Awake()
    {
        //alcohol_texts = GameObject.Find("alcohol_text").GetComponent<Text>();
        pill_bottle_texts = GameManager.pill_bottle_text.GetComponent<Text>();
        pill_texts = GameManager.pill_text.GetComponent<Text>();
        capsule_blue_red_texts = GameManager.capsule_blue_red_text.GetComponent<Text>();
        alcohol_texts = GameManager.alcohol_text.GetComponent<Text>();
        blurr = GameObject.Find("Blur");
		audioSource = GetComponent<AudioSource> ();

        gameMan = GameObject.Find("GameManager");
        inGame = gameMan.GetComponent<GameManager>().inPlay;

        restart = GameObject.Find("Restart");
    }

    void Restart()
    {
        pharmAmount = 100f;
        playerSpeed = 3f;
        incSize = 0f;
        blurr.SetActive(false);
        rend = blurr.GetComponent<Renderer>();
        rend.material.SetFloat("_Size", incSize);
        alcohol_count = 0;
        alcohol_texts.text = "000";
        pill_bottle_count = 0;
        pill_bottle_texts.text = "000";
        pill_count = 0;
        pill_texts.text = "000";
        capsule_blue_red_count = 0;
        capsule_blue_red_texts.text = "000";
        inGame = true;
    }

    /// <summary>
    /// Updates text on screen with the pharmAmount
    /// Checks to see if the game is over if the pharmAmount has run out
    /// </summary>
    void FixedUpdate()
    {
        while (inGame == false)
        {
            inGame = gameMan.GetComponent<GameManager>().inPlay;
        }

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
            inGame = false;
            if (other.name == "jeep_blue" ||
			    other.name == "jeep_green" ||
			    other.name == "jeep_red") {

				audioSource.clip = soundEffects [2];
			} else {
				audioSource.clip = soundEffects [3];
			}

			audioSource.Play ();
			ParticleSystem death_gibs = Instantiate (gibs, new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z), Quaternion.identity);


            pharmAmount = 0f;
            playerSpeed = 0f;
            StopAllCoroutines();
            //restart.SetActive(true);
			this.gameObject.SetActive(false);
			gameMan.GetComponent<GameManager>().isDead = true;


            //Player showing death
            //Game Over
        }

        if (other.gameObject.tag == "SlowObstacle")
        {
            //Slow player down
			if (other.name == "Traffic_Cone") {
				audioSource.clip = soundEffects [6];
			} else if (other.name == "wheelie_bin") {
				audioSource.clip = soundEffects [7];
			}

			audioSource.Play ();

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
			audioSource.clip = soundEffects [4];
			audioSource.Play ();

			pill_count++;
            if (incSize > 0.25f)
            {
                incSize -= incPharm;
            }
			pill_texts.text = pill_count.ToString ();
            pharmAmount += pillPharm;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "PillBottle")
        {
            //Increase PharmAmount
			audioSource.clip = soundEffects [5];
			audioSource.Play ();

			pill_bottle_count++;
            if (incSize > 0.5f)
            {
                incSize -= pillPharm;
            }
			pill_bottle_texts.text = pill_bottle_count.ToString ();
            pharmAmount += pillBottlePharm;
            Destroy(other.gameObject);
        }        

        if (other.gameObject.tag == "Alcohol")
        {
            //Slowly Increase PharmAmount for three seconds
			audioSource.clip = soundEffects [0];
			audioSource.Play ();

			alcohol_count++;
            StartCoroutine("BlurrScreen");
            incSize += 0.25f;
            playerSpeed -= 0.2f;
			alcohol_texts.text = alcohol_count.ToString ();
            StartCoroutine("IncreasingPharm");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "CapsuleRedBlue")
        {
			audioSource.clip = soundEffects [1];
			audioSource.Play ();

            capsule_blue_red_count++;
            if (capsule_blue_red_count % 5 == 0)
            {
                StartCoroutine("BlurrScreen");
                incSize += 0.5f;
            }
            playerSpeed += 0.2f;
			capsule_blue_red_texts.text = capsule_blue_red_count.ToString ();
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