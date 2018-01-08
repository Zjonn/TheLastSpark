using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wlaserScript : MonoBehaviour
{

    public GameObject laser;
    public Transform spawnPointLaser;

    private GameObject clone;

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
        clone = Instantiate(laser, spawnPointLaser.position, spawnPointLaser.rotation) as GameObject;
    }
}