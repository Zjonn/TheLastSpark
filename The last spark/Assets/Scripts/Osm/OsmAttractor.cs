using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsmAttractor : MonoBehaviour, IOsmCollector, ISendCollision
{
    public Editor_block sc;
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
            osm.position = Vector3.MoveTowards(v, transform.position, Time.deltaTime * (3 + 8 / Vector2.Distance(v, transform.position)));
        }
    }

    public void Collision(GameObject un, Collider2D go, bool toMagnet)
    {
        if (go.CompareTag("Osm"))
        {
            if (toMagnet && isSpace)
            {
                osms.Add(go.transform);
            }
            else
            {
                osms.Remove(go.transform);
            }
        }
        else
        {
            sc.Collision(un, go, toMagnet);
        }
    }

    public float GetCapacity()
    {
        return 50;
    }
}
