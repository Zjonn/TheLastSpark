using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [Range(0, 100)]
    public float maxThrust = 10;

    public float AvailableThrust
    {
        get
        {
            return maxThrust - thrust;
        }
    }

    float thrust = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (thrust > 0)
            thrust -= 1;
    }

    public void SetThrust(float thrust)
    {
        thrust += thrust;
        if (thrust > maxThrust) thrust = maxThrust;
    }
}
