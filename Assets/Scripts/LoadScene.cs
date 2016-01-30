using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public string sceneToLoad;

    public bool isDoor = false;

    private PlayerController playerController;

    private ScreenFader screenFader;

    private AudioSource audioSource;

    public AudioClip doorSound;

    private bool insideArea = false;

    void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        screenFader = GameObject.FindGameObjectsWithTag("Fader")[0].GetComponent<ScreenFader>();
        playerController = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isDoor && insideArea && playerController.grounded && Input.GetButtonDown("Jump"))
        {
            playerController.freeze = true;
            if (isDoor) audioSource.PlayOneShot(doorSound);
            isDoor = false;
            screenFader.EndScene(sceneToLoad);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        insideArea = true;
        if (!isDoor && other.tag.Equals("Player"))
        {
            playerController.freeze = true;
            screenFader.EndScene(sceneToLoad);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        insideArea = false;
    }
}
