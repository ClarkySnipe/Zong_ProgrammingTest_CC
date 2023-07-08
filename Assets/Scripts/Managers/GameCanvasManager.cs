using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + GameManager.Instance.Score;
    }
}
