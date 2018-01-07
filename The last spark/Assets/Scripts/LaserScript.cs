using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {
    // Use this for initialization

    public float movementSpeed = 1;

    void Start () {
        Destroy(gameObject, Random.Range(5,6));
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
    }
}
