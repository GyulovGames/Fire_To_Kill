using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private AudioSource buttonsSFXPlayer;
    [SerializeField] private FadeController fadeController;
    [Space(10)]
    [SerializeField] private CanvasGroup mainMenuGroup;
    [SerializeField] private CanvasGroup levelsMenuGroup;
    [SerializeField] private CanvasGroup gamesMenuGroup;
    [SerializeField] private CanvasGroup LoadingMenuGroup;
    [Space(10)]
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Sprite soundsONSprite;
    [SerializeField] private Sprite soundsOFFSprite;


    private bool sounds = true;
    private int levels = 1;


    private void Start()
    {
        GetSavedData();
        SetSoundsSettings();
        SetCompletedLevels();
        RemoveLoadingMenu();
    }


    private void GetSavedData()
    {
        sounds = YG2.saves.soundsSettings;
        levels = YG2.saves.completedLevels;
    }
    private void SetSoundsSettings()
    {
        if (sounds)
        {
            soundsButtonImage.sprite = soundsONSprite;
            buttonsSFXPlayer.volume = 1f;
            sounds = true;
        }
        else
        {
            soundsButtonImage.sprite = soundsOFFSprite;
            buttonsSFXPlayer.volume = 0f;
            sounds = false;
        }
    }
    private void SetCompletedLevels()
    {

    }
    private void RemoveLoadingMenu()
    {
        fadeController.Disappear(LoadingMenuGroup);
        fadeController.Appear(mainMenuGroup);
    }


    public void Btn_OpenLevelsMenu()
    {
        buttonsSFXPlayer.Play();

        fadeController.Disappear(mainMenuGroup);
        fadeController.Appear(levelsMenuGroup);
    }
    public void Btn_CloseLevelsMenu()
    {
        buttonsSFXPlayer.Play();

        fadeController.Disappear(levelsMenuGroup);
        fadeController.Appear(mainMenuGroup);
    }
    public void Btn_OpenGamesMenu()
    {
        buttonsSFXPlayer.Play();

        fadeController.Disappear(mainMenuGroup);
        fadeController.Appear(gamesMenuGroup);
    }
    public void Btn_CloseGamesMenu()
    {
        buttonsSFXPlayer.Play();

        fadeController.Disappear(gamesMenuGroup);
        fadeController.Appear(mainMenuGroup);
    }
    public void Btn_SoundsSwitching()
    {
        if (sounds)
        {
            soundsButtonImage.sprite = soundsOFFSprite;
            buttonsSFXPlayer.volume = 0f;
            sounds = false;
            YG2.saves.soundsSettings = false;
        }
        else
        {
            soundsButtonImage.sprite = soundsONSprite;
            buttonsSFXPlayer.volume = 1f;
            sounds = true;
            buttonsSFXPlayer.Play();
            YG2.saves.soundsSettings = true;
        }

        YG2.SaveProgress();
    }
}