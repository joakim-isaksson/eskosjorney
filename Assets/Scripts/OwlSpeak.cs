using UnityEngine;
using System.Collections;

public class OwlSpeak : MonoBehaviour {

    private SpriteRenderer bubble;
    private SpriteRenderer text;

    private AudioSource audioSource;

    public AudioClip speakSound;

    private bool triggeredOnce = false; 

    void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        bubble = GameObject.FindGameObjectWithTag("owl_bubble").GetComponent<SpriteRenderer>();
        text = GameObject.FindGameObjectWithTag("owl_text").GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggeredOnce && other.tag.Equals("Player"))
        {
            audioSource.PlayOneShot(speakSound);
            triggeredOnce = true;
            bubble.enabled = true;
            text.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            bubble.enabled = false;
            text.enabled = false;
        }
    }

}
