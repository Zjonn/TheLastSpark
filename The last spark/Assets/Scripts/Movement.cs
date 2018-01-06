using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 1;

    List<Transform> shipEngineParts;

    List<Transform> partsW;
    List<Transform> partsS;
    List<Transform> partsA;
    List<Transform> partsD;

    private Vector3 mousePos;
    private Rigidbody2D rb2d;

    private bool wPressed = false;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        shipEngineParts = new List<Transform>();

        partsW = new List<Transform>();
        partsS = new List<Transform>();
        partsA = new List<Transform>();
        partsD = new List<Transform>();
        Transform[] engineParts = GetComponentsInChildren<Transform>();
        

        foreach (Transform t in engineParts)
        {
            if (!t.tag.Equals("Engine"))
                continue;

            if (t.rotation.Equals(transform.rotation))
                partsW.Add(t);
            else if (t.rotation.eulerAngles.Equals(new Vector3(0, 0, 180)))
                partsS.Add(t);
            else if (t.rotation.eulerAngles.Equals(new Vector3(0, 0, 90)))
                partsA.Add(t);
            else if (t.rotation.eulerAngles.Equals(new Vector3(0, 0, 270)))
                partsD.Add(t);

            shipEngineParts.Add(t);
            print(t.rotation.eulerAngles);
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
        {
            foreach(Transform t in partsW)
            {
                rb2d.AddForce(new Vector2(speed, 0)); 
            }
        }
            
    }
}