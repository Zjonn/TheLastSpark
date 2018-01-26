using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public float renderDelay;
    public Transform toFollow;
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.enabled = false;
        StartCoroutine(CameraRender(1));
    }

    IEnumerator CameraRender(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(renderDelay);
            cam.Render();
        }
    }


    private void FixedUpdate()
    {
        Vector3 v = toFollow.position;
        v.z = transform.position.z;

        this.transform.position = v;
    }
}
