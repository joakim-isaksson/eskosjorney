using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIFader : MonoBehaviour {

    public float fadeSpeed = 1.8f;

    private SpriteRenderer image;

    private bool fadingOut;

    void Awake()
    {
        image = GetComponent<SpriteRenderer>();
        image.enabled = true;
        fadingOut = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (fadingOut)
        {
            image.color = Color.Lerp(image.color, Color.clear, fadeSpeed * Time.deltaTime);
            if (image.color.a <= 0.05f)
            {
                image.color = Color.clear;
                image.enabled = false;
                fadingOut = false;
            }
        }
	}
}
