using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Editor_UI : MonoBehaviour
{
    public GameObject prefab;
    public List<SpriteRenderer> parts;
    public Transform gridLayout;
    public Button button;
    // Use this for initialization
    void Start()
    {
        foreach (SpriteRenderer sp in parts)
        {
            Button go = Instantiate<Button>(button, gridLayout);
            go.onClick.AddListener(() => Click(sp));
            go.image.sprite = sp.sprite;
        }
    }

    public void Click(SpriteRenderer sp)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        Instantiate<GameObject>(sp.gameObject, pos, Quaternion.identity);
    }
}
