using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLaser : MonoBehaviour, IDamageAmount
{
    public float damage = 10;
    bool die = false;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        die = true;
    }

    public float GetDamage()
    {
        return damage;
    }
}
