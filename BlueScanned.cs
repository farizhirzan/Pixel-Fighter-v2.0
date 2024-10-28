using UnityEngine;

public class BlueScanned : MonoBehaviour
{
    public int bValueGain = 25;
    public int expGain = 250;
    public int energyDrain = 20;

    void Start()
    {
        GainBValue();
        GainEXP();
        DrainEnergy();
    }

    public void GainBValue()
    {
        ScriptForPlayableSpriteBValue bValueScript = FindObjectOfType<ScriptForPlayableSpriteBValue>();
        if (bValueScript != null)
        {
            bValueScript.AddToBValue(bValueGain);
        }
        else
        {
            Debug.LogWarning("ScriptForPlayableSpriteBValue not found in the scene.");
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
