using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    DamageManagement dm;

    public int maxHealth = 2;
    int health;
    // Use this for initialization
    void Start()
    {
        //try
        //{
        dm = GetComponentInParent<DamageManagement>();
        //}
        //catch (Unity)
        //{

        //}
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            dm.DeadHandler(gameObject);
            health = maxHealth;
        }
    }

    void Damage(float val)
    {
        health -= (int)val;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damage(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WeaponLaser"))
            Damage(collision.gameObject.GetComponent<IDamageAmount>().GetDamage());
    }
}
