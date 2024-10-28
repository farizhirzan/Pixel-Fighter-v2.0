using UnityEngine;
using UnityEngine.SceneManagement;

public class FighterColorInitializer : MonoBehaviour
{
    public Animator characterAnimator;

    void Start()
    {
        if (characterAnimator == null)
        {
            characterAnimator = GetComponent<Animator>();
            if (characterAnimator == null)
            {
                Debug.LogError("Character Animator is not assigned.");
                return;
            }
        }

        characterAnimator.Play("BaseIdle", 0, 0);

        SetFighterColorBasedOnPreviousScene();
    }

    private void SetFighterColorBasedOnPreviousScene()
    {
        string previousScene = PlayerPrefs.GetString("PreviousScene");

        switch (previousScene)
        {
            case "04A_ScanRedPanda":
                PlayableSpriteController.FighterColour = 1;
                PlayableSpriteController.RValue = 50;
                PlayableSpriteController.GValue = 0;
                PlayableSpriteController.BValue = 0;
                break;

            case "04B_ScanPyroMonkey":
                PlayableSpriteController.FighterColour = 2;
                PlayableSpriteController.RValue = 25;
                PlayableSpriteController.GValue = 15;
                PlayableSpriteController.BValue = 10;
                break;

            case "04C_ScanTaiyaki":
                PlayableSpriteController.FighterColour = 3;
                PlayableSpriteController.RValue = 0;
                PlayableSpriteController.GValue = 25;
                PlayableSpriteController.BValue = 25;
                break;

            case "04D_ScanUFOCat":
                PlayableSpriteController.FighterColour = 4;
                PlayableSpriteController.RValue = 0;
                PlayableSpriteController.GValue = 50;
                PlayableSpriteController.BValue = 0;
                break;

            case "04E_ScanHydroPhantasm":
                PlayableSpriteController.FighterColour = 5;
                PlayableSpriteController.RValue = 0;
                PlayableSpriteController.GValue = 0;
                PlayableSpriteController.BValue = 50;
                break;

            case "04F_ScanPurpleFighter":
                PlayableSpriteController.FighterColour = 6;
                PlayableSpriteController.RValue = 25;
                PlayableSpriteController.GValue = 0;
                PlayableSpriteController.BValue = 25;
                break;

            case "04G_ScanPlusPlayer":
                PlayableSpriteController.FighterColour = 7;
                PlayableSpriteController.RValue = 10;
                PlayableSpriteController.GValue = 25;
                PlayableSpriteController.BValue = 15;
                break;

            default:
                Debug.LogWarning("Previous scene not recognized. Using default color values.");
                break;
        }

        Debug.Log($"Fighter Colour: {PlayableSpriteController.FighterColour}, R: {PlayableSpriteController.RValue}, G: {PlayableSpriteController.GValue}, B: {PlayableSpriteController.BValue}");

        characterAnimator.SetInteger("FighterColour", PlayableSpriteController.FighterColour);
    }
}
