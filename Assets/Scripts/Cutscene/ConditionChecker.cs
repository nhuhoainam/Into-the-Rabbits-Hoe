using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ConditionChecker : MonoBehaviour
{
    private PlayerController player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
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
}
