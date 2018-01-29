using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor_mainBlock : MonoBehaviour, ISendCollision
{
    public GameObject editor;
    Transform collision;
    Transform caller;

    public void Collision(GameObject caller, GameObject go, bool isEnter)
    {
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
        if (!Input.GetKey(KeyCode.Mouse0) && collision != null)
        {
            Transform parent = caller.root;
            collision.SetParent(transform);

            Vector3 diff = transform.position - caller.position;
            parent.position += diff;
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
