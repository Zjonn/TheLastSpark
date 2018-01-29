using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor_UI : MonoBehaviour
{
    public GameObject prefab;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
    }
}
