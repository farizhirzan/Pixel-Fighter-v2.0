using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InactivityTimer : MonoBehaviour
{
    public float inactivityThreshold = 10f;
    public string idleSceneName = "12_ExploreMenu";

    private float inactivityTimer = 0f;

    public Button[] UIButtons;

    void Start()
    {
        foreach (Button button in UIButtons)
        {
            button.onClick.AddListener(ResetInactivityTimer);
        }
    }

    void Update()
    {
        inactivityTimer += Time.deltaTime;

        if (inactivityTimer >= inactivityThreshold)
        {
            Debug.Log("Inactivity detected. Switching to idle scene.");
            SceneManager.LoadScene(idleSceneName);
        }

        CheckForScreenActivity();
    }

    private void CheckForScreenActivity()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            ResetInactivityTimer();
        }
    }

    private void ResetInactivityTimer()
    {
        inactivityTimer = 0f;
    }
}
