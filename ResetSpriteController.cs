using UnityEngine;

public class ResetPlayableSpriteController : MonoBehaviour
{
    private void Start()
    {
        // Reset the static variables when returning to 00_StartScene
        PlayableSpriteController.EvoLevel = 0;
        PlayableSpriteController.RValue = 10;
        PlayableSpriteController.GValue = 25;
        PlayableSpriteController.BValue = 15;
        PlayableSpriteController.EXP = 0;
        PlayableSpriteController.StepCountValue = 0;
        PlayableSpriteController.EnergyValue = 240;
    }
}
