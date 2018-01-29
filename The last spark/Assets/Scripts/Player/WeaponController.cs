using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    List<IWeapon> WLaser;
    List<IWeapon> WRocket;
    List<IWeapon> WMelee;

    float nextFire = 0.1F;

    // Use this for initialization
    void Start()
    {
        WLaser = new List<IWeapon>();
        WRocket = new List<IWeapon>();
        WMelee = new List<IWeapon>();

        var objects = gameObject.GetComponentsInChildren<IWeapon>();
        if (objects != null)
        {
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
    }

    // Update is called once per frame
    void Update()
    {
        FireLaserType0();
        //Trzeba rozszerzyc przy wiekrzej ilosci broni
    }

    void FireLaserType0()
    {
        if (Input.GetMouseButton(0) && WLaser != null)
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
}
