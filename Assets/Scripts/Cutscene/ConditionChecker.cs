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
                    "You are an IT employee with a salary of 4000 dollars a month.",
                    "However, you feel unsatisfied with your mundane life.",
                    "One day, while going to work, you receive news that you have inherited an island.",
                    "You immediately submit your resignation and leave work in the middle of the day.",
                    "Upon arriving home, you see a few strangers in black clothes waiting right at the gate.",
                    "Suddenly, you realize someone is standing behind you, covering your nose and mouth with a cloth",
                    "Everything suddenly goes dark…",
                    "When you wake up, you find yourself in the middle of an island, surrounded by no one.",
                }
            );
            player.playerData.newGame = false;
        }
        if (BeginningActivated()) {
            GameObject[] beginningObstacles = GameObject.FindGameObjectsWithTag("BeginningObstacles");
            foreach (GameObject obstacle in beginningObstacles) {
                obstacle.SetActive(false);
            }
        }
    }

    bool BeginningActivated()
    {
        return player.playerData.activeQuests.Contains("Beginning") || player.playerData.completedQuests.Contains("Beginning");
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
