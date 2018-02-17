using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Editor_block : MonoBehaviour, ISendCollision
{
    public GameObject editorPoints;

    List<Pair> collisions;
    bool notAttached;

    public void EditorState(bool isActive)
    {
        if (editorPoints.activeSelf != isActive)
            editorPoints.SetActive(isActive);
    }

    public void Collision(GameObject caller, Collider2D go, bool isEnter)
    {
        if (isEnter)
        {
            collisions.Add(new Pair(caller, go.gameObject));
        }
        else
        {
            collisions.Remove(new Pair(caller, go.gameObject));
        }

    }

    private void Start()
    {
        collisions = new List<Pair>();
        notAttached = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManangment.isEditorMode)
        {
            EditorModeUpdate();
        }
        else
        {
            GameModeUpdate();
        }
    }

    void EditorModeUpdate()
    {
        EditorState(true);
        if (notAttached)
        {
            ManageInput();
        }
    }

    void ManageInput()
    {
        bool canBeAttached = (collisions.Count > 0) ? true : false;

        if (Input.GetKeyUp(KeyCode.R))
        {
            Rotate();
        }

        if ((Input.GetMouseButton(0) && !canBeAttached) || Input.GetMouseButton(1))
        {
            Destroy();
        }
        else if (Input.GetMouseButton(0) && canBeAttached)
        {
            Attach();
        }
        else
        {
            FollowMousePos();
        }
    }

    void Rotate()
    {
        var r = transform.rotation.eulerAngles;
        r += new Vector3(0, 0, 90);
        transform.rotation = Quaternion.Euler(r);
    }

    void Destroy()
    {
        DestroyObject(gameObject);
    }

    void Attach()
    {
        AlignPos();
        DisableAttachPoints();
        notAttached = false;
    }

    void AlignPos()
    {
        GameObject correctPos = GetParent(collisions[0].second);
        transform.parent = correctPos.transform;

        Vector3 diffCorrectPos = collisions[0].first.transform.position - collisions[0].second.transform.position;
        transform.position -= diffCorrectPos;
    }

    void DisableAttachPoints()
    {
        foreach (Pair p in collisions.ToArray<Pair>())
        {
            p.first.SetActive(false);
            p.second.SetActive(false);
        }
    }

    void FollowMousePos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;
    }

    void GameModeUpdate()
    {
        EditorState(false);
        if (notAttached)
        {
            Destroy(gameObject);
        }
    }


    GameObject GetParent(GameObject go)
    {
        return go.transform.parent.parent.gameObject;
    }
}

internal class Pair : IEqualityComparer<Pair>
{
    public GameObject first;
    public GameObject second;

    public Pair(GameObject f, GameObject s)
    {
        first = f;
        second = s;
    }

    public bool Equals(Pair x, Pair y)
    {
        return (x.first.transform.position == y.first.transform.position) && (x.second.transform.position == y.second.transform.position);
    }

    public int GetHashCode(Pair obj)
    {
        return 0;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var x = obj as Pair;
        return first == x.first && second && x.second;
    }

    public override int GetHashCode()
    {
        var hashCode = 405212230;
        hashCode = hashCode * -1521134295 + EqualityComparer<GameObject>.Default.GetHashCode(first);
        hashCode = hashCode * -1521134295 + EqualityComparer<GameObject>.Default.GetHashCode(second);
        return hashCode;
    }
}
