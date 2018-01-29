using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    DamageManagement dm;

    public int health = 10;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer < 12)
            Damage(collision.GetComponent<IDamageAmount>().GetDamage());
    }

    private void OnValidate()
    {
        if (health < 0)
        {
            health = 0;
        }
    }
}
