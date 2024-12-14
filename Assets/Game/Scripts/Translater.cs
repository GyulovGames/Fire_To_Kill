using UnityEngine;
using UnityEngine.UI;
//using //YG;

public class Translater : MonoBehaviour
{
    [SerializeField] private string ruTextVersion;
    [SerializeField] private string enTextVersion;

    private Text translateText;

    private void Start()
    {
        translateText = GetComponent<Text>();

        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            translateText.text = ruTextVersion.ToString();
        }
        else
        {
            translateText.text = enTextVersion.ToString();
        }
    }
}