using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public string sceneToStart;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene(sceneToStart);
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}
