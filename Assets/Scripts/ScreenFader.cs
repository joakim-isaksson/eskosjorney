using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour {

    public float fadeSpeed = 1.8f;

    private SpriteRenderer spriteRenderer;

    private bool sceneStarting;
    private bool sceneEnding;
    private string sceneToStart;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        FitToScreen();

        spriteRenderer.color = Color.black;
        spriteRenderer.enabled = true;
        sceneStarting = true;
    }

    void Update()
    {
        if (sceneStarting)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.clear, fadeSpeed * Time.deltaTime);
            if (spriteRenderer.color.a <= 0.05f)
            {
                spriteRenderer.color = Color.clear;
                spriteRenderer.enabled = false;
                sceneStarting = false;
            }
        }

        if (sceneEnding)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.black, fadeSpeed * Time.deltaTime);
            if (spriteRenderer.color.a >= 0.95f)
            {
                spriteRenderer.color = Color.black;
                sceneStarting = false;

                SceneManager.LoadScene(sceneToStart);
            }
        }
    }

    public void EndScene(string sceneToStart)
    {
        this.sceneToStart = sceneToStart;
        spriteRenderer.color = Color.clear;
        spriteRenderer.enabled = true;
        sceneEnding = true;
    }

    private void FitToScreen()
    {
        transform.localScale = new Vector3(1, 1, 1);

        float width = spriteRenderer.sprite.bounds.size.x;
        float height = spriteRenderer.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize / 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(
            worldScreenWidth / width, worldScreenHeight / height, 1
        );
    }
}
