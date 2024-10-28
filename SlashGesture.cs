using UnityEngine;
using UnityEngine.SceneManagement;

public class SlashGesture : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip slashSound;
    public AudioClip cooldownSound;
    public AudioClip winSound;

    public float slashThreshold = 3.0f;
    public float cooldownTime = 0.7f;
    private Vector3 lastAcceleration;
    private float lastSlashTime;
    private bool hasPlayedWinSound = false;
    private bool hasPlayedCooldownSound = false;

    private int slashCount = 0;
    public int maxSlashes = 10;

    private bool isSlashDisabled = false;

    void Start()
    {
        lastAcceleration = Input.acceleration;
        lastSlashTime = Time.time;

        if (audioSource == null || slashSound == null || animator == null)
        {
            Debug.LogError("Missing required components. Please check assignments.");
        }

        slashCount = 0;
        hasPlayedWinSound = false;
        isSlashDisabled = false;
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool isInCooldownSlashState = stateInfo.IsName("CooldownSlash");
        bool isInWinState = stateInfo.IsName("Win");

        if (isInCooldownSlashState)
        {
            if (!hasPlayedCooldownSound)
            {
                hasPlayedCooldownSound = true;
            }
        }
        else
        {
            hasPlayedCooldownSound = false;
        }

        if (isInWinState && !hasPlayedWinSound)
        {
            ResetSlashCount();
            PlayWinSound();
            hasPlayedWinSound = true;
            isSlashDisabled = true;
        }

        if (!isSlashDisabled)
        {
            Vector3 currentAcceleration = Input.acceleration;
            Vector3 deltaAcceleration = currentAcceleration - lastAcceleration;
            float deltaMagnitude = deltaAcceleration.magnitude;

            if (deltaMagnitude > slashThreshold && Time.time > lastSlashTime + cooldownTime)
            {
                TriggerSlash();
                PlaySlashSound();
                lastSlashTime = Time.time;
            }

            lastAcceleration = currentAcceleration;
        }
    }

    void TriggerSlash()
    {
        animator.SetTrigger("Slash");

        slashCount++;
        Debug.Log("Slash Count: " + slashCount);

        if (slashCount >= maxSlashes)
        {
            Debug.Log("Max slashes reached. Triggering 'Sleep' animation.");
            animator.SetTrigger("Sleep");
            isSlashDisabled = true;
        }
    }

    void ResetSlashCount()
    {
        slashCount = 0;
        isSlashDisabled = false;
        Debug.Log("Slash count reset.");
    }

    void PlaySlashSound()
    {
        if (audioSource != null && slashSound != null)
        {
            audioSource.PlayOneShot(slashSound);
        }
    }

    void PlayCooldownSound()
    {
        if (audioSource != null && cooldownSound != null)
        {
            audioSource.PlayOneShot(cooldownSound);
        }
    }

    void PlayWinSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.loop = false;
            audioSource.PlayOneShot(winSound);
        }
    }
}
