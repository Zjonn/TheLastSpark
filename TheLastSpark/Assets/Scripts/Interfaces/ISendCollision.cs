using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISendCollision
{
    void Collision(GameObject sendingObject, Collider2D collision, bool isEnter);
}
