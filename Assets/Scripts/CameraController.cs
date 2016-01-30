using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float OffsetX;

    private GameObject player;

    void Awake ()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void FixedUpdate() {

        transform.position = new Vector3(
            player.transform.position.x + OffsetX,
            transform.position.y,
            transform.position.z
        );
    }
}
