using UnityEngine;

public class CoffeeEnergy : MonoBehaviour
{
    public int energyGain = 60;
    private const int MaxEnergy = 240; // Define the maximum energy cap

    void Start()
    {
        GainEnergy();
    }

    public void GainEnergy()
    {
        PlayableSpriteController.EnergyValue += energyGain;

        if (PlayableSpriteController.EnergyValue > MaxEnergy)
        {
            PlayableSpriteController.EnergyValue = MaxEnergy;
        }

        Debug.Log("EnergyValue after gain: " + PlayableSpriteController.EnergyValue);
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

