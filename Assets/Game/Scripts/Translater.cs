using UnityEngine;
using UnityEngine.UI;
using YG;

public class Translater : MonoBehaviour
{
    [SerializeField] private string ruTextVersion;
    [SerializeField] private string enTextVersion;

    private void Start()
    {
        Text thisText = GetComponent<Text>();
        string language = YG2.lang;

        if(language == "ru")
        {
            thisText.text = ruTextVersion.ToString();
        }
        else if(language == "en")
        {
            thisText.text = enTextVersion.ToString();
        }
        else
        {
            thisText.text = enTextVersion.ToString();
        }
    }
}