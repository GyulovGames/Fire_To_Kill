using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using YG;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private AudioSource buttonsSFXPlayer;
    [SerializeField] private FadeController fadeController;
    [Space(10)]
    [SerializeField] private CanvasGroup gameMenuGroup;
    [SerializeField] private CanvasGroup pauseMenuGroup;
    [SerializeField] private CanvasGroup successMenuGroup;
    [SerializeField] private CanvasGroup loadingMenuGroup;

    private GameObject[] enemies;
    private int killedEnemyes;

    public static UnityEvent PauseEvent = new UnityEvent();
    public static UnityEvent ResumeGame = new UnityEvent();


    private void Start()
    {
        SetSoundsSettings();
        CollectAllEnemies();
        RemoveLoadingMenu();

        Bacteria.EnemyKill.AddListener(KilledEnemyesCounter);
    }


    private void SetSoundsSettings()
    {
        bool sounds = YG2.saves.soundsSettings;
        AudioSource[] allAudio = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in allAudio)
        {
            if (sounds)
            {
                audio.volume = 1f;
            }
            else
            {
                audio.volume = 0f;
            }
        }
    }
    private void CollectAllEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Bacteria");
    }
    private void RemoveLoadingMenu()
    {
        fadeController.Disappear(loadingMenuGroup);
        fadeController.Appear(gameMenuGroup);
    }
    private IEnumerator DelayLoadScene(int sceneIndex)
    {
        Bacteria.EnemyKill.RemoveListener(KilledEnemyesCounter);

        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(sceneIndex);
    }
    private void KilledEnemyesCounter()
    {
        killedEnemyes++;

        if(killedEnemyes >= enemies.Length)
        {
            SaveCompletedLevels();
            Invoke(nameof(DelayBeforeSuccess), 1.3f);
            PauseEvent.Invoke();
        }
    }
    private void SaveCompletedLevels()
    {
        int levels = YG2.saves.completedLevels;

        if(levels == 122)
        {
            YG2.saves.completedLevels = 122;
        }
        else if(levels != 122)
        {
            YG2.saves.completedLevels = SceneManager.GetActiveScene().buildIndex + 1;
        }

        YG2.SaveProgress();
    }
    private void DelayBeforeSuccess()
    {
        fadeController.Appear(successMenuGroup);
    }


    public void Btn_Pause()
    {
        buttonsSFXPlayer.Play();
        PauseEvent.Invoke();
        fadeController.Disappear(gameMenuGroup);
        fadeController.Appear(pauseMenuGroup);
    }
    public void Btn_Resume()
    {
        buttonsSFXPlayer.Play();
        ResumeGame.Invoke();
        fadeController.Disappear(pauseMenuGroup);
        fadeController.Appear(gameMenuGroup);
    }
    public void Btn_Restart()
    {
        buttonsSFXPlayer.Play();
        fadeController.Appear(loadingMenuGroup);
        int scene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(DelayLoadScene(scene));
    }
    public void Btn_NextLevel()
    {
        buttonsSFXPlayer.Play();
        fadeController.Appear(loadingMenuGroup);
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if(currentScene == 122)
        {
            int randomScene = Random.Range(1, 122);
            StartCoroutine(DelayLoadScene(randomScene));
        }
        else
        {
            StartCoroutine(DelayLoadScene(currentScene + 1));
        }
    }
    public void Btn_Home()
    {
        buttonsSFXPlayer.Play();
        fadeController.Appear(loadingMenuGroup);
        StartCoroutine(DelayLoadScene(0));
    }
}