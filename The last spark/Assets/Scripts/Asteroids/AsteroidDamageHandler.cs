using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDamageHandler : MonoBehaviour
{
    SpriteRenderer spriteRend;
    OsmSpawner osmSpawner;
    Color firstColor;
    Color newColor;

    float curDamage = 0;
    float damage = 0;
    Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        osmSpawner = GetComponent<OsmSpawner>();
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.color = Color.HSVToRGB(Random.value, 1, 1);
        firstColor = spriteRend.color;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.localScale.x < 0.5f)
        {
            Die();
        }
        else if (damage > 0)
        {
            //Zmiana koloru przy otrzymaniu obrażeń
            spriteRend.color = Color.Lerp(spriteRend.color, newColor, Mathf.PingPong(Time.time, 0.1f));

            //Zmiana wielkości           
            transform.localScale = Vector3.SmoothDamp(transform.localScale, new Vector3(0, 0, 0), ref velocity, 50 * 1 / damage);
            damage -= curDamage * Time.deltaTime * 5;
        }
        else
        {
            float H, S, V;
            Color.RGBToHSV(firstColor, out H, out S, out V);
            spriteRend.color = Color.Lerp(spriteRend.color, Color.HSVToRGB(H, 1 - transform.localScale.x / 20, 1), Mathf.PingPong(Time.time, 0.2f));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Osm") && !collision.CompareTag("Asteroid"))
        {
            float dealDamage = collision.GetComponent<IDamageAmount>().GetDamage();
            damage += dealDamage;
            curDamage = damage;
            newColor = Color.HSVToRGB(Random.value, 1, 1);
            osmSpawner.spawnOsm(transform.localScale.x);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
