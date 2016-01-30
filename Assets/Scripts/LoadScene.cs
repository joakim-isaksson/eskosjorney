using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public string sceneToLoad;

    private GameObject player;

    private ScreenFader screenFader;

    void Awake()
    {
        screenFader = GameObject.FindGameObjectsWithTag("Fader")[0].GetComponent<ScreenFader>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            screenFader.EndScene(sceneToLoad);
        }
    }
}
