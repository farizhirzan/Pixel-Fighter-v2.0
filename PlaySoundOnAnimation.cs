using UnityEngine;

public class PlaySoundOnAnimation : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip introMusic;
    public AudioClip startSound;                             

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned.");
        }
    }

    public void PlayIntroMusic()
    {
        audioSource.loop = true;
        audioSource.clip = introMusic;
        audioSource.Play();
    }

    public void PlayStartSound()
    {
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer != null)
        {
            Destroy(musicPlayer.gameObject);
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        
        audioSource.PlayOneShot(startSound);
    }
}
