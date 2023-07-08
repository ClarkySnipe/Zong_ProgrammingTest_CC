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


    public GameObject MainMenuUI;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenuUI.SetActive(true);
        }
    }

    public void UpdateSoundSettings(float masterVol, float musicVol, float sfxVol)
    {
        Debug.Log("Sound settings = " + masterVol + musicVol + sfxVol);
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
