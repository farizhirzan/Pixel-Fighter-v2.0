using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class AttackOutcomeManager : MonoBehaviour
{
    public Image playerAttackSprite;
    public Image bossAttackSprite;

    public Sprite redAttackSprite;
    public Sprite greenAttackSprite;
    public Sprite blueAttackSprite;

    public Animator playerAnimator;
    public Animator bossAnimator;

    void Start()
    {
        playerAnimator = playerAttackSprite.GetComponent<Animator>();
        bossAnimator = bossAttackSprite.GetComponent<Animator>();

        string playerAttack = PlayerPrefs.GetString("SpriteSelectedAttack", "RedAttack");
        string bossAttack = PlayerPrefs.GetString("BossSelectedAttack", "RedAttack");

        Debug.Log("Player Attack: " + playerAttack);
        Debug.Log("Boss Attack: " + bossAttack);

        switch (playerAttack)
        {
            case "RedAttack":
                playerAttackSprite.sprite = redAttackSprite;
                playerAnimator.Play("RedAttackAnimation");
                break;
            case "GreenAttack":
                playerAttackSprite.sprite = greenAttackSprite;
                playerAnimator.Play("GreenAttackAnimation");
                break;
            case "BlueAttack":
                playerAttackSprite.sprite = blueAttackSprite;
                playerAnimator.Play("BlueAttackAnimation");
                break;
            default:
                Debug.LogWarning("Unknown player attack type: " + playerAttack);
                break;
        }

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

        string outcome = DetermineOutcome(playerAttack, bossAttack);
        StartCoroutine(HandleOutcome(outcome));
    }

    string DetermineOutcome(string playerAttack, string bossAttack)
    {
        if (playerAttack == bossAttack)
        {
            int playerR = PlayableSpriteController.RValue;
            int playerG = PlayableSpriteController.GValue;
            int playerB = PlayableSpriteController.BValue;

            int bossR = BossController.BossRValue;
            int bossG = BossController.BossGValue;
            int bossB = BossController.BossBValue;

            if ((playerAttack == "RedAttack" && bossAttack == "RedAttack" && playerR == bossR) ||
                (playerAttack == "GreenAttack" && bossAttack == "GreenAttack" && playerG == bossG) ||
                (playerAttack == "BlueAttack" && bossAttack == "BlueAttack" && playerB == bossB))
            {
                return "Draw";
            }
            else if ((playerAttack == "RedAttack" && playerR > bossR) ||
                     (playerAttack == "GreenAttack" && playerG > bossG) ||
                     (playerAttack == "BlueAttack" && playerB > bossB))
            {
                return "PlayerWin";
            }
            else
            {
                return "BossWin";
            }
        }
        else
        {
            if ((playerAttack == "RedAttack" && bossAttack == "GreenAttack") ||
                (playerAttack == "GreenAttack" && bossAttack == "BlueAttack") ||
                (playerAttack == "BlueAttack" && bossAttack == "RedAttack"))
            {
                return "PlayerWin";
            }
            else
            {
                return "BossWin";
            }
        }
    }

    IEnumerator HandleOutcome(string outcome)
    {
        yield return new WaitForSeconds(1.75f);

        if (outcome == "PlayerWin")
        {
            bossAttackSprite.enabled = false;
            yield return new WaitForSeconds(3.1f);
            SceneManager.LoadScene("26A_BossHit");
        }
        else if (outcome == "BossWin")
        {
            playerAttackSprite.enabled = false;
            yield return new WaitForSeconds(3.1f);
            SceneManager.LoadScene("26B_PlayerHit");
        }
        else if (outcome == "Draw")
        {
            playerAttackSprite.enabled = false;
            bossAttackSprite.enabled = false;
            SceneManager.LoadScene("26C_Draw");
        }
    }
}
