using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendCollision : MonoBehaviour
{
    public OsmAttractor oa;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Osm"))
        {
            oa.MagnetObject(obj, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Osm"))
        {
            oa.MagnetObject(obj, false);
        }
    }
}
