using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{

    public Transform toFollow;
    public float minSize = 2;
    public float maxSize = 5;

    Camera playerCamera;


    // Use this for initialization
    void Start()
    {
        playerCamera = GetComponent<Camera>();
        playerCamera.orthographicSize = (minSize + maxSize) / 2;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && playerCamera.orthographicSize < maxSize) // forward
        {
            playerCamera.orthographicSize += 0.5f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f && playerCamera.orthographicSize > minSize) // backwards
        {
            playerCamera.orthographicSize -= 0.5f;
        }
    }

    private void FixedUpdate()
    {
        Vector3 v = toFollow.position;
        v.z = transform.position.z;

        this.transform.position = v;
    }

    private void OnValidate()
    {
        if (minSize > maxSize)
        {
            float tmp = maxSize;
            maxSize = minSize;
            minSize = tmp;
        }
    }

    private void Reset()
    {
        minSize = 2;
        maxSize = 5;
    }
}
