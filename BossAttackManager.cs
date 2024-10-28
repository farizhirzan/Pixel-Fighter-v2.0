using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAttackManager : MonoBehaviour
{
    public Image attackSprite;
    public Sprite redAttackSprite;
    public Sprite greenAttackSprite;
    public Sprite blueAttackSprite;
    private Animator attackAnimator;

    void Start()
    {
        attackAnimator = attackSprite.GetComponent<Animator>();

        PerformRandomAttack();
    }

    void PerformRandomAttack()
    {
        int randomBossAttack = Random.Range(0, 3);
        string bossAttackType = "";

        switch (randomBossAttack)
        {
            case 0:
                attackSprite.sprite = redAttackSprite;
                attackAnimator.Play("RedAttackAnimation");
                bossAttackType = "RedAttack";
                break;
            case 1:
                attackSprite.sprite = greenAttackSprite;
                attackAnimator.Play("GreenAttackAnimation");
                bossAttackType = "GreenAttack";
                break;
            case 2:
                attackSprite.sprite = blueAttackSprite;
                attackAnimator.Play("BlueAttackAnimation");
                bossAttackType = "BlueAttack";
                break;
            default:
                Debug.LogWarning("Unknown attack type: " + randomBossAttack);
                break;
        }
        PlayerPrefs.SetString("BossSelectedAttack", bossAttackType);
        PlayerPrefs.Save();
    }

}
