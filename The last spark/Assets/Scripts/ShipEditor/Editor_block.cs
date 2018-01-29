using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor_block : MonoBehaviour, ISendCollision
{
    public GameObject editor;
    Transform collision = null;
    Transform caller;
    bool isNew = true;


    public void Collision(GameObject caller, GameObject go, bool isEnter)
    {
        print(caller);
        if (isEnter)
        {
            collision = go.transform;
            this.caller = caller.transform;
        }
        else
            collision = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isNew && ((Input.GetMouseButton(0) && collision == null) || Input.GetMouseButton(1)))
        {
               Destroy(gameObject);
        }
        else if (isNew)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.Mouse0) && collision != null)
        {
            Transform parent = caller.root;
            collision.SetParent(transform);

            Vector3 diff = transform.position - caller.position;
            parent.position += diff;
            isNew = false;
        }
        //else if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    editor.SetActive(true);
        //}
        //else
        //{
        //    editor.SetActive(false);
        //}
    }
}
