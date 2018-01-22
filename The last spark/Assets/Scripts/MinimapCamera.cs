using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour {


    public Transform toFollow;


    private void FixedUpdate()
    {
        Vector3 v = toFollow.position;
        v.z = transform.position.z;

        this.transform.position = v;
    }
}
