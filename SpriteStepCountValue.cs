using UnityEngine;
using UnityEngine.UI;

public class ScriptForPlayableSpriteStepCount : MonoBehaviour
{
    public Text ShowingStepCount;

    void Start()
    {
        UpdateStepCountUI();
    }

    void Update()
    {
        UpdateStepCountUI();
    }

    void UpdateStepCountUI()
    {
        if (ShowingStepCount != null)
        {
            ShowingStepCount.text = PlayableSpriteController.StepCountValue.ToString();
        }
    }
}
