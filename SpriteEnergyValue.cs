using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptForPlayableSpriteEnergy : MonoBehaviour
{
    public Text ShowingEnergy;
    private const int MaxEnergy = 240;

    private static ScriptForPlayableSpriteEnergy instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateEnergyText();
        StartCoroutine(AddEnergyEverySixMinute());
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "07_FighterStats" || scene.name == "09_ScanRed" || 
            scene.name == "10_ScanGreen" || scene.name == "11_ScanBlue" || 
            scene.name == "15_ActivePlayEXP" || scene.name == "20_CoffeeScanned" || scene.name == "28_BossEXP")
        {
            ShowingEnergy = GameObject.Find("EnergyValue").GetComponent<Text>();
            UpdateEnergyText();
        }
        else
        {
            ShowingEnergy = null;
        }
    }

    IEnumerator AddEnergyEverySixMinute()
    {
        while (true)
        {
            yield return new WaitForSeconds(360);
            if (PlayableSpriteController.EnergyValue < MaxEnergy)
            {
                PlayableSpriteController.EnergyValue += 1;

                if (PlayableSpriteController.EnergyValue > MaxEnergy)
                {
                    PlayableSpriteController.EnergyValue = MaxEnergy;
                }

                UpdateEnergyText();
                Debug.Log($"Energy increased: {PlayableSpriteController.EnergyValue}");
            }
            else
            {
                Debug.Log("Energy is at max and will not increase.");
            }
        }
    }

    public void UpdateEnergyText()
    {
        if (ShowingEnergy != null)
        {
            ShowingEnergy.text = $"{PlayableSpriteController.EnergyValue}/{MaxEnergy}";
        }
    }
    
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
