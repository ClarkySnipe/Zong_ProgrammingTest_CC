using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    public Rigidbody rb;

    //This will control behaviours based on what the player decides
    public enum SphereType
    {
        inActive,
        wolfStone,
        lizardCrystal,
        crushSkullSteel
    }

    public SphereType sphereType = SphereType.inActive;

    public virtual void OnSpherePickup()
    {
        Debug.Log("Sphere has been picked up");
    }

    public virtual void OnSphereDrop()
    {
        Debug.Log("Sphere has been dropped");
    }

    /// <summary>
    /// Sphere types by int : 0. inActice 1. wolfStone 2. lizardCrystal 3. crushSkullSteel
    /// </summary>
    /// <param name="value"></param>
    public virtual void SelectSphereType(int value)
    {
        sphereType = (SphereType)value;

        Debug.Log("new sphere type = " + sphereType);
    }

}
