using UnityEngine;

public class ShowBox : MonoBehaviour
{
    public GameObject dialogueBox;
    public Animator animator;

    private void Start()
    {
        dialogueBox.SetActive(false);
    }

    public void DisplayBox()
    {
        dialogueBox.SetActive(true);
    }

    public void HideBox()
    {
        dialogueBox.SetActive(false);
    }
}