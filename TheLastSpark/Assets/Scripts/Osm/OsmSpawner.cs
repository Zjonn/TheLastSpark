using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsmSpawner : MonoBehaviour
{

    [Range(0, 1)]
    public float chance;
    [Range(1, 5)]
    public int InOneUpdate;

    public List<GameObject> osmPrefabs;

    int osmToSpawn = 0;
    SpriteRenderer sr;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; osmToSpawn > 0 && i < InOneUpdate; i++, osmToSpawn--)
        {
            Vector3 dir = Quaternion.Euler(0, 0, Random.Range(0, 360)) * (sr.bounds.size / 3);
            Vector3 pos = dir + transform.position;


            GameObject osm = Instantiate<GameObject>(osmPrefabs[Random.Range(0, osmPrefabs.Count)], pos, Quaternion.identity);
            osm.GetComponent<Rigidbody2D>().AddForce(dir.normalized);
        }
    }

    public void spawnOsm(float numF)
    {
        int num = (int)numF;
        for (int i = 0; i < num; i++)
        {
            float rand = Random.value;
            if (rand < chance)
            {
                osmToSpawn++;
            }
        }
    }

    private void Reset()
    {
        chance = 0.5f;
        InOneUpdate = 1;
    }
}
