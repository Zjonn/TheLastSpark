using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCollector : MonoBehaviour, IOsmCollector {
    public float GetCapacity()
    {
        return 100;
    }
}
