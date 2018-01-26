using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OsmGathering : MonoBehaviour
{
    public float capacity;
    public Text osmInfo;

    OsmAttractor osmAttr;

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

    void Start()
    {
        maxCapacity = capacity;
        osmInfo.text = "Osm: 0/" + maxCapacity;

        osmAttr = GetComponentInChildren<OsmAttractor>();

        IOsmCollector[] ocs = GetComponentsInChildren<IOsmCollector>();
        foreach (IOsmCollector oc in ocs)
        {
            maxCapacity += oc.GetCapacity();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Osm"))
        {
            UsedCapacity += collision.gameObject.GetComponent<Osm>().Value;
            osmInfo.text = "Osm: " + UsedCapacity + "/" + maxCapacity;
            Destroy(collision.gameObject);
        }
    }

    void SpecifyAttractorState()
    {
        if (UsedCapacity == maxCapacity && osmAttr != null)
        {
            osmAttr.AttractorState(false);
        }
        else
        {
            osmAttr.AttractorState(true);
        }
    }
}
