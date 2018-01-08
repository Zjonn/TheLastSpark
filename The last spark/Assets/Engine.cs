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
            return maxThrust - rotationThrust;
        }
    }

    float thrust = 0;
    float rotationThrust = 0;

    private ParticleSystem particle;

    // Use this for initialization
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        rotationThrust = dropDown(rotationThrust, 0.05f * maxThrust);

        var emmision = particle.emission;
        float emmisionRate = thrust + rotationThrust * 10;
        if (emmisionRate > maxThrust) emmisionRate = maxThrust;
        emmision.rateOverTime = emmisionRate;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        thrust = 0;
    }

    public void SetThrust(float thrust)
    {
        this.thrust += thrust;
        if (this.thrust + rotationThrust > maxThrust) thrust = maxThrust - rotationThrust;
    }

    public void SetRotation(float thrust)
    {
        this.rotationThrust += thrust;
        if (this.rotationThrust > maxThrust) rotationThrust = maxThrust;
    }

    float dropDown(float val, float sub)
    {
        if (val > 0)
        {
            val = val - sub;
        }
        if (val < 0) val = 0;

        return val;
    }
}
