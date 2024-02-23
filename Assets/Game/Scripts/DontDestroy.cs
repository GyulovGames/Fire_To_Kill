using UnityEngine;
using YG;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy dontDestroy;

    private void Start()
    {
        DontDestroyOnLoad(this);

        if (dontDestroy == null)
        {
            dontDestroy = this;
        }
        else
        {
            Object.Destroy(gameObject);
        }
    }
}
