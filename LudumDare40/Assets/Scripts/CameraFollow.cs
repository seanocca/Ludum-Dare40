using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public static CameraFollow Instance { get; set; }

	[SerializeField]
    private GameObject player;

	private GameObject gameManager;

    public bool inPlay;

    void Awake()
    {
		gameManager = GameObject.Find ("GameManager");
		this.inPlay = gameManager.GetComponent<GameManager> ().inPlay;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (inPlay)
        {
			transform.position = new Vector3 (0f ,player.transform.position.y + 2.4f, player.transform.position.z - 1.25f );
        }

    }

}
