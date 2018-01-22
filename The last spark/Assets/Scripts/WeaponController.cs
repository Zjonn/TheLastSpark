using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    Transform[] objects;
    List<Transform> WLaser;
    List<Transform> WRocket;
    List<Transform> WMelee;

    float nextFire = 0.0F;

    // Use this for initialization
    void Start()
    {
        WLaser = new List<Transform>();
        WRocket = new List<Transform>();
        WMelee = new List<Transform>();

        objects = gameObject.GetComponentsInChildren<Transform>() as Transform[];
        if (objects != null)
        {
            foreach (Transform object0 in objects)
            {
                if (object0.CompareTag("WeaponLaser"))
                {
                    WLaser.Add(object0);
                }
                else if (object0.CompareTag("WeaponLaser"))
                {
                    WRocket.Add(object0);
                }
                else if (object0.CompareTag("WeaponLaser"))
                {
                    WMelee.Add(object0);
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
        if (Input.GetMouseButton(0) && WLaser != null && Time.time > nextFire)
        {
            nextFire = Time.time + 0.1f;
            foreach (Transform weapon in WLaser)
            {
                weapon.GetComponent<IWeapon>().Fire();
            }
        }
    }
}
