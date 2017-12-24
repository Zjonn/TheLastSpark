using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform toFollow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
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
}
