using UnityEngine;

public static class BossController
{
    public static int BossRValue;
    public static int BossGValue;
    public static int BossBValue;
    public static int CurrentBossNumber;

    public static void InitializeBoss(int BossNumber)
    {
        CurrentBossNumber = BossNumber;

        switch (BossNumber)
        {
            case 1: // SplitGirl
                BossRValue = 400;
                BossGValue = 700;
                BossBValue = 300;
                break;
            case 2: // SwapKitty
                BossRValue = 600;
                BossGValue = 500;
                BossBValue = 200;
                break;
            case 3: // SmokeFace
                BossRValue = 300;
                BossGValue = 800;
                BossBValue = 100;
                break;
            case 4: // GraffitiGorilla
                BossRValue = 500;
                BossGValue = 600;
                BossBValue = 400;
                break;
            default:
                Debug.LogError("Invalid Boss Number");
                break;
        }
    }
}
