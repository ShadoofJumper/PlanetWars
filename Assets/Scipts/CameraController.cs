using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 4f;
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 15f;
    private float currentScale;

    void Start()
    {
        currentScale = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        currentScale -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentScale = Mathf.Clamp(currentScale, minZoom, maxZoom);
        Camera.main.orthographicSize = currentScale;
    }
}

