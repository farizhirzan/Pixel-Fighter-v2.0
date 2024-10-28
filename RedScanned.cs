using UnityEngine;

public class RedScanned : MonoBehaviour
{
    public int rValueGain = 25;
    public int expGain = 200;
    public int energyDrain = 20;

    void Start()
    {
        GainRValue();
        GainEXP();
        DrainEnergy();
    }

    public void GainRValue()
    {
        ScriptForPlayableSpriteRValue rValueScript = FindObjectOfType<ScriptForPlayableSpriteRValue>();
        if (rValueScript != null)
        {
            rValueScript.AddToRValue(rValueGain);
        }
        else
        {
            Debug.LogWarning("ScriptForPlayableSpriteRValue not found in the scene.");
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
