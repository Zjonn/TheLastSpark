using System.Collections;
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
    private List<Vector3> visibleChunks;

    // Use this for initialization
    void Start()
    {
        InitChunks();  
    }

    private void InitChunks()
    {
        visibleChunks = new List<Vector3>();
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

        cameraCornersPos[0] = CameraCorners(playerCamera.ViewportToWorldPoint(new Vector3(0, 0, playerCamera.nearClipPlane)), false, false);
        cameraCornersPos[1] = CameraCorners(playerCamera.ViewportToWorldPoint(new Vector3(1, 0, playerCamera.nearClipPlane)), true, false);
        cameraCornersPos[2] = CameraCorners(playerCamera.ViewportToWorldPoint(new Vector3(0, 1, playerCamera.nearClipPlane)), false, true);
        cameraCornersPos[3] = CameraCorners(playerCamera.ViewportToWorldPoint(new Vector3(1, 1, playerCamera.nearClipPlane)), true, true);

        List<Vector3> newVisibleChunks = new List<Vector3>();

        for (int i = (int)cameraCornersPos[0].x; i <= (int)(cameraCornersPos[3].x); i++)
        {
            for (int j = (int)cameraCornersPos[0].y; j <= (int)(cameraCornersPos[3].y); j++)
            {
                newVisibleChunks.Add(new Vector3(i, j, 0));
            }
        }

        foreach (Vector3 v in visibleChunks)
        {
            if (!newVisibleChunks.Contains(v))
            {
                chunks.DisableChunkElements(chunks.GetChunkFromPos(v));
            }
        }
        visibleChunks = newVisibleChunks;
        foreach (List<Transform> chunk in chunks.GetChunks(visibleChunks))
        {
            chunks.DisableOutOfSignElements(chunk, cameraPos, switchingDist);
        }
    }

    private Vector3 CameraCorners(Vector3 camPos, bool addX, bool addY)
    {
        camPos.x = (addX) ? camPos.x + cornersPosShift : camPos.x - cornersPosShift;
        camPos.y = (addY) ? camPos.y + cornersPosShift : camPos.y - cornersPosShift;
        return chunks.GetChunkPos(camPos);
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
    public int chunkSize;

    public int sizeX, sizeY;

    public List<Transform>[,] chunks;

    private int sortedElements;

    public Chunks(Transform[] objects, float sizeX, float sizeY, int chunkSize = 50)
    {
        this.sizeX = (int)sizeX;
        this.sizeY = (int)sizeY;
        this.chunkSize = chunkSize;
        InitChunksArray();
        SortElements(objects);
    }

    public List<Transform> GetChunkFromWorldPos(float x, float y)
    {
        int w = (int)((x + sizeX / 2) / chunkSize);
        int h = (int)((y + sizeY / 2) / chunkSize);

        if (w < 0) w = 0;
        else if (w >= sizeX / chunkSize) w = sizeX / chunkSize - 1;

        if (h < 0) h = 0;
        else if (h >= sizeY / chunkSize) h = sizeY / chunkSize - 1;

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

    public List<List<Transform>> GetChunks(List<Vector3> v3)
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
        int w = (int)((v.x + sizeX / 2) / chunkSize);
        int h = (int)((v.y + sizeY / 2) / chunkSize);

        if (w < 0) w = 0;
        else if (w >= sizeX / chunkSize) w = sizeX / chunkSize - 1;

        if (h < 0) h = 0;
        else if (h >= sizeY / chunkSize) h = sizeY / chunkSize - 1;

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
        int cX = (int)(sizeX / chunkSize);
        int cY = (int)(sizeY / chunkSize);

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
            GameObject go = elem.gameObject;
            if (go.activeInHierarchy && (go.layer == 9 || go.layer == 11))
            {
                GetChunkFromWorldPos(elem.position.x, elem.position.y).Add(elem);              
                go.SetActive(false);
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

