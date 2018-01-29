using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWeapon : MonoBehaviour, IWeapon
{
    public float movementSpeed = 10;
    public GameObject bulletPrefab;
    public Transform spawnPointLaser;
    Rigidbody2D player;


    List<GameObject> bullets;
	// Use this for initialization
	void Start () {
        bullets = new List<GameObject>();
        player = gameObject.GetComponentInParent(typeof(Rigidbody2D)) as Rigidbody2D;

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void FireLasert()
    {
        GameObject clone = Instantiate(bulletPrefab, spawnPointLaser.position, Quaternion.Euler(0, 0, Random.Range(-3, 3)) * spawnPointLaser.rotation) as GameObject;
        if (player != null)
            clone.GetComponent<Rigidbody2D>().velocity = player.velocity;
        clone.transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
        bullets.Add(clone);
        Debug.Log(true);
    }

    public void Fire()
    {
        FireLasert();
    }

    public override string ToString()
    {
        return gameObject.tag;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
