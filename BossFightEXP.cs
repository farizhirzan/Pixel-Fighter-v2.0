using UnityEngine;

public class BossFightExp : MonoBehaviour
{
    public int expGain = 500;
    public int energyDrain = 40;

    void Start()
    {
        GainEXP();
        DrainEnergy();
    }

    public void GainEXP()
    {
        PlayableSpriteController.EXP += expGain;
        Debug.Log("EXP after gain: " + PlayableSpriteController.EXP);
    }

    public void DrainEnergy()
    {
        PlayableSpriteController.EnergyValue -= energyDrain;
        Debug.Log("EnergyValue after drain: " + PlayableSpriteController.EnergyValue);
        UpdateEnergyUI();
    }

    private void UpdateEnergyUI()
    {
        ScriptForPlayableSpriteEnergy energyScript = FindObjectOfType<ScriptForPlayableSpriteEnergy>();
        if (energyScript != null)
        {
            energyScript.UpdateEnergyText();
        }
        else
        {
            Debug.LogWarning("ScriptForPlayableSpriteEnergy not found in the scene.");
        }
    }
}
