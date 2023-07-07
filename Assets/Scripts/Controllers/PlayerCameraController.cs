using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] float sensX;
    [SerializeField] float sensY;
    [SerializeField] float minXRot;
    [SerializeField] float mmaxXRot;

    [SerializeField] Transform orientation;
    [SerializeField] Transform position;

    [HideInInspector]
    [SerializeField] float xRotation;
    [HideInInspector]
    [SerializeField] float yRotation;


    private Camera mainCamera;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, minXRot, mmaxXRot);

        transform.position = position.position;
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        orientation.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
