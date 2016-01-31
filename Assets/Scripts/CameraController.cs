using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float rightOffset = 12f;
    public float leftOffset = -12f;
    public float upOffset = 9f;
    public float downOffset = 9f;

    public float leftEdge = -62f;
    public float rightEdge = 100f;
    public float topEdge = 40;
    public float bottomEdge = -1;

    private float cameraSpeedX = 4.0f;
    private float cameraSpeedY = 0.5f;

    private GameObject player;
    private PlayerController playerController;

    private float oldPlayerPosY;

    private float targetX;
    private float targetY;

    void Awake ()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerController = player.GetComponent<PlayerController>();
        targetX = transform.position.x;
        targetY = transform.position.y;
        oldPlayerPosY = player.transform.position.y;
    }

    void FixedUpdate() {

        float cameraX = transform.position.x;
        float cameraY = transform.position.y;
        float cameraZ = transform.position.z;

        float offsetX = cameraX - player.transform.position.x;
        float offsetY = cameraY - player.transform.position.y;

        if (playerController.facingRight && offsetX < rightOffset)
            targetX = player.transform.position.x + rightOffset;
        else if (offsetX > leftOffset)
            targetX = player.transform.position.x + leftOffset;

        if (oldPlayerPosY < player.transform.position.y && offsetY < upOffset)
            targetY = player.transform.position.y + upOffset;
        else if (oldPlayerPosY > player.transform.position.y && offsetY > downOffset)
            targetY = player.transform.position.y + downOffset;

        var vertExtent = Camera.main.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;
        float minX = leftEdge + (horzExtent / 2);
        float maxX = rightEdge - (horzExtent / 2);
        float minY = bottomEdge + (vertExtent / 2);
        float maxY = topEdge - (vertExtent / 2);

        float speedY = cameraSpeedY;
        if (oldPlayerPosY > player.transform.position.y && offsetY > downOffset)
        {
            speedY *= 20;
        }

        targetX = Mathf.Clamp(targetX, minX, maxX);
        targetY = Mathf.Clamp(targetY, minY, maxY);

        cameraX = Mathf.Lerp(cameraX, targetX, Time.deltaTime * cameraSpeedX);
        cameraY = Mathf.Lerp(cameraY, targetY, Time.deltaTime * speedY);

        transform.position = new Vector3(cameraX, cameraY, cameraZ);
        oldPlayerPosY = player.transform.position.y;
    }
}
