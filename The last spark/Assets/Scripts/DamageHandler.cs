using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    DamageManagement dm;

    public int health = 2;
    int maxHealth;
    // Use this for initialization
    void Start()
    {
        dm = GetComponentInParent<DamageManagement>();
        maxHealth = health;
    }

    void Damage(float val)
    {
        health -= (int)val;
        if (health <= 0)
        {
            dm.DeadHandler(gameObject);
            health = maxHealth;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Osm"))
            Damage(1);
    }

}
