using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendCollision : MonoBehaviour
{
    ISendCollision toSend;

    private void Start()
    {
        toSend = GetComponentInParent<ISendCollision>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Osm"))
        {
            toSend.Collision(obj, gameObject, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Osm"))
        {
            toSend.Collision(obj, gameObject, false);
        }
    }
}
