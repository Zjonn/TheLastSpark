using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsmAttractor : MonoBehaviour, IOsmCollector
{

    List<Transform> osms;
    bool isSpace;

    public void AttractorState(bool state)
    {
        isSpace = state;
    }

    void Start()
    {
        osms = new List<Transform>();
        isSpace = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Transform osm in osms)
        {
            Vector2 v = osm.position;
            osm.position = Vector3.MoveTowards(v, transform.position, Time.deltaTime * (3 + Vector2.Distance(v, transform.position)));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Osm"))
        {
            if (isSpace)
                osms.Add(obj.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Osm"))
        {
            osms.Remove(obj.transform);
        }
    }

    public float GetCapacity()
    {
        return 50;
    }
}
