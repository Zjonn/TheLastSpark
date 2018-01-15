using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDamageHandler : MonoBehaviour
{
    SpriteRenderer spriteRend;
    Color firstColor;

    float curDamage = 0;
    float damage = 0;

    // Use this for initialization
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        firstColor = spriteRend.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (damage>0)
        {
            //Zmiana koloru przy otrzymaniu obrażeń
            spriteRend.color = Color.Lerp(spriteRend.color, Color.green, Mathf.PingPong(Time.time, 0.1f));

            //Zmiana wielkości
            Vector3 velocity = Vector3.zero;
            transform.localScale = Vector3.SmoothDamp(transform.localScale, new Vector3(0, 0, 0), ref velocity, 0.3f);
            damage -= curDamage * Time.deltaTime*5;
        }
        else
        {
            spriteRend.color = Color.Lerp(spriteRend.color, firstColor, Mathf.PingPong(Time.time, 0.2f));
        }
        if (transform.localScale.x < 0.5f)
        {
            Die();
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        damage += collision.GetComponent<IDamageAmount>().GetDamage();
        curDamage = damage;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
