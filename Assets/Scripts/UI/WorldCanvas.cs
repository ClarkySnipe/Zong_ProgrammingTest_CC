using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class WorldCanvas : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Transform playerTForm;
    [SerializeField] Image backGroundPanel;
    [SerializeField] Image maskingPanel;
    [SerializeField] TextMeshProUGUI textMesh;

    [SerializeField] private Canvas Canvas;

    [Header("Rotation and Appearence Values")]
    [SerializeField] float rotationSpeed;
    [Tooltip("This value, when a player is within this distance, our canvas will become active")]
    [SerializeField] float distToPlayerThreshold = 10;
    [SerializeField] float alphaValue = 0;
    [SerializeField] float alphaIncreaseValue = 0;
    [SerializeField] float fillValue = 0;
    [SerializeField] float fillRate = 0;
    [SerializeField] float lifeTime = 10;
    [SerializeField] float timeAtSpawn = 10;
    [SerializeField] bool useLifeTime = false;



    private bool isActive = false; //if a player comes within a certain distance, we activate this canvas

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        timeAtSpawn = Time.time;

        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            HandelUIFading(CheckDistance());

            //we want our gamecanvas to be looking at the player when its active
            Quaternion targetRotation = Quaternion.LookRotation(playerTForm.position - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Time.time > timeAtSpawn + lifeTime && useLifeTime)
            {
                gameObject.SetActive(false);
                isActive = false;
            }
        }
    }

    private bool CheckDistance()
    {
        //Debug.Log("Player distance = " + Vector3.Distance(transform.position, playerTForm.position));

        return (Vector3.Distance(transform.position, playerTForm.position) < distToPlayerThreshold);
    }

    private void HandelUIFading(bool playerInDist)
    {
        if (playerInDist) //While the player is in range -
        {
            //Our background Alpha needs to increase from 0 - 1
            //our Texts alpha needs to increase from 0 - 1
            alphaValue += Time.deltaTime * alphaIncreaseValue;

            //our maskingPanels image fill amount needs to lerp down to 0
            fillValue -= Time.deltaTime * fillRate;
        }
        else
        {
            alphaValue -= Time.deltaTime * alphaIncreaseValue;

            //our maskingPanels image fill amount needs to lerp down to 0
            fillValue += Time.deltaTime * fillRate;
        }

        alphaValue = Mathf.Clamp01(alphaValue);
        fillValue = Mathf.Clamp01(fillValue);


        //background and maskingPanel alpha
        backGroundPanel.CrossFadeAlpha(alphaValue, Time.deltaTime, true);
        maskingPanel.CrossFadeAlpha(alphaValue * 10, Time.deltaTime, true);

        //TMP alpha
        textMesh.CrossFadeAlpha(alphaValue, Time.deltaTime, true);

        //remove our fill on maskingPanel
        maskingPanel.fillAmount = fillValue;

    }
}
