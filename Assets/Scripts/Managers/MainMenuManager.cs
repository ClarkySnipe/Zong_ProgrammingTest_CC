using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;

        if (AudioManager.AM_instance != null)
        {
            AudioManager.AM_instance.Play("MainMenu", 0);
        }

        MasterSlider.onValueChanged.AddListener(delegate { GameManager.Instance.UpdateSoundSettings(MasterSlider.value, MusicSlider.value, SFXSlider.value); });
        MusicSlider.onValueChanged.AddListener(delegate { GameManager.Instance.UpdateSoundSettings(MasterSlider.value, MusicSlider.value, SFXSlider.value); });
        SFXSlider.onValueChanged.AddListener(delegate { GameManager.Instance.UpdateSoundSettings(MasterSlider.value, MusicSlider.value, SFXSlider.value); });

    }
    private void OnEnable()
    {
        Time.timeScale = 0.0f;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
