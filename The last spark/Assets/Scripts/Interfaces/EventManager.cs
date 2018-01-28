using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public interface IWeapon
{
    void Fire();
    GameObject GetGameObject();
    //Nadpisz ToString żeby zwracało tag!
}
