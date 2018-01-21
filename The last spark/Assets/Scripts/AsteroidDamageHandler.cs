using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDamageHandler : MonoBehaviour
{
    SpriteRenderer spriteRend;
    Color firstColor;

    float curDamage = 0;
    float damage = 0;
    Color c;

    // Use this for initialization
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.color = Color.HSVToRGB(Random.value, 1, 1);
        firstColor = spriteRend.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (damage > 0)
        {
            //Zmiana koloru przy otrzymaniu obrażeń
            spriteRend.color = Color.Lerp(spriteRend.color, c, Mathf.PingPong(Time.time, 0.1f));

            //Zmiana wielkości
            Vector3 velocity = Vector3.zero;
            transform.localScale = Vector3.SmoothDamp(transform.localScale, new Vector3(0, 0, 0), ref velocity, 0.3f);
            damage -= curDamage * Time.deltaTime * 5;
        }
        else
        {
            float H, S, V;
           Color.RGBToHSV(firstColor, out H, out S, out V);
            spriteRend.color = Color.Lerp(spriteRend.color, Color.HSVToRGB(H, 1 - transform.localScale.x / 20,1 ), Mathf.PingPong(Time.time, 0.2f));
        }
        if (transform.localScale.x < 0.5f)
        {
            Die();
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            damage += collision.GetComponent<IDamageAmount>().GetDamage();
            curDamage = damage;
            c = Color.HSVToRGB(Random.value, 1, 1);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
