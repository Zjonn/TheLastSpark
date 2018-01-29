using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wlaserScript : MonoBehaviour, IWeapon
{
    public GameObject boss;
    public GameObject laser;
    public Transform spawnPointLaser;
    public float attackSpeed = 0.5f;


    float nextFire = 0;
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
        clone.GetComponent<IDamageAmount>().StartMovingBullet();
    }

    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + attackSpeed;
            FireLasert();
        }
    }

    public override string ToString()
    {
        return gameObject.tag;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}