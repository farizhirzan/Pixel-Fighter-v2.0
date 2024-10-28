using UnityEngine;

public class BossHitAnimationManager : MonoBehaviour
{
    public Animator bossAnimator; // Attach the boss Animator here

    void Start()
    {
        PlayBossHitAnimation();
    }

    void PlayBossHitAnimation()
    {
        int bossNumber = BossController.CurrentBossNumber;

        switch (bossNumber)
        {
            case 1: // SplitGirl Hit
                bossAnimator.Play("SplitGirlHit");
                break;
            case 2: // SwapKitty Hit
                bossAnimator.Play("SwapKittyHit");
                break;
            case 3: // SmokeFace Hit
                bossAnimator.Play("SmokeFaceHit");
                break;
            case 4: // GraffitiGorilla Hit
                bossAnimator.Play("GraffitiGorillaHit");
                break;
            default:
                Debug.LogError("No hit animation for this boss");
                break;
        }
    }
}
