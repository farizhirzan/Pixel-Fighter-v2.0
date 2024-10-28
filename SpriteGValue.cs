using UnityEngine;
using UnityEngine.UI;

public class ScriptForPlayableSpriteGValue : MonoBehaviour
{
    public Text ShowingGValue;
    private const int MaxGValue = 999;

    void Start()
    {
        UpdateGValueText();
    }

    public void AddToGValue(int additionalValue)
    {
        PlayableSpriteController.GValue = Mathf.Min(PlayableSpriteController.GValue + additionalValue, MaxGValue);
        Debug.Log("GValue after adding: " + PlayableSpriteController.GValue);

        UpdateGValueText();
    }

    public void UpdateGValueText()
    {
        ShowingGValue.text = PlayableSpriteController.GValue.ToString();
    }
}
