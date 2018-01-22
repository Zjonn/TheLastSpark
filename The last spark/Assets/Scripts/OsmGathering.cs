using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OsmGathering : MonoBehaviour
{

    public float capacity;
    public float osmVal;
    public Text osmInfo;

    float maxCapacity;

    float usedCapacity;
    public float UsedCapacity
    {
        set
        {
            if (usedCapacity + value > maxCapacity)
                usedCapacity = maxCapacity;
            else
                usedCapacity = value;
        }
        get
        {
            return usedCapacity;
        }
    }

    List<Transform> osms;

    // Use this for initialization
    void Start()
    {
        osms = new List<Transform>();

        maxCapacity = capacity;
        osmInfo.text = "Osm: 0/" + maxCapacity;

        IOsmCollector[] ocs = GetComponentsInChildren<IOsmCollector>();
        foreach (IOsmCollector oc in ocs)
        {
            maxCapacity += oc.GetCapacity();
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Osm":
                UsedCapacity += collision.gameObject.GetComponent<Osm>().Value;
                osmInfo.text = "Osm: " + UsedCapacity + "/" + maxCapacity;
                Destroy(collision.gameObject);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        switch (obj.tag)
        {
            case "Osm":
                if (UsedCapacity != maxCapacity)
                    osms.Add(obj.transform);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        switch (obj.tag)
        {
            case "Osm":
                osms.Remove(obj.transform);
                break;
        }
    }
}
