using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Osm : MonoBehaviour {

    public float Value
    {
        private set;
        get;
    }

	// Use this for initialization
	void Start () {
        Value = Random.Range(1, 4);
        float rnd = Random.value;
        if (rnd < 0.1)
        {
            Value *= 5; 
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
