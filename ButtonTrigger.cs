using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
    public Button button;
    public Animator animator;
    public string ButtonPressed;

    void Start()
    {
        button.onClick.AddListener(TriggerAnimation);
    }

    public void TriggerAnimation()
    {
        animator.SetTrigger(ButtonPressed);
    }
}
