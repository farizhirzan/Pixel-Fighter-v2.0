using UnityEngine;
using UnityEngine.UI;

public class AttackSceneManager : MonoBehaviour
{
    public Image attackSprite;
    public Sprite redAttackSprite;
    public Sprite greenAttackSprite;
    public Sprite blueAttackSprite;
    private Animator attackAnimator;

    void Start()
    {
        string SpriteSelectedAttack = PlayerPrefs.GetString("SpriteSelectedAttack", "RedAttack");
        attackAnimator = attackSprite.GetComponent<Animator>();

        switch (SpriteSelectedAttack)
        {
            case "RedAttack":
                attackSprite.sprite = redAttackSprite;
                attackAnimator.Play("RedAttackAnimation");
                break;
            case "GreenAttack":
                attackSprite.sprite = greenAttackSprite;
                attackAnimator.Play("GreenAttackAnimation");
                break;
            case "BlueAttack":
                attackSprite.sprite = blueAttackSprite;
                attackAnimator.Play("BlueAttackAnimation");
                break;
            default:
                Debug.LogWarning("Unknown attack type: " + SpriteSelectedAttack);
                break;
        }
        PlayerPrefs.SetString("SpriteSelectedAttack", SpriteSelectedAttack);
        PlayerPrefs.Save();
    }
}
