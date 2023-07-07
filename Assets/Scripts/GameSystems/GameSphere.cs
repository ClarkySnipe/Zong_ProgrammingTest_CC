using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSphere : SphereManager
{
    [SerializeField] float launchForce;
    public override void OnSpherePickup()
    {
        Debug.Log("Sphere has been picked up - disabling world canvas");
        
        myCanvas.SetActive(false);
        
    }
    public override void OnSphereDrop()
    {

        rb.AddForce(transform.root.forward * launchForce, ForceMode.Impulse);

        transform.SetParent(null);
        rb.useGravity = true;


        //myCanvas.SetActive(true);
        //myCanvas.transform.position = new Vector3(transform.position.x, transform.position.y + myCanvasYoffset, transform.position.z);
    }

    public override void SelectSphereType(int value)
    {
        base.SelectSphereType(value);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            rb.velocity = Vector3.zero;

            myCanvas.SetActive(true);
            myCanvas.transform.position = new Vector3(transform.position.x, transform.position.y + myCanvasYoffset, transform.position.z);
        }
    }
}
