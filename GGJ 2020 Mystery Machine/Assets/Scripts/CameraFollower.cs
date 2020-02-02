using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform player;
    private Camera _camera;
    public float margin = 1f; // If the player stays inside this margin, the camera won't move.
    public float smoothing = 3f; // The bigger the value, the faster is the camera.
 
    public BoxCollider2D cameraBoundsUpstairs;
    public BoxCollider2D cameraBoundsCellar;

    private BoxCollider2D currentCameraBounds;
 
    private Vector3 min, max;

    private bool _enabled = true;

 
 
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        currentCameraBounds = cameraBoundsUpstairs;
    }
 
    void Start()
    {
        min = currentCameraBounds.bounds.min;
        max = currentCameraBounds.bounds.max;
        _camera = GetComponent<Camera>();

    }
 
    void Update()
    {
        if (!_enabled) return;
        var x = transform.position.x;
 
        if (Mathf.Abs(x - player.position.x) > margin)
            x = Mathf.Lerp(x, player.position.x, smoothing * Time.deltaTime);
 
        // ortographicSize is the haldf of the height of the Camera.
        float cameraHalfWidth = _camera.orthographicSize * ((float)Screen.width / Screen.height);
 
        x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
 
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
 
    // PixelPerfectScript.
    public static float RoundToNearestPixel(float unityUnits, Camera viewingCamera)
    {
        float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2)) * unityUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2));
        return adjustedUnityUnits;
    }
 
    void LateUpdate()
    {
        if (!_enabled) return;
        Vector3 newPos = transform.position;
        Vector3 roundPos = new Vector3(RoundToNearestPixel(newPos.x, _camera), RoundToNearestPixel(newPos.y, _camera), newPos.z);
        transform.position = roundPos;
    }
 
    public void SetBounds(bool inCellar) {
        _enabled = false;
        if (inCellar) {
            currentCameraBounds = cameraBoundsCellar;
        } else {
            currentCameraBounds = cameraBoundsUpstairs;
        }
        transform.position = new Vector3(Player.Instance.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        Invoke("EnableFollow()", 0.5f);
    }

    private void EnableFollow() {
        _enabled = true;
    }
}
