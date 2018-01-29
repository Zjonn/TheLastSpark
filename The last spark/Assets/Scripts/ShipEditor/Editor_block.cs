using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Editor_block : MonoBehaviour, ISendCollision
{
    public GameObject editor;
    public bool isNew;
    List<Pair> collisions;


    private void Start()
    {
        collisions = new List<Pair>();
        isNew = false;
    }

    public void EditorState(bool isActive)
    {
        editor.SetActive(isActive);
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

    // Update is called once per frame
    void Update()
    {
        if (!GameManangment.isEditor)
        {
            editor.SetActive(false);
            return;
        }
        else if (!editor.activeSelf)
        {
            editor.SetActive(true);
        }

        if (isNew && GameManangment.isKeyPressed(KeyCode.R, true))
        {
            var r = transform.rotation.eulerAngles;
            r += new Vector3(0, 0, 90);
            transform.rotation = Quaternion.Euler(r);
        }
        if (!isNew)
            return;

        else if ((Input.GetMouseButton(0) && collisions.Count == 0) || Input.GetMouseButton(1))
        {
            Destroy(gameObject);
        }
        else if (Input.GetMouseButton(0) && collisions.Count > 0)
        {
            GameObject otherParent = GetParent(collisions[0].second);
            transform.parent = otherParent.transform;

            Vector3 diff = collisions[0].first.transform.position - collisions[0].second.transform.position;
            transform.position -= diff;

            foreach (Pair p in collisions.ToArray<Pair>())
            {
                p.first.SetActive(false);
                p.second.SetActive(false);
            }
            isNew = false;
        }
        else
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            transform.position = pos;
        }
    }

    GameObject GetParent(GameObject go)
    {
        return go.transform.parent.parent.gameObject;
    }
}

class Pair : IEqualityComparer<Pair>
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

    // override object.Equals
    public override bool Equals(object obj)
    {
        //       
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237  
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var x = obj as Pair;
        return first == x.first && second && x.second;
    }

}
