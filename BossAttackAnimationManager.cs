using UnityEngine;

public class BossAttackAnimationManager : MonoBehaviour
{
    public Animator bossAnimator; // Attach the boss Animator here

    void Start()
    {
        PlayBossAttackAnimation();
    }

    void PlayBossAttackAnimation()
    {
        int bossNumber = BossController.CurrentBossNumber;

        switch (bossNumber)
        {
            case 1: // SplitGirl Attack
                bossAnimator.Play("SplitGirlAttack");
                break;
            case 2: // SwapKitty Attack
                bossAnimator.Play("SwapKittyAttack");
                break;
            case 3: // SmokeFace Attack
                bossAnimator.Play("SmokeFaceAttack");
                break;
            case 4: // GraffitiGorilla Attack
                bossAnimator.Play("GraffitiGorillaAttack");
                break;
            default:
                Debug.LogError("No attack animation for this boss");
                break;
        }
    }
}
