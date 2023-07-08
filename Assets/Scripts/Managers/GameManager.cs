using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float MasterVolume = 1;
    public float MusicVolume = 1;
    public float SFXVolume = 1;

    public int ChosenSphereType = 0;
    public int Score;

    public GameObject MainMenuUI;
    public GameObject SpherePickupCanvas;

    public GameSphere gameSphere;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        Score = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenuUI.SetActive(true);
        }
    }

    public void AddScore(int score)
    {
        score += score;
    }

    public void OnSpherePickup()
    {
        Debug.Log("Setting sphere pickup panel to active");
        SpherePickupCanvas.SetActive(true);
    }

    public void SetSphereType()
    {

    }

    public void OnWeaponButtonPress(int sphereType)
    {
        //ChosenSphereType = sphereType;

        gameSphere.SelectSphereType(sphereType);
    }


    public void UpdateSoundSettings(float masterVol, float musicVol, float sfxVol)
    {
        Debug.Log("Sound settings = " + "master " + masterVol + "music " + musicVol + "sfx " + sfxVol);
        MasterVolume = masterVol;
        MusicVolume = musicVol;
        SFXVolume = sfxVol;

        AudioManager.AM_instance.SetSoundVolumes();
    }

    public void OnPlayButtonPress()
    {
        //disable game canvas and play audio
        MainMenuUI.SetActive(false);

        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void OnQuitButtonPress()
    {
        Application.Quit();
    }



}
