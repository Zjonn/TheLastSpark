using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform toFollow;
    public float minSize = 2;
    public float maxSize = 5;

    Camera camera;

    // Use this for initialization
    void Start()
    {
        camera = GetComponent<Camera>();
        camera.orthographicSize = minSize;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && camera.orthographicSize < maxSize) // forward
        {
            camera.orthographicSize += 0.5f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f && camera.orthographicSize > minSize) // backwards
        {
            camera.orthographicSize -= 0.5f;
        }
    }

    private void FixedUpdate()
    {
        Vector3 v = toFollow.position;
        v.z = transform.position.z;

        //float dis = Vector3.Distance(v, transform.position);
        //float lerp = dis/16 * Time.deltaTime * 8;
        //if(dis > 20)
        //{
        //    lerp = 1;
        //}
        //else if(dis < 4)
        //{
        //    lerp = 0;
        //}
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
