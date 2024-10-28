using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonIsPressed : MonoBehaviour
{
    public Button yourButton;
    public string sceneName;

    void Start()
    {
        yourButton.onClick.AddListener(LoadScene);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
