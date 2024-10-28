using UnityEngine;
using UnityEngine.SceneManagement;

public class PunchAnimation : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip chargeSound;
    public AudioClip punchSound;
    public AudioClip cooldownSound;
    public AudioClip winSound;

    public float backwardThreshold = -0.2f;
    public float forwardThreshold = 2.0f;
    public float minPunchSpeed = 2.0f;
    public float cooldownTime = 0.1f;
    public float deadZone = 0.1f;
    public float idleThreshold = 0.08f;

    private bool isIdle = true;
    private bool isCharging = false;
    private float cooldownTimer = 0.0f;
    private Vector3 lastAcceleration;
    private int punchCount = 0;

    private bool hasPlayedWinSound = false;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        lastAcceleration = Input.acceleration;
        animator.SetBool("IsIdle", true);
    }

    void Update()
    {
        Vector3 currentAcceleration = Input.acceleration;

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        Vector3 velocity = currentAcceleration - lastAcceleration;

        if (Mathf.Abs(velocity.x) < deadZone)
        {
            velocity.x = 0;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool isInCooldownState = stateInfo.IsName("Cooldown");
        bool isInIdleState = stateInfo.IsName("Idle");
        bool isInWinState = stateInfo.IsName("Win");

        if (isInIdleState && isIdle && velocity.x < backwardThreshold && cooldownTimer <= 0)
        {
            isIdle = false;
            isCharging = true;
            animator.SetBool("IsCharging", true);
            PlayChargeSound();
        }

        if (isCharging && velocity.x > forwardThreshold && velocity.magnitude > minPunchSpeed && cooldownTimer <= 0)
        {
            isCharging = false;
            animator.SetBool("IsCharging", false);
            animator.SetBool("IsPunching", true);
            cooldownTimer = cooldownTime;
            PlayPunchSound();

            punchCount++;
            animator.SetInteger("PunchCount", punchCount);

            if (punchCount >= 2)
            {
                animator.SetInteger("PunchCount", 2);
                punchCount = 0;
            }
        }

        if (isInCooldownState && !isIdle)
        {
            if (!audioSource.isPlaying)
            {
                PlayCooldownSound();
            }
        }

        if (isInWinState && !hasPlayedWinSound)
        {
            PlayWinSound();
            hasPlayedWinSound = true;
        }

        lastAcceleration = currentAcceleration;
    }

    void PlayCooldownSound()
    {
        if (audioSource.isPlaying && audioSource.clip == winSound)
        {
            audioSource.Stop();
        }
        audioSource.PlayOneShot(cooldownSound);
        isIdle = true;
    }

    void PlayChargeSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.clip = chargeSound;
            audioSource.Play();
        }
    }

    void PlayPunchSound()
    {
        if (audioSource.isPlaying && audioSource.clip == chargeSound)
        {
            audioSource.Stop();
        }
        audioSource.loop = false;
        audioSource.PlayOneShot(punchSound);
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
