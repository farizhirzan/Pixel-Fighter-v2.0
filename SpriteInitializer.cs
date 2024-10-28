using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptForPlayableSpriteInitialization : MonoBehaviour
{
    public Animator spriteAnimator;
    private int lastMilestone = 0;

    void Start()
    {
        if (spriteAnimator == null)
        {
            Debug.LogWarning("Animator not set on ScriptForPlayableSpriteInitialization");
        }

        UpdateAnimator(); // Set initial animator state
    }

    void Update()
    {
        CheckForStepMilestone();
        UpdateAnimator();
    }

    void CheckForStepMilestone()
    {
        int milestoneStep = 1000;

        if (PlayableSpriteController.StepCountValue >= lastMilestone + milestoneStep)
        {
            lastMilestone += milestoneStep;

            Debug.Log($"Loading scene: 08_ScanColourAlert (Step Count: {PlayableSpriteController.StepCountValue})");
            SceneManager.LoadScene("08_ScanColourAlert");
        }
    }

    void UpdateAnimator()
    {
        if (spriteAnimator != null)
        {
            spriteAnimator.SetInteger("EvoLevel", PlayableSpriteController.EvoLevel);
        }
    }
}
