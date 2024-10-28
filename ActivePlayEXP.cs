using UnityEngine;

public class ActivePlayExp : MonoBehaviour
{
    public int expGain = 200;
    public int energyDrain = 20;

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
