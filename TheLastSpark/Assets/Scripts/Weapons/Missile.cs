using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public float movementSpeed = 1;
    public float damage = 10;
    bool die = false;

    void Start()
    {
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
    void FixedUpdate()
    {
        transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        die = true;
    }

    public float GetDamage()
    {
        return damage;
    }
}
