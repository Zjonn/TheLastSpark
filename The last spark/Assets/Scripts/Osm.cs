using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Osm : MonoBehaviour
{
    public float Value
    {
        private set;
        get;
    }

    void Start()
    {
        Value = Random.Range(1, 4);
        float rnd = Random.value;
        if (rnd < 0.1)
        {
            Value *= 5;
        }
    }
}
