using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserScript : MonoBehaviour, IDamageAmount
{
    // Use this for initialization
    public float movementSpeed = 1;
    public float damage = 10;
    bool die = false;

    void Start()
    {
        Destroy(gameObject, 2);
    }

    void Update()
    {
        if (die)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Osm"))
        {
            die = true;
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }

    public void StartMovingBullet()
    {
        //napisać później
    }
}
