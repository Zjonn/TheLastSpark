using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 1;
    [Range(0,1)]
    public float rotationSpeed = 1;
    public float maxSpeed = 50;
    public float maxRotationSpeed = 100;

    //Silniki odpowiadające za ruch w danym kierunku
    List<EnginePart> partsW;
    List<EnginePart> partsS;
    List<EnginePart> partsA;
    List<EnginePart> partsD;

    //Silniki odpowiadające za obrót w danym kierunku
    List<EnginePart> partsQ;
    List<EnginePart> partsE;

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

        partsW = new List<EnginePart>();
        partsS = new List<EnginePart>();
        partsA = new List<EnginePart>();
        partsD = new List<EnginePart>();

        partsE = new List<EnginePart>();
        partsQ = new List<EnginePart>();

        Transform[] engineParts = GetComponentsInChildren<Transform>();

        foreach (Transform t in engineParts)
        {
            if (!t.tag.Equals("Engine"))
                continue;

            EnginePart enginePart = new EnginePart(t, t.GetComponent<Engine>());
            if (t.rotation.Equals(transform.rotation))
            {
                partsW.Add(enginePart);
                if (t.position.x < transform.position.x)
                {
                    partsE.Add(enginePart);
                }
                else if (t.position.x > transform.position.x)
                {
                    partsQ.Add(enginePart);
                }
            }
            else if (t.rotation.eulerAngles.Equals(new Vector3(0, 0, 180)))
            {
                partsS.Add(enginePart);
                if (t.position.x < transform.position.x)
                {
                    partsQ.Add(enginePart);
                }
                else if (t.position.x > transform.position.x)
                {
                    partsE.Add(enginePart);
                }
            }
            else if (t.rotation.eulerAngles.Equals(new Vector3(0, 0, 90)))
            {
                partsA.Add(enginePart);
                if (t.position.y < transform.position.y)
                {
                    partsE.Add(enginePart);
                }
                else if (t.position.y > transform.position.y)
                {
                    partsQ.Add(enginePart);
                }
            }
            else if (t.rotation.eulerAngles.Equals(new Vector3(0, 0, 270)))
            {
                partsD.Add(enginePart);
                if (t.position.y < transform.position.y)
                {
                    partsQ.Add(enginePart);
                }
                else if (t.position.y > transform.position.y)
                {
                    partsE.Add(enginePart);
                }
            }

        }
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        wPressed = Input.GetKey(KeyCode.W);
        aPressed = Input.GetKey(KeyCode.A);
        sPressed = Input.GetKey(KeyCode.S);
        dPressed = Input.GetKey(KeyCode.D);
    }

    private void FixedUpdate()
    {
        Vector3 shipDir = mousePos - this.transform.position;
        float angle = Mathf.Atan2(shipDir.y, shipDir.x) * Mathf.Rad2Deg - 90;

        EngineAddRotation(angle);

        if (wPressed)
        {
            EngineAddForce(partsW);
        }
        if (aPressed)
        {
            EngineAddForce(partsA);
        }
        if (sPressed)
        {
            EngineAddForce(partsS);
        }
        if (dPressed)
        {
            EngineAddForce(partsD);
        }


        //Ograniczenie prędkości
        if (rb2d.velocity.magnitude > maxSpeed || rb2d.velocity.magnitude < -maxSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
        }
        //Ograniczenie prędkości obrotu
        if (rb2d.angularVelocity > maxRotationSpeed)
        {
            rb2d.angularVelocity = maxRotationSpeed;
        }
        else if (rb2d.angularVelocity < -maxRotationSpeed)
        {
            rb2d.angularVelocity = -maxRotationSpeed;
        }
    }

    private void EngineAddForce(List<EnginePart> parts)
    {
        foreach (EnginePart e in parts)
        {
            if (e.EngineTransform.gameObject.activeInHierarchy)
            {
                float avaibleThrust = e.EngineScript.AvailableThrust;
                e.EngineScript.SetThrust(avaibleThrust);

                Transform t = e.EngineTransform;
                Vector3 dir = t.rotation * new Vector3(0, speed * avaibleThrust / e.EngineScript.maxThrust, 0);
                rb2d.AddForceAtPosition(new Vector2(dir.x, dir.y), t.position);
            }
        }
    }

    private void EngineAddRotation(float angle)
    {
        float angleDiff = Mathf.DeltaAngle(rb2d.rotation, angle);

        if (angleDiff > 0.5 && rb2d.angularVelocity < angleDiff)
        {
            AddTorqueByParts(partsQ, 1, angleDiff);
        }
        else if (angleDiff < -0.5 && rb2d.angularVelocity > angleDiff)
        {
            AddTorqueByParts(partsE, -1, angleDiff);
        }
    }

    private void AddTorqueByParts(List<EnginePart> parts, int sign, float angleDiff)
    {
        float thrustMultipler;
        angleDiff = Mathf.Abs(angleDiff);

        thrustMultipler = angleDiff / 180;

        foreach (EnginePart t in parts)
        {
            if (t.EngineTransform.gameObject.activeInHierarchy)
            {               
                float thrust = t.EngineScript.AvailableThrust * thrustMultipler;
                rb2d.AddTorque(sign * thrust * rotationSpeed);
                t.EngineScript.SetRotation(thrust);
            }
        }
    }

    private void Reset()
    {
        speed = 2;
        rotationSpeed = 1;
        maxSpeed = 50;
        maxRotationSpeed = 1000;
    }
}

class EnginePart
{
    public Transform EngineTransform
    {
        get;
        private set;
    }
    public Engine EngineScript
    {
        get;
        private set;
    }

    public EnginePart(Transform t, Engine e)
    {
        EngineTransform = t;
        EngineScript = e;
    }
}