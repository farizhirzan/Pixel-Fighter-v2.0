using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StepCounterManager : MonoBehaviour
{
    private static StepCounterManager instance;
    private int lastStepCount = 0;
    private const string LastResetDateKey = "LastStepCountResetDate";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            CheckForMidnightReset();
            StartCoroutine(AddStepCountEveryInterval());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator AddStepCountEveryInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "06_MainMenu" || currentScene == "07_FighterStats")
            {
                PlayableSpriteController.StepCountValue += 100;
                CheckForMilestone();
            }
        }
    }

    void CheckForMilestone()
    {
        if (PlayableSpriteController.StepCountValue >= lastStepCount + 1000)
        {
            lastStepCount += 1000;

            SceneManager.LoadScene("08_ScanColourAlert");
        }
    }

    void CheckForMidnightReset()
    {
        string lastResetDateString = PlayerPrefs.GetString(LastResetDateKey, DateTime.MinValue.ToString());
        DateTime lastResetDate = DateTime.Parse(lastResetDateString);

        DateTime todayMidnight = DateTime.Today;

        if (lastResetDate < todayMidnight)
        {
            ResetStepCount();
        }
    }

    void ResetStepCount()
    {
        PlayableSpriteController.StepCountValue = 0;
        lastStepCount = 0;

        PlayerPrefs.SetString(LastResetDateKey, DateTime.Today.ToString());
        PlayerPrefs.Save();

        Debug.Log("Step count reset at midnight.");
    }
}
