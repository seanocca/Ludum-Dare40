using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public static CameraFollow Instance { get; set; }

    private GameObject player;

    public bool inPlay;

    void Awake()
    {
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
        player = GameObject.FindGameObjectWithTag("Player");

        inPlay = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            if (inPlay)
            {
				transform.position = new Vector3 (player.transform.position.x ,player.transform.position.y + 1.2f, player.transform.position.z - 1.4f );
            }
        }
    }

}
