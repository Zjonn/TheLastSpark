using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLaser : MonoBehaviour, IDamageAmount
{
    public float movementSpeed = 10;
    public float damage = 10;
    bool die = false;
    bool isMoving = false;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (die)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Osm"))
        {
            die = true;
        }
    }

    public float GetDamage()
    {
        return damage;
    }

    public void StartMovingBullet()
    {
        isMoving = true;
    }
}
