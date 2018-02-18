using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    List<IWeapon> WLaser;
    List<IWeapon> WRocket;
    List<IWeapon> WMelee;
    IWeapon[] objects;

    void Start()
    {
        WLaser = new List<IWeapon>();
        WRocket = new List<IWeapon>();
        WMelee = new List<IWeapon>();
        objects = gameObject.GetComponentsInChildren<IWeapon>();
        AddWeaponsToLists();
    }

    // Update is called once per frame
    void Update()
    {
        FireLaserType0();
        //Trzeba rozszerzyc przy wiekrzej ilosci broni
    }

    void AddWeaponsToLists()
    {
        if (objects != null)
            return;

        foreach (IWeapon object0 in objects)
        {
            switch (object0.ToString())
            {
                case "WeaponLaser":
                    WLaser.Add(object0);
                    break;
                case "WeaponLaser2":
                    WRocket.Add(object0);
                    break;
                case "WeaponSTH":
                    WMelee.Add(object0);
                    break;
            }
        }
}

    void FireLaserType0()
    {
        if (Input.GetMouseButton(0) && WLaser != null)
        {
            FireEachWeapon();
        }
    }

    void FireEachWeapon()
    {
        foreach (IWeapon weapon in WLaser)
        {
            if (weapon.GetGameObject().activeInHierarchy)
            {
                weapon.Fire();
            }
        }
    }
}
