using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypewriterEffect : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    public Text dialogueText;
    [TextArea(3, 10)]
    public string[] dialogueLines;

    public Animator animator;
    public string animationNameStart;
    public string animationNameEnd;
    public Button okButton;

    public float delayBetweenLines = 0.12f;

    private int currentLineIndex = 0;
    private string currentText = "";
    private bool isTyping = false;
    private bool skipToFullLine = false;
    private bool canProceedToNextLine = true;
    private void Start()
    {
        dialogueText.gameObject.SetActive(false);
        okButton.onClick.AddListener(OnOkButtonClicked);
        StartCoroutine(WaitForAnimationAndStartDialogue());
    }

    private IEnumerator WaitForAnimationAndStartDialogue()
    {
        dialogueText.gameObject.SetActive(true);

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationNameStart))
        {
            yield return null;
        }

        yield return StartCoroutine(TypeInitializingText(""));

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        dialogueText.text = "";
        if (dialogueLines.Length > 0)
        {
            StartTypingLine(0);
        }
    }

    private IEnumerator TypeInitializingText(string line)
    {
        currentText = "";
        dialogueText.text = currentText;

        for (int i = 0; i < line.Length; i++)
        {
            currentText += line[i];
            dialogueText.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void StartTypingLine(int lineIndex)
    {
        if (lineIndex >= 0 && lineIndex < dialogueLines.Length)
        {
            currentLineIndex = lineIndex;
            currentText = "";
            StartCoroutine(ShowText(dialogueLines[lineIndex]));
        }
    }

    private IEnumerator ShowText(string line)
    {
        isTyping = true;
        skipToFullLine = false;
        dialogueText.text = currentText = "";

        for (int i = 0; i < line.Length; i++)
        {
            if (skipToFullLine)
            {
                dialogueText.text = line;
                isTyping = false;
                break;
            }

            currentText += line[i];
            dialogueText.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        StartCoroutine(EnableProceedToNextLine());
    }

    private void OnOkButtonClicked()
    {
        if (!canProceedToNextLine)
        {
            return;
        }

        if (isTyping)
        {
            skipToFullLine = true;
        }
        else
        {
            if (currentLineIndex < dialogueLines.Length - 1)
            {
                ShowNextLine();
            }
            else
            {
                LoadNextScene();
            }
        }
    }

    public void ShowNextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length)
        {
            StartTypingLine(currentLineIndex);
        }
        else
        {
            currentLineIndex = 0;
        }
    }

    private void LoadNextScene()
    {
        animator.Play(animationNameEnd);
        StartCoroutine(WaitForAnimationAndLoadScene());
    }

    private IEnumerator WaitForAnimationAndLoadScene()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationNameEnd))
        {
            yield return null;
        }

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        if (SceneManager.GetActiveScene().name == "01_Tutorial")
        {
            SceneManager.LoadScene("02_MapTutorial");
        }

        else if (SceneManager.GetActiveScene().name == "02_MapTutorial")
        {
            SceneManager.LoadScene("03_FighterMap");
        }

        else if (SceneManager.GetActiveScene().name == "05_GetSprite")
        {
            SceneManager.LoadScene("06_MainMenu");
        }
    }

    private IEnumerator EnableProceedToNextLine()
    {
        canProceedToNextLine = false;
        yield return new WaitForSeconds(delayBetweenLines);
        canProceedToNextLine = true;
    }
}
