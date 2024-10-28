using UnityEngine;

public class PlayableSpriteManager : MonoBehaviour
{
    private static PlayableSpriteManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
