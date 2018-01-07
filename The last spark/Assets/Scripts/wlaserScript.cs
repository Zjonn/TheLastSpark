using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wlaserScript : MonoBehaviour
{

    public Object laser;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown ("space"))
        {
            FireLasert();
        }
    }

    void FireLasert()
    {
        Instantiate(laser, transform.position + transform.up, transform.rotation);
    }
}