using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BossAnimationSceneSwitcher : MonoBehaviour
{
    public float delayBeforeSwitch = 0.0f;

    private string[] bossScenes = new string[]
    {
        "21A_FightSplitGirl",
        "21B_FightSwapKitty",
        "21C_FightSmokeFace",
        "21D_FightGraffitiGorilla"
    };

    public void OnAnimationEnd()
    {
        Debug.Log("Animation finished. Switching scene after delay.");
        StartCoroutine(SwitchSceneAfterDelay());
    }

    private IEnumerator SwitchSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeSwitch);

        int bossNumber = BossController.CurrentBossNumber;

        if (bossNumber >= 1 && bossNumber <= 4)
        {
            SceneManager.LoadScene(bossScenes[bossNumber - 1]);
        }
        else
        {
            Debug.LogError("Invalid Boss Number in BossController! Please assign a value between 1 and 4.");
        }
    }
}
