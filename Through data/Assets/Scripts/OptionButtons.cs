using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButtons : MonoBehaviour {

    public Sprite musicOn;
    public Sprite musicOff;
    public Button musicButton;

    public Sprite SFX_On;
    public Sprite SFX_Off;
    public Button sfxButton;
    public Scrollbar scrollbar;

    private bool sfx;
    private bool music;

    private void Start()
    {
        music = StaticVariables.music;
        sfx = StaticVariables.sfx;

        if (music)
        {
            musicButton.GetComponent<Image>().sprite = musicOn;
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = musicOff;
        }

        if (sfx)
        {
            sfxButton.GetComponent<Image>().sprite = SFX_On;
        }
        else
        {
            sfxButton.GetComponent<Image>().sprite = SFX_Off;
        }
    }

    public void back()
    {
        Application.LoadLevel(0);
    }

    public void muteMusic()
    {
        if (music)
        {
            music = false;
            musicButton.GetComponent<Image>().sprite = musicOff;
        }
        else
        {
            music = true;
            musicButton.GetComponent<Image>().sprite = musicOn;
        }

        StaticVariables.music = music;
    }

    public void muteSFX()
    {
        if (sfx)
        {
            sfx = false;
            sfxButton.GetComponent<Image>().sprite = SFX_Off;
        }
        else
        {
            sfx = true;
            sfxButton.GetComponent<Image>().sprite = SFX_On;
        }

        StaticVariables.sfx= sfx;
    }

    public void changedSensibility()
    {
        StaticVariables.mouseSensibility = scrollbar.value;
    }
}
