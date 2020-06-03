using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 4f;
    [SerializeField] private float moveSpeed = 1f;
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
        //zoom
        currentScale -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentScale = Mathf.Clamp(currentScale, minZoom, maxZoom);
        Camera.main.orthographicSize = currentScale;
        //move
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), transform.position.z);
        Vector3 newPos = transform.position + moveInput * Time.deltaTime * moveSpeed;
        newPos = Vector3.ClampMagnitude(newPos, 15.0f);
        newPos.z = transform.position.z;
        transform.position = newPos;
    }
}

