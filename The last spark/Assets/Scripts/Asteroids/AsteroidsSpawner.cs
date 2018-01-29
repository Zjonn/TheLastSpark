using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    public float scale;

    [Range(0.01f, 0.2f)]
    public float triggerRange;
    public float w, h;

    public GameObject asteroid;
    public Transform parent;

    // Use this for initialization
    void Awake()
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                float xPos = i / w * scale;
                float yPos = j / h * scale;

                float noise = Mathf.PerlinNoise(xPos, yPos);

                if (noise > 0.5-triggerRange && noise < 0.5f || noise > 0.15f && noise < 0.15+triggerRange || noise > 0.9f - triggerRange && noise < 0.9f)
                {
                    GameObject astero = Instantiate<GameObject>(asteroid, new Vector3((i - w / 2) * 10f, (j - h / 2) * 10f, 0), Quaternion.identity, parent);
                    float scale = Random.Range(2, 10);
                    astero.transform.localScale = new Vector3(scale, scale, 0);
                }
            }
        }
    }
}
