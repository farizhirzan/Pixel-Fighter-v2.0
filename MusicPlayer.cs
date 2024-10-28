using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        AudioSource audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying && IsAllowedScene(SceneManager.GetActiveScene().name))
        {
            audioSource.Play();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (IsAllowedScene(scene.name))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private bool IsAllowedScene(string sceneName)
    {
        return sceneName == "01_Tutorial" || sceneName == "02_MapTutorial" || sceneName == "26A_BossHit" || sceneName == "26B_PlayerHit";
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
