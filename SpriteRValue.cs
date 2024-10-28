using UnityEngine;
using UnityEngine.UI;

public class ScriptForPlayableSpriteRValue : MonoBehaviour
{
    public Text ShowingRValue;
    private const int MaxRValue = 999;

    void Start()
    {
        UpdateRValueText();
    }

    public void AddToRValue(int additionalValue)
    {
        PlayableSpriteController.RValue = Mathf.Min(PlayableSpriteController.RValue + additionalValue, MaxRValue);
        Debug.Log("RValue after adding: " + PlayableSpriteController.RValue);

        UpdateRValueText();
    }

    public void UpdateRValueText()
    {
        ShowingRValue.text = PlayableSpriteController.RValue.ToString();
    }
}
