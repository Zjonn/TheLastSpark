using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public float health = 500;
    public GameObject laser;
    public GameObject missile;

    //Vector3 localPosition;
    Rigidbody2D boss;
    Collider2D m_Collider;
    Renderer m_Renderer;

    bool regenerate;
    bool canFire;
    float maxHealth;


    public Transform spawnPointCenter;
    public Transform spawnPointLeft0;
    public Transform spawnPointLeft1;
    public Transform spawnPointRight0;
    public Transform spawnPointRight1;


    // Use this for initialization
    void Start()
    {
        boss = GetComponentInParent<Rigidbody2D>();
        regenerate = false;
        canFire = true;
        maxHealth = health;
        m_Collider = GetComponent<Collider2D>();
        m_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            isActivate(false);
            regenerate = true;
        }
        if (health >= maxHealth)
        {
            isActivate(true);
            regenerate = false;
        }
        if (regenerate)
        {
            health += 100 * Time.deltaTime;
        }
    }

    void isActivate(bool toBool)
    {
        m_Renderer.enabled = toBool;
        m_Collider.enabled = toBool;
        canFire = toBool;
    }

    public void BossFireType0()
    {
        if (canFire)
        {
            GameObject clone = Instantiate(laser, spawnPointCenter.position, spawnPointCenter.rotation) as GameObject;
            if (boss != null)
                clone.GetComponent<Rigidbody2D>().velocity = boss.velocity;
        }
    }

    public void BossFireType1()
    {
        if (canFire)
        {
            //center spawnpoint
            GameObject clone0 = Instantiate(laser, spawnPointCenter.position, spawnPointCenter.rotation) as GameObject;
            if (boss != null)
                clone0.GetComponent<Rigidbody2D>().velocity = boss.velocity;

            //rest spawnpoints
            GameObject clone1 = Instantiate(laser, spawnPointLeft0.position, spawnPointLeft0.rotation) as GameObject;
            if (boss != null)
                clone1.GetComponent<Rigidbody2D>().velocity = boss.velocity;

            GameObject clone2 = Instantiate(laser, spawnPointLeft1.position, spawnPointLeft1.rotation) as GameObject;
            if (boss != null)
                clone2.GetComponent<Rigidbody2D>().velocity = boss.velocity;

            GameObject clone3 = Instantiate(laser, spawnPointRight0.position, spawnPointRight0.rotation) as GameObject;
            if (boss != null)
                clone3.GetComponent<Rigidbody2D>().velocity = boss.velocity;

            GameObject clone4 = Instantiate(laser, spawnPointRight1.position, spawnPointRight1.rotation) as GameObject;
            if (boss != null)
                clone4.GetComponent<Rigidbody2D>().velocity = boss.velocity;

        }
    }

    public void BossFireType2()
    {
        if (canFire)
        {
            GameObject clone0 = Instantiate(missile, spawnPointCenter.position, spawnPointCenter.rotation) as GameObject;
            if (boss != null)
                clone0.GetComponent<Rigidbody2D>().velocity = boss.velocity;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Osm") && !collision.CompareTag("Asteroid"))
        {
            health -= collision.GetComponent<IDamageAmount>().GetDamage();
        }
    }
}
