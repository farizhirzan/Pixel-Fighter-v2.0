using UnityEngine;
using UnityEngine.UI;

public class BossAttackUpdater : MonoBehaviour
{
    public Image bossAttackSprite;

    public Sprite redAttackSprite;
    public Sprite greenAttackSprite;
    public Sprite blueAttackSprite;

    public Animator bossAnimator;

    void Start()
    {
        bossAnimator = bossAttackSprite.GetComponent<Animator>();

        string bossAttack = PlayerPrefs.GetString("BossSelectedAttack", "RedAttack");
        Debug.Log("Boss Attack: " + bossAttack);

        switch (bossAttack)
        {
            case "RedAttack":
                bossAttackSprite.sprite = redAttackSprite;
                bossAnimator.Play("RedAttackAnimation");
                break;
            case "GreenAttack":
                bossAttackSprite.sprite = greenAttackSprite;
                bossAnimator.Play("GreenAttackAnimation");
                break;
            case "BlueAttack":
                bossAttackSprite.sprite = blueAttackSprite;
                bossAnimator.Play("BlueAttackAnimation");
                break;
            default:
                Debug.LogWarning("Unknown boss attack type: " + bossAttack);
                break;
        }
    }
}
