using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    public float typingSpeed = 0.05f; // Speed of typing effect
    private Queue<string> textsToDisplay = new Queue<string>();

    void Awake()
    {
        // Hide the text initially
        uiText.text = "";
    }

    public void StartTyping(List<string> texts)
    {
        // Add the texts to the queue
        foreach (string text in texts)
        {
            textsToDisplay.Enqueue(text);
        }

        // Start the typing coroutine if not already running
        if (textsToDisplay.Count > 0 && !IsTyping())
        {
            StartCoroutine(TypeText());
        }
    }

    private bool IsTyping()
    {
        return uiText.text.Length > 0;
    }

    IEnumerator TypeText()
    {
        while (textsToDisplay.Count > 0)
        {
            string fullText = textsToDisplay.Dequeue();
            string currentText = "";

            for (int i = 0; i <= fullText.Length; i++)
            {
                currentText = fullText.Substring(0, i);
                uiText.text = currentText;
                yield return new WaitForSeconds(typingSpeed);
            }

            // Wait for a while before starting the next text (optional)
            yield return new WaitForSeconds(1.0f);
        }

        // Clear the text after finishing
        uiText.text = "";

        // Notify the ConditionChecker that typing is complete
        GameObject.FindObjectOfType<ConditionChecker>().OnTypingComplete();
    }
}
