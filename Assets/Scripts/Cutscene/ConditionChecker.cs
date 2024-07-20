using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ConditionChecker : MonoBehaviour
{
    public GameObject storyPanel;
    public GameObject blackScreen; // Reference to the black screen panel
    private TypingEffect typingEffect; // Reference to the TypingEffect script
    private bool firstSceneAvailable = true;
    private List<string> texts; // Texts to display

    void Start()
    {
        typingEffect = storyPanel.GetComponent<TypingEffect>();
        storyPanel.SetActive(false); // Ensure storyPanel is inactive at the start
    }

    void Update()
    {
        // Replace with your own condition
        if (YourCondition() && firstSceneAvailable)
        {
            ShowBlackScreenWithText();
            firstSceneAvailable = false;
        }
    }

    bool YourCondition()
    {
        // Your condition logic here
        return Input.GetKeyDown(KeyCode.Space); // Example: press Space key to trigger
    }

    void ShowBlackScreenWithText()
    {
        storyPanel.SetActive(true);

        texts = new List<string>()
        {
            "This is the first scene.",
            "Story continues..."
        };

        // Set the black screen image component to be fully opaque
        blackScreen.GetComponent<Image>().color = new Color(0, 0, 0, 1);

        typingEffect.StartTyping(texts);
    }

    public void OnTypingComplete()
    {
        StartCoroutine(SetScreenTransparent());
    }

    IEnumerator SetScreenTransparent()
    {
        // Wait for a brief moment after typing completes (optional)
        yield return new WaitForSeconds(1.0f);

        // Set the black screen to be transparent
        blackScreen.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        // Deactivate the story panel
        storyPanel.SetActive(false);
    }
}
