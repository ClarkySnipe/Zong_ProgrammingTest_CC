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
    [SerializeField] float dropDelay = 1;
    private float timeAtPickup;



    private Vector3 rayOrigin; //our screen center point

    private void Start()
    {
        rayOrigin = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        timeAtPickup = 0;
    }

    private void Update()
    {
        if (Utilities.IsRightClicking()) //if we are right clicking
        {
            if (isHoldingObj && Time.time > timeAtPickup + dropDelay) //if we are holding an object and we right click - we want to drop it
            {
                Debug.Log("we are right clicking and holding an obj - time to drop");

                currentObject.OnSphereDrop();
                currentObject = null;

                isHoldingObj = false;

                return;
            }
        }

        if (isHoldingObj) //while we are holding make sure our sphere doesnt run away on us
        {
            currentObject.transform.position = objectHolderTform.position;
            return;
        }

        if (Utilities.IsRightClicking()) //if we make it here - we do not have an object - but we are searching for one, begin our interaction logic
        {
            hitLocation = PerformRayCast();

            if (hitLocation == Vector3.zero) return;

            PerformOverlapSphere();

            if (!currentObject) return;

            if (!isHoldingObj)
                PerformObjectMoveTo();
            else
                currentObject.transform.position = objectHolderTform.position;
        }
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
        }
        else
        {
            isHoldingObj = true;
            timeAtPickup = Time.time;

            currentObject.transform.SetParent(objectHolderTform);
            currentObject.rb.useGravity = false;
            currentObject.OnSpherePickup();

        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitLocation, interactionRadius);
    }
}
