using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct QuestItem
{
    public string name;
    public int amount;
}

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string[] startDialogue;
    public string[] progressDialogue;
    public string[] endDialogue;
    public QuestStatus status;
    public QuestType questType;
    public int required;
    public int startMoney;
    public int rewardMoney;
    public List<QuestItem> startItems; // Give the player some items at the start of the quest
    public List<QuestItem> rewardItems; // Give the player some items as a reward
}
public enum QuestStatus
{
    Inactive,
    Active,
    Completed
}

public enum QuestType
{
    Planting,
    EarnMoney
}
