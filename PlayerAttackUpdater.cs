using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackUpdater : MonoBehaviour
{
    public Image playerAttackSprite;

    public Sprite redAttackSprite;
    public Sprite greenAttackSprite;
    public Sprite blueAttackSprite;

    public Animator playerAnimator;

    void Start()
    {
        playerAnimator = playerAttackSprite.GetComponent<Animator>();

        string playerAttack = PlayerPrefs.GetString("SpriteSelectedAttack", "RedAttack");
        Debug.Log("Player Attack: " + playerAttack);

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
    }
}
