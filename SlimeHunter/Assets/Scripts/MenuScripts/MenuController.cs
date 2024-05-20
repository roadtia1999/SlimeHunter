using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public Toggle disableMuteToggle;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 144;
        int togglecheck = PlayerPrefs.GetInt("disableMute");
        if (togglecheck == 1)
        {
            disableMuteToggle.isOn = true;
        }
        else
        {
            disableMuteToggle.isOn = false;
        }

        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeSlider()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    public void DisableToggle()
    {
        if (disableMuteToggle.isOn)
        {
            PlayerPrefs.SetInt("disableMute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("disableMute", 0);
        }
    }

    public void StartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }

    public void SettingsButtonClicked()
    {
        settingsPanel.SetActive(true);
    }

    public void ReturnToMenu()
    {
        settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
