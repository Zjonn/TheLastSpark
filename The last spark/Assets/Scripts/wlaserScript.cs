using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wlaserScript : MonoBehaviour, IWeapon
{

    public GameObject laser;
    public Transform spawnPointLaser;

    private GameObject clone;
    Rigidbody2D player;

    // Use this for initialization
    void Start()
    {
        player = gameObject.GetComponentInParent(typeof(Rigidbody2D)) as Rigidbody2D;
    }


    public void FireLasert()
    {
        clone = Instantiate(laser, spawnPointLaser.position, spawnPointLaser.rotation) as GameObject;
        if (player != null)
            clone.GetComponent<Rigidbody2D>().velocity = player.velocity;
         
    }

    public void Fire()
    {
        FireLasert();
    }
}