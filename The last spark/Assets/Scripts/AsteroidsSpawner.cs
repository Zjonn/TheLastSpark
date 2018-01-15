using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    public float scale;
    public float w, h;
    public GameObject asteroid;
    public Transform parent;
    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                float xPos = i / w * scale;
                float yPos = j / h * scale;

                float noise = Mathf.PerlinNoise(xPos, yPos);

                if (noise > 0.59f && noise < 0.6f)
                {
                    Instantiate<GameObject>(asteroid, new Vector3(i - w / 2 + 0.64f, j - h / 2 + 0.64f, 0), new Quaternion(0, 0, 0, 0), parent);
                }
            }
        }
    }
}
