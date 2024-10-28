using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AnimationSceneSwitcher : MonoBehaviour
{
    public string sceneToLoad = "Scene Name";
    public float delayBeforeSwitch = 0.0f;

    public void OnAnimationEnd()
    {
        Debug.Log("Animation finished. Switching scene after delay.");
        StartCoroutine(SwitchSceneAfterDelay());
    }

    private IEnumerator SwitchSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeSwitch);
        SceneManager.LoadScene(sceneToLoad);
    }
}