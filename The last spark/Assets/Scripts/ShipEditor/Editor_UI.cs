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
        InitUI();
    }

    void InitUI()
    {
        foreach (SpriteRenderer sp in parts)
        {
            Button uiPart = Instantiate<Button>(button, gridLayout);
            uiPart.onClick.AddListener(() => SpawnPart(sp));
            uiPart.image.sprite = sp.sprite;
        }
    }

    void SpawnPart(SpriteRenderer sp)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        Instantiate<GameObject>(sp.gameObject, pos, Quaternion.identity);
    }
}
