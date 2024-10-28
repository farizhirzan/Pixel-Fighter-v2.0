using UnityEngine;

public class GreenScanned : MonoBehaviour
{
    public int gValueGain = 25;
    public int expGain = 250;
    public int energyDrain = 20;

    void Start()
    {
        GainGValue();
        GainEXP();
        DrainEnergy();
    }

    public void GainGValue()
    {
        ScriptForPlayableSpriteGValue gValueScript = FindObjectOfType<ScriptForPlayableSpriteGValue>();
        if (gValueScript != null)
        {
            gValueScript.AddToGValue(gValueGain);
        }
        else
        {
            Debug.LogWarning("ScriptForPlayableSpriteGValue not found in the scene.");
        }
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
