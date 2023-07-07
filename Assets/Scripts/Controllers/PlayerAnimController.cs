using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private bool isMousePressed = false;

    [Header("Weight thresholds")]
    [SerializeField] float minLeftHandWeight; //the minimum wieght we can have on our Chain IK constraint
    [SerializeField] float maxLeftHandWeight; //the max weight we can have on our Chain IK constraint
    [SerializeField] float minRightHandWeight;
    [SerializeField] float maxRighttHandWeight;
    [SerializeField] float fingerMoveSpeed; 

    [HideInInspector]
    [SerializeField] private float weightValueLeft;
    [HideInInspector]
    [SerializeField] private float weightValueRight;


    #region Animation Components
    [Header("IK Constraint Components")]
    [SerializeField] ChainIKConstraint leftMiddleFinger;
    [SerializeField] ChainIKConstraint leftIndxFinger;
    [SerializeField] ChainIKConstraint leftThumbFinger;

    [SerializeField] ChainIKConstraint rightMiddleFinger;
    [SerializeField] ChainIKConstraint rightIndxFinger;
    [SerializeField] ChainIKConstraint rightThumbFinger;

    [Header("Animator Component")]
    [SerializeField] Animator animator;
    #endregion


    private void Awake()
    {
        //Variable initialization
        isMousePressed = false;
        weightValueRight = minRightHandWeight;
        weightValueLeft = minLeftHandWeight;
        fingerMoveSpeed = 2.0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) //left click
            weightValueLeft += (fingerMoveSpeed * Time.deltaTime);
        else
            weightValueLeft -= (fingerMoveSpeed * Time.deltaTime);

        if (Input.GetMouseButton(1))
            weightValueRight += (fingerMoveSpeed * Time.deltaTime);
        else
            weightValueRight -= (fingerMoveSpeed * Time.deltaTime);

        bool isAttacking = weightValueLeft > minLeftHandWeight || weightValueRight > minRightHandWeight; //if our weight value is above our minimum threshold - we are attackin.

        SetAnimatorBool("isAttacking", isAttacking);

        weightValueLeft = Mathf.Clamp(weightValueLeft, minLeftHandWeight, maxLeftHandWeight);
        weightValueRight = Mathf.Clamp(weightValueRight, minRightHandWeight, maxRighttHandWeight);

        leftMiddleFinger.weight = weightValueLeft;
        leftIndxFinger.weight = weightValueLeft;
        leftThumbFinger.weight = weightValueLeft;

        rightMiddleFinger.weight = weightValueRight;
        rightIndxFinger.weight = weightValueRight;
        rightThumbFinger.weight = weightValueRight;
    }

    public void SetAnimatorBool(string parameterName, bool value)
    {
        if (!animator) return;

        animator.SetBool(parameterName, value);
    }
}
