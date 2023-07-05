using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    [SerializeField] Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    Vector3 moveDirection = Vector3.zero;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //rb.AddForce(moveDirection.normalized * (moveSpeed * 100) * Time.deltaTime, ForceMode.Force);

        rb.position = rb.position + (moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, orientation.rotation, rotationSpeed * Time.deltaTime);
        //transform.rotation = orientation.rotation;
    }
}
