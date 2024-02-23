using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int lastLevelIndex;

    private GameObject[] enemyesPool;
    private int enemyesNumber;

    private void Start()
    {
        enemyesPool = GameObject.FindGameObjectsWithTag("Bacteria");
        
        for(int i = 0; i < enemyesPool.Length; i++)
        {
            enemyesNumber++;
        }
    }

    public void Kills()
    {
        enemyesNumber--;

        if(enemyesNumber <= 0)
        {
            Invoke("CallCanvas", 1.2f);
        }
    }

    private void CallCanvas()
    {
        GameObject mainCanvasObject = GameObject.FindGameObjectWithTag("MainCanvas");
        MainCanvas mainCanvasScript = mainCanvasObject.GetComponent<MainCanvas>();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int completedLevels = YandexGame.savesData.savesCompletedLevels;

        if(currentSceneIndex >= lastLevelIndex)
        {
            YandexGame.savesData.savesCompletedLevels = lastLevelIndex;
            YandexGame.SaveProgress();
            mainCanvasScript.OpenTheEndMenu();
        }
        else if (currentSceneIndex >= completedLevels)
        {
            YandexGame.savesData.savesCompletedLevels = currentSceneIndex + 1;
            YandexGame.SaveProgress();
            mainCanvasScript.OpenSuccessMenu();
        }
        else if(currentSceneIndex < completedLevels)
        {
            mainCanvasScript.OpenSuccessMenu();
        }
    }
}