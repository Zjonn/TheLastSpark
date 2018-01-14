using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabling : MonoBehaviour
{
    public float switchingDist;
    public int chunksSize;
    public float maxX;
    public float maxY;

    public Transform player;

    List<Transform>[,] chunks;
    //List<Transform> gameObjects;

    // Use this for initialization
    void Start()
    {
        int cX = (int)(maxX * 2 / chunksSize);
        int cY = (int)(maxY * 2 / chunksSize);

        chunks = new List<Transform>[cX, cY];
        for (int x = 0; x < cX; x++)
        {
            for (int y = 0; y < cY; y++)
            {
                chunks[x, y] = new List<Transform>();
            }
        }
        int i = 0;
        //gameObjects = new List<Transform>();
        foreach (Transform go in FindObjectsOfType<Transform>())
        {
            if (go.gameObject.activeInHierarchy && go.gameObject.layer == 9)
            {
                GetChunk(go.position.x, go.position.y).Add(go);
                go.gameObject.SetActive(false);
                print(++i);
            }
        }
    }

    List<Transform> GetChunk(float x, float y)
    {
        return chunks[(int)(x / chunksSize + maxX / chunksSize), (int)(y / chunksSize + maxY / chunksSize)];
    }


    // Update is called once per frame
    void Update()
    {
        foreach (Transform go in GetChunk(player.position.x, player.position.y))
        {
            if (go.Equals(null))
            {
                GetChunk(player.position.x, player.position.y).RemoveAll(null);
            }
            if (Vector3.Distance(player.position, go.position) > switchingDist && go.gameObject.activeSelf)
            {
                go.gameObject.SetActive(false);
            }
            else if (Vector3.Distance(player.position, go.position) <= switchingDist && !go.gameObject.activeSelf)
            {
                go.gameObject.SetActive(true);
            }
        }
    }

    private void Reset()
    {
        switchingDist = 25;
        chunksSize = 50;
        maxX = 500;
        maxY = 500;
    }

    private void OnValidate()
    {
        switchingDist = (switchingDist > 0) ? switchingDist : 1;
        maxX = (maxX > 0) ? maxX : 1;
        maxY = (maxY > 0) ? maxY : 1;
        chunksSize = (chunksSize > 0) ? chunksSize : 1;
    }
}

