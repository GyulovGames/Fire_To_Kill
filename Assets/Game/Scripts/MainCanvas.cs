using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainCanvas : MonoBehaviour
{
    private static MainCanvas mainCanvas;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject succesMenu;
    [SerializeField] private GameObject levelsMenu;
    [SerializeField] private GameObject allGamesMenu;
    [SerializeField] private GameObject gameEndMenu;
    [Space(30)]
    [SerializeField] private Sprite sounds_ON_Icon;
    [SerializeField] private Sprite sounds_OFF_Icon;
    [Space(30)]
    [SerializeField] private Image soundsButtonIcon;
    [Space(30)]
    [SerializeField] private GameObject[] levelsButtons;


    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Awake()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetData();
        }
    }

    public void GetData()
    {
        ActivateCompletedLevelsButtons();
        UpdateSoundsSettings();
    }

    private void ActivateCompletedLevelsButtons()
    {
        int completedLevels = YandexGame.savesData.savesCompletedLevels;

        for (int i = 0; i < completedLevels; i++)
        {
            levelsButtons[i].GetComponent<Image>().color = new Color32(255, 255, 213, 255);
            levelsButtons[i].GetComponent<Button>().interactable = true;
        }
    }

    private void UpdateSoundsSettings()
    {
        if (YandexGame.savesData.savesSoundsIsON)
        {
            AudioListener.volume = 1;
            soundsButtonIcon.sprite = sounds_ON_Icon;
        }
        else
        {
            AudioListener.volume = 0;
            soundsButtonIcon.sprite = sounds_OFF_Icon;
        }
    }

    public void btn_OffSounds()
    {
        if(AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
            soundsButtonIcon.sprite = sounds_OFF_Icon;
            YandexGame.savesData.savesSoundsIsON = false;
            YandexGame.SaveProgress();
        }
        else if(AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            soundsButtonIcon.sprite = sounds_ON_Icon;
            YandexGame.savesData.savesSoundsIsON = true;
            YandexGame.SaveProgress();
        }
    }

    public void btn_OpenLevelsMenu()
    {
        ActivateCompletedLevelsButtons();
        GetObjectComponent(mainMenu).SetTrigger("HideMenu");
        GetObjectComponent(levelsMenu).SetTrigger("ShowMenu");
    }

    public void btn_CloseLevelsMenu()
    {
        GetObjectComponent(levelsMenu).SetTrigger("HideMenu");
        GetObjectComponent(mainMenu).SetTrigger("ShowMenu");
    }

    public void OpenAllGamesMenu()
    {
        GetObjectComponent(allGamesMenu).SetTrigger("ShowMenu");
    }

    public void AllGames_NO()
    {
        GetObjectComponent(allGamesMenu).SetTrigger("HideMenu");
    }

    public void AllGames_YES()
    {
        GetObjectComponent(allGamesMenu).SetTrigger("HideMenu");
    }

    public void btn_LoadSelectedLevel(int levelIndex)
    {
        StartCoroutine(LoadLevelAsync(levelIndex, levelsMenu, gameMenu));
    }

    public void btn_PauseGame()
    {
        Time.timeScale = 0;
        GetObjectComponent(gameMenu).SetTrigger("HideMenu");
        GetObjectComponent(pauseMenu).SetTrigger("ShowMenu");
    }

    public void btn_ResumeGame()
    {
        Time.timeScale = 1;
        GetObjectComponent(pauseMenu).SetTrigger("HideMenu");
        GetObjectComponent(gameMenu).SetTrigger("ShowMenu");
    }

    public void btn_LoadNextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadLevelAsync(sceneIndex, succesMenu, gameMenu));
    }

    public void OpenSuccessMenu()
    {
        GetObjectComponent(gameMenu).SetTrigger("HideMenu");
        GetObjectComponent(succesMenu).SetTrigger("ShowMenu");
        Time.timeScale = 0;
    }

    public void OpenTheEndMenu()
    {
        GetObjectComponent(gameMenu).SetTrigger("HideMenu");
        GetObjectComponent(gameEndMenu).SetTrigger("ShowMenu");
        Time.timeScale = 0;
    }

    public void btn_RestartLevel(GameObject menu)
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        switch (menu.name)
        {
            case "PauseMenu":
                StartCoroutine(LoadLevelAsync(sceneIndex, pauseMenu, gameMenu));
                break;
            case "SuccessMenu":
                StartCoroutine(LoadLevelAsync(sceneIndex, succesMenu, gameMenu));
                break;
        }

        Time.timeScale = 1;
    }

    public void btn_BackToMainMenu(GameObject menu)
    {
        switch (menu.name)
        {
            case "PauseMenu":
                StartCoroutine(LoadLevelAsync(0, pauseMenu, mainMenu));
                break;
            case "SuccessMenu":
                StartCoroutine(LoadLevelAsync(0, succesMenu, mainMenu));
                break;
            case "GameEndMenu":
                StartCoroutine(LoadLevelAsync(0, gameEndMenu, mainMenu));
                break;
        }

        Time.timeScale = 1;
    }


    private Animator GetObjectComponent(GameObject menu)
    {
        Animator animator = menu.GetComponent<Animator>();
        return animator;
    }

    private IEnumerator LoadLevelAsync(int index, GameObject hideMenu, GameObject showMenu)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);

        while (!asyncLoad.isDone)
        {
            yield return null;
            Time.timeScale = 1;

            GetObjectComponent(hideMenu).SetTrigger("HideMenu");
            GetObjectComponent(showMenu).SetTrigger("ShowMenu");
        }
    }
}