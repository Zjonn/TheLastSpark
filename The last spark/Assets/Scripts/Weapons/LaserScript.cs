using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour, IDamageAmount
{
    // Use this for initialization

    public float movementSpeed = 10;
    public float damage = 10;
    bool die = false;
    bool isMoving = false;

    void Start() {
        Destroy(gameObject, Random.Range(5, 6));
    }

    void Update()
    {
        if (die)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
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
        isMoving = true;
    }
}
