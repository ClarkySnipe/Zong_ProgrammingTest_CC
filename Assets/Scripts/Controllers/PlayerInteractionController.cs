using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    //This script will perform raycasts/spherecasts while the player is holding down RMB
    //if the player is to overlap with the gamesphere - we will lerp it to our obj holder position
    //If the player is to release RMB while holder the sphere, we will drop (or launch) it

    [SerializeField] Transform objectHolderTform;
    [SerializeField] GameSphere currentObject;

    [Header("Casting Values")]
    [SerializeField] float interactionDistance;
    [SerializeField] float interactionRadius;
    [SerializeField] LayerMask interactionMask;
    private Vector3 hitLocation;


    [Header("Object Interaction Values")]
    [SerializeField] float objMoveSpeed;
    [SerializeField] bool isHoldingObj;


    private Vector3 rayOrigin; //our screen center point

    private void Start()
    {
        rayOrigin = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    private void Update()
    {
        if (!Utilities.IsRightClicking()) //if we let go of right click
        {
            isHoldingObj = false;

            if (currentObject)
            {
                currentObject.transform.SetParent(null);

                currentObject.rb.useGravity = true;

                currentObject = null;
            }
            return; // we are not right clicking so we dont search for an obj. -- will need to add drop mechanics here
        }


        hitLocation = PerformRayCast();

        if (hitLocation == Vector3.zero) return;

        PerformOverlapSphere();

        if (!currentObject) return;

        //PerformObjectMoveTo();

        //if (!isHoldingObj)
            PerformObjectMoveTo();
       //else
            //currentObject.transform.position = objectHolderTform.position;

    }

    private Vector3 PerformRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(rayOrigin);

        RaycastHit hit;

       
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            //Debug.Log("Ray hit something");
            return hit.point;
        }

        //Debug.Log("Ray did not hit something");
        return Vector3.zero;


    }

    private void PerformOverlapSphere()
    {
        RaycastHit hit;

        Collider[] colliders = Physics.OverlapSphere(hitLocation, interactionRadius, interactionMask);

        foreach (Collider c in colliders)
        {
            GameSphere tempSphere = c.transform.GetComponent<GameSphere>();

            if (tempSphere != null)
            {
                Debug.Log("We hit our sphere");
                currentObject = tempSphere;
                return;
            }
        }

    }

    private void PerformObjectMoveTo()
    {
        //Vector3 direction = (objectHolderTform.position - currentObject.transform.position).normalized;

        //currentObject.transform.position = currentObject.transform.position + (direction * objMoveSpeed * Time.deltaTime);
        //move our gameobject to ur ObjectHolderTForm
        //currentObject.transform.position = Vector3.Lerp(currentObject.transform.position, objectHolderTform.position, objMoveSpeed * Time.deltaTime);

        if (Vector3.Distance(currentObject.transform.position, objectHolderTform.position) > 0.15f)
        {
            Vector3 direction = (objectHolderTform.position - currentObject.transform.position).normalized;

            currentObject.transform.position = currentObject.transform.position + (direction * objMoveSpeed * Time.deltaTime);
            isHoldingObj = true;
            currentObject.transform.SetParent(objectHolderTform);
            currentObject.rb.useGravity = false;
           

        }
        else
        {

            isHoldingObj = true;
            currentObject.transform.SetParent(objectHolderTform);
            currentObject.rb.useGravity = false;

        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitLocation, interactionRadius);
    }
}
