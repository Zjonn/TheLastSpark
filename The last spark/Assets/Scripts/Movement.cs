using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 1;

    //Silniki odpowiadające za ruch w danym kierunku
    List<Transform> partsW;
    List<Transform> partsS;
    List<Transform> partsA;
    List<Transform> partsD;

    //Silniki odpowiadające za obrót w danym kierunku
    List<Transform> partsQ;
    List<Transform> partsE;

    private Vector3 mousePos;
    private Rigidbody2D rb2d;

    private bool wPressed = false;
    private bool aPressed = false;
    private bool sPressed = false;
    private bool dPressed = false;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        partsW = new List<Transform>();
        partsS = new List<Transform>();
        partsA = new List<Transform>();
        partsD = new List<Transform>();

        partsE = new List<Transform>();
        partsQ = new List<Transform>();

        Transform[] engineParts = GetComponentsInChildren<Transform>();

        foreach (Transform t in engineParts)
        {
            if (!t.tag.Equals("Engine"))
                continue;

            if (t.rotation.Equals(transform.rotation))
            {
                partsW.Add(t);
                if (t.position.x < transform.position.x)
                {
                    partsE.Add(t);
                }
                else
                {
                    partsQ.Add(t);
                }
            }
            else if (t.rotation.eulerAngles.Equals(new Vector3(0, 0, 180)))
                partsS.Add(t);
            else if (t.rotation.eulerAngles.Equals(new Vector3(0, 0, 90)))
                partsA.Add(t);
            else if (t.rotation.eulerAngles.Equals(new Vector3(0, 0, 270)))
                partsD.Add(t);


            //print(t.rotation.eulerAngles);
        }
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        wPressed = Input.GetKey(KeyCode.W);
    }

    private void FixedUpdate()
    {
        Vector3 shipDir = mousePos - this.transform.position;
        float angle = Mathf.Atan2(shipDir.y, shipDir.x) * Mathf.Rad2Deg - 90;
        angle = Mathf.LerpAngle(rb2d.rotation, angle, Time.deltaTime);

        if (wPressed)
        {
            wasdAddForce(partsW);
        }
        if (aPressed)
        {
            wasdAddForce(partsA);
        }
        if (sPressed)
        {
            wasdAddForce(partsS);
        }
        if (dPressed)
        {
            wasdAddForce(partsD);
        }

    }

    private void wasdAddForce(List<Transform> parts)
    {
        foreach(Transform t in parts)
        {
            Vector3 dir = t.rotation * new Vector3(0, speed, 0);
            rb2d.AddForceAtPosition(new Vector2(dir.x, dir.y), t.position);
        }
    }
}