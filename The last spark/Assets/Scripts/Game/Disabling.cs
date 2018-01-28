﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabling : MonoBehaviour
{
    public int chunksSize;
    public float switchingDist;
    public float mapSizeX;
    public float mapSizeY;

    public Camera playerCamera;
    public int cornersPosShift;


    private Chunks chunks;
    private Vector3[] visiableChunks;

    // Use this for initialization
    void Start()
    {
        InitChunks();
    }

    private void InitChunks()
    {
        visiableChunks = new Vector3[4];
        chunks = new Chunks(FindObjectsOfType<Transform>(), mapSizeX, mapSizeY);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateChunks();
    }

    private void UpdateChunks()
    {
        Vector3 cameraPos = playerCamera.transform.position;
        cameraPos.z = 0;

        Vector3[] cameraCornersPos = new Vector3[4];

        cameraCornersPos[0] = playerCamera.ViewportToWorldPoint(new Vector3(0, 0, playerCamera.nearClipPlane));
        cameraCornersPos[0].x -= cornersPosShift;
        cameraCornersPos[0].y += cornersPosShift;

        cameraCornersPos[1] = playerCamera.ViewportToWorldPoint(new Vector3(1, 0, playerCamera.nearClipPlane));
        cameraCornersPos[1].x -= cornersPosShift;
        cameraCornersPos[1].y -= cornersPosShift;

        cameraCornersPos[2] = playerCamera.ViewportToWorldPoint(new Vector3(0, 1, playerCamera.nearClipPlane));
        cameraCornersPos[2].x += cornersPosShift;
        cameraCornersPos[2].y += cornersPosShift;

        cameraCornersPos[3] = playerCamera.ViewportToWorldPoint(new Vector3(1, 1, playerCamera.nearClipPlane));
        cameraCornersPos[3].x += cornersPosShift;
        cameraCornersPos[3].y -= cornersPosShift;


        for (int i = 0; i < 4; i++)
        {
            bool stillActive = false;

            if (visiableChunks[i] == chunks.GetChunkPos(cameraCornersPos[i]))
                continue;

            for (int j = 0; j < 4 && i != j; j++)
            {
                if (visiableChunks[i].Equals(chunks.GetChunkPos(cameraCornersPos[j])))
                    stillActive = true;
            }

            if (!stillActive)
            {
                chunks.DisableChunkElements(chunks.GetChunkFromPos(visiableChunks[i]));
            }
            visiableChunks[i] = chunks.GetChunkPos(cameraCornersPos[i]);
        }

        foreach (List<Transform> chunk in chunks.GetChunks(visiableChunks))
        {
            chunks.DisableOutOfSignElements(chunk, cameraPos, switchingDist);
        }
    }

    private void OnValidate()
    {
        switchingDist = (switchingDist > 0) ? switchingDist : 1;
        mapSizeX = (mapSizeX > 0) ? mapSizeX : 1;
        mapSizeY = (mapSizeY > 0) ? mapSizeY : 1;
        chunksSize = (chunksSize > 0) ? chunksSize : 1;
    }

    private void Reset()
    {
        switchingDist = 25;
        chunksSize = 50;
        mapSizeX = 500;
        mapSizeY = 500;
    }
}

class Chunks
{
    public int chunksSize;

    public int sizeX, sizeY;

    public List<Transform>[,] chunks;

    private int sortedElements;

    public Chunks(Transform[] objects, float sizeX, float sizeY, int chunksSize = 50)
    {
        this.sizeX = (int)sizeX;
        this.sizeY = (int)sizeY;
        this.chunksSize = chunksSize;
        InitChunksArray();
        SortElements(objects);
    }

    public List<Transform> GetChunkFromWorldPos(float x, float y)
    {
        int w = (int)((x + sizeX / 2) / chunksSize);
        int h = (int)((y + sizeY / 2) / chunksSize);

        if (w < 0) w = 0;
        else if (w >= sizeX / chunksSize) w = sizeX / chunksSize - 1;

        if (h < 0) h = 0;
        else if (h >= sizeY / chunksSize) h = sizeY / chunksSize - 1;

        return chunks[w, h];
    }

    public List<Transform> GetChunkFromWorldPos(Vector2 v)
    {
        return GetChunkFromWorldPos(v.x, v.y);
    }

    public List<Transform> GetChunkFromWorldPos(Vector3 v)
    {
        return GetChunkFromWorldPos(v.x, v.y);
    }

    public List<Transform> GetChunkFromPos(Vector3 v)
    {
        return chunks[(int)v.x, (int)v.y];
    }

    public List<List<Transform>> GetChunks(Vector3[] v3)
    {
        List<List<Transform>> chunks = new List<List<Transform>>();

        foreach (Vector3 v in v3)
        {
            List<Transform> chunk = this.chunks[(int)v.x, (int)v.y];
            //Brzydkie :/
            if (!chunks.Contains(chunk))
                chunks.Add(chunk);
        }
        return chunks;
    }

    public Vector3 GetChunkPos(Vector3 v)
    {
        int w = (int)((v.x + sizeX / 2) / chunksSize);
        int h = (int)((v.y + sizeY / 2) / chunksSize);

        if (w < 0) w = 0;
        else if (w >= sizeX / chunksSize) w = sizeX / chunksSize - 1;

        if (h < 0) h = 0;
        else if (h >= sizeY / chunksSize) h = sizeY / chunksSize - 1;

        return new Vector3(w, h, 0);
    }

    public void DisableChunkElements(List<Transform> chunk)
    {
        foreach (Transform t in chunk)
        {
            if (t.gameObject.activeSelf)
                t.gameObject.SetActive(false);
        }
    }

    public void DisableOutOfSignElements(List<Transform> chunk, Vector2 point, float distance = 20)
    {
        bool hasNull = false;
        foreach (Transform go in chunk)
        {
            if (go.Equals(null))
            {
                hasNull = true;
                continue;
            }

            if (Vector3.Distance(point, go.position) > distance && go.gameObject.activeSelf)
            {
                go.gameObject.SetActive(false);
            }
            else if (Vector3.Distance(point, go.position) <= distance && !go.gameObject.activeSelf)
            {
                go.gameObject.SetActive(true);
            }
        }
        if (hasNull) chunk.RemoveAll(x => x == null);
    }

    private void InitChunksArray()
    {
        int cX = (int)(sizeX / chunksSize);
        int cY = (int)(sizeY / chunksSize);

        chunks = new List<Transform>[cX, cY];

        for (int x = 0; x < cX; x++)
        {
            for (int y = 0; y < cY; y++)
            {
                chunks[x, y] = new List<Transform>();
            }
        }
    }

    private void SortElements(Transform[] elems)
    {
        sortedElements = 0;
        foreach (Transform elem in elems)
        {
            if (elem.gameObject.activeInHierarchy && elem.gameObject.layer == 9)
            {
                GetChunkFromWorldPos(elem.position.x, elem.position.y).Add(elem);
                elem.gameObject.SetActive(false);
                sortedElements++;

            }
        }
    }

    override
    public string ToString()
    {
        return "W chunkach znajduje się " + sortedElements + " elementów.";
    }
}
