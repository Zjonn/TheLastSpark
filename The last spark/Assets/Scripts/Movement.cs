using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 1;

    List<Transform> shipEngineParts;

    private Vector3 mousePos;
    private Rigidbody2D rb2d;

    private bool wPressed = false;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        shipEngineParts = new List<Transform>();
        Transform[] engineParts = GetComponentsInChildren<Transform>();

        foreach (Transform t in engineParts)
        {
            if (t.tag.Equals("Engine"))
                shipEngineParts.Add(t);
        }
    }


    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        wPressed = Input.GetKey(KeyCode.W);
    }

    private void FixedUpdate()
    {
        Vector3 dir = mousePos - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        angle = Mathf.LerpAngle(rb2d.rotation, angle, Time.deltaTime);
        rb2d.MoveRotation(angle);

        if (wPressed)
            rb2d.AddForce(transform.up * speed);
    }
}