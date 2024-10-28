using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptForPlayableSpriteEXP : MonoBehaviour
{
    public Text ShowingEXP;
    private const int MaxEXP = 9999;

    void Start()
    {
        UpdateEXPText();
    }

    public void SetEXP(int newEXP)
    {
        PlayableSpriteController.EXP = Mathf.Min(newEXP, MaxEXP);
        UpdateEXPText();
    }

    void UpdateEXPText()
    {
        ShowingEXP.text = PlayableSpriteController.EXP.ToString();
    }
}
