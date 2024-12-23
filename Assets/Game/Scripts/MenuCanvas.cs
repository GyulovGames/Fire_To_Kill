using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private CanvasGroup loadingMenuGroup;
    [Space(10)]
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Sprite soundsONSprite;
    [SerializeField] private Sprite soundsOFFSprite;
    [Space(10)]
    [SerializeField] private Button[] levelButtons;


    private void Start()
    {
        SetSoundsSettings();
        SetCompletedLevels();
        RemoveLoadingMenu();

        YG2.InterstitialAdvShow();
    }

    private void SetSoundsSettings()
    {
        bool sounds = YG2.saves.soundsSettings;

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
        int levels = YG2.saves.completedLevels;

        for (int i = 0; i < levels; i++)
        {
            levelButtons[i].interactable = true;
            Image buttonImage = levelButtons[i].gameObject.GetComponent<Image>();
            buttonImage.color = Color.white;
        }
    }
    private void RemoveLoadingMenu()
    {
        fadeController.Disappear(loadingMenuGroup);
        fadeController.Appear(mainMenuGroup);
    }
    private IEnumerator DelayLoadScene(int sceneIndex)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }


    public void Btn_Play()
    {
        buttonsSFXPlayer.Play();

        fadeController.Disappear(mainMenuGroup);
        fadeController.Appear(levelsMenuGroup);
    }
    public void Btn_CloseLevels()
    {
        buttonsSFXPlayer.Play();

        fadeController.Disappear(levelsMenuGroup);
        fadeController.Appear(mainMenuGroup);
    }
    public void Btn_Games()
    {
        buttonsSFXPlayer.Play();

        fadeController.Disappear(mainMenuGroup);
        fadeController.Appear(gamesMenuGroup);
    }
    public void Btn_CloseGames()
    {
        buttonsSFXPlayer.Play();

        fadeController.Disappear(gamesMenuGroup);
        fadeController.Appear(mainMenuGroup);
    }
    public void Btn_Sounds()
    {
        bool sounds = YG2.saves.soundsSettings;

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
    public void Btn_LoadLevel(int levelIndex)
    {
        buttonsSFXPlayer.Play();
        fadeController.Appear(loadingMenuGroup);
        StartCoroutine(DelayLoadScene(levelIndex));
    }
}