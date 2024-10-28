using UnityEngine;
using UnityEngine.UI;

public class ScriptForPlayableSpriteBValue : MonoBehaviour
{
    public Text ShowingBValue;
    private const int MaxBValue = 999;

    void Start()
    {
        UpdateBValueText();
    }

    public void AddToBValue(int additionalValue)
    {
        PlayableSpriteController.BValue = Mathf.Min(PlayableSpriteController.BValue + additionalValue, MaxBValue);
        Debug.Log("BValue after adding: " + PlayableSpriteController.BValue);

        UpdateBValueText();
    }

    public void UpdateBValueText()
    {
        ShowingBValue.text = PlayableSpriteController.BValue.ToString();
    }
}
