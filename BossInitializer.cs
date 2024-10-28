using UnityEngine;
using UnityEngine.SceneManagement;

public class BossInitializer : MonoBehaviour
{
    void Start()
    {
        InitializeBossByScene();
    }

    void InitializeBossByScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "21A_FightSplitGirl":
                BossController.InitializeBoss(1); // Boss 1: SplitGirl
                break;
            case "21B_FightSwapKitty":
                BossController.InitializeBoss(2); // Boss 2: SwapKitty
                break;
            case "21C_FightSmokeFace":
                BossController.InitializeBoss(3); // Boss 3: SmokeFace
                break;
            case "21D_FightGraffitiGorilla":
                BossController.InitializeBoss(4); // Boss 4: GraffitiGorilla
                break;
            default:
                Debug.LogError("Unknown scene: " + sceneName);
                break;
        }

        Debug.Log("Boss Initialized: " + BossController.CurrentBossNumber);
    }
}
