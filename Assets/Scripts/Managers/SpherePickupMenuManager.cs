using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpherePickupMenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;


    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        scoreText.text = "Score : " + GameManager.Instance.Score;

    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
