using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement and rotation")]
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [Tooltip("When the players movement direction magnitude is above this value - we toggle 'IsMoving' on our animator")]
    [SerializeField] float isMovingThreshold;

    [Header("Components")]
    [SerializeField] Transform orientation;
    [SerializeField] PlayerAnimController playerAnimator;

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

        //Debug.Log(moveDirection.magnitude * moveSpeed);
        bool isMoving = moveDirection.magnitude * moveSpeed > isMovingThreshold ? true : false;

        if (playerAnimator != null) playerAnimator.SetAnimatorBool("isMoving", isMoving);

        rb.MovePosition(transform.position + (moveDirection.normalized * moveSpeed * Time.fixedDeltaTime));

        transform.rotation = Quaternion.Lerp(transform.rotation, orientation.rotation, rotationSpeed * Time.deltaTime);
        //transform.rotation = orientation.rotation;
    }

}
