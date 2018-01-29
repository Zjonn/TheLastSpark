using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWeapon : MonoBehaviour, IWeapon
{
    public float movementSpeed = 100;
    public GameObject bulletPrefab;
    public Transform spawnPointLaser;
    public float attackSpeed = 0.5f;

    Rigidbody2D player;
    float nextFire = 0;
    //List<GameObject> bullets;

    // Use this for initialization
    void Start()
    {
        //bullets = new List<GameObject>();
        player = gameObject.GetComponentInParent(typeof(Rigidbody2D)) as Rigidbody2D;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireLasert()
    {
        GameObject clone = Instantiate(bulletPrefab, spawnPointLaser.position, Quaternion.Euler(0, 0, Random.Range(-3, 3)) * spawnPointLaser.rotation) as GameObject;
        if (player != null)
            clone.GetComponent<Rigidbody2D>().velocity = player.velocity;
        clone.GetComponent<IDamageAmount>().StartMovingBullet();
        Destroy(clone, Random.Range(5, 6));
        //bullets.Add(clone); planowane w przyszłości...
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
