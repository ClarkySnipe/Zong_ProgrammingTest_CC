using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private bool isMousePressed = false;


    [SerializeField] float minLeftHandWeight; //the minimum wieght we can have on our Chain IK constraint
    [SerializeField] float maxLeftHandWeight; //the max weight we can have on our Chain IK constraint
    [SerializeField] float fingerMoveSpeed; //the max weight we can have on our Chain IK constraint

    [SerializeField] private float weightValue;


    #region Animation Components
    [SerializeField] ChainIKConstraint leftMiddleFinger;
    [SerializeField] ChainIKConstraint leftIndxFinger;
    [SerializeField] ChainIKConstraint leftThumbFinger;
    #endregion


    private void Awake()
    {
        isMousePressed = false;
        weightValue = 0.0f;
        fingerMoveSpeed = 2.0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) //left click
            weightValue += (fingerMoveSpeed * Time.deltaTime);
        else
            weightValue -= (fingerMoveSpeed * Time.deltaTime);

        weightValue = Mathf.Clamp(weightValue, minLeftHandWeight, maxLeftHandWeight);

        leftMiddleFinger.weight = weightValue;
        leftIndxFinger.weight = weightValue;
        leftThumbFinger.weight = weightValue;
    }
}
