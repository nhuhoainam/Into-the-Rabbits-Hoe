using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ConditionChecker : MonoBehaviour
{
    public GameObject storyPanel;
    public GameObject blackScreen; // Reference to the black screen panel
    private TypingEffect typingEffect; // Reference to the TypingEffect script
    private List<string> texts; // Texts to display
    private PlayerController player;

    void Start()
    {
        typingEffect = storyPanel.GetComponent<TypingEffect>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        storyPanel.SetActive(false); // Ensure storyPanel is inactive at the start
    }

    void Update()
    {
        // Replace with your own condition
        if (player.playerData.newGame)
        {
            ShowBlackScreenWithText(
                new List<string>
                {
                    "This is the first scene.",
                    "You can add more text here."
                }
            );
            player.playerData.newGame = false;
        }
        if (BeginningActivated()) {
            GameObject[] beginningObstacles = GameObject.FindGameObjectsWithTag("BeginningObstacle");
            foreach (GameObject obstacle in beginningObstacles) {
                obstacle.SetActive(false);
            }
        }
    }

    bool BeginningActivated()
    {
        return player.playerData.activeQuests.Contains("Beginning");
    }

    bool FirstSceneCondition()
    {
        return player.playerData.newGame;
    }

    void ShowBlackScreenWithText(List<string> text)
    {
        storyPanel.SetActive(true);

        texts = new List<string>(text);

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
