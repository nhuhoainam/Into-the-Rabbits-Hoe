using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Animancer;

[System.Serializable]
public class PlayerData
{
    public bool newGame = true;
    public int Money = 0;
    public int treesPlanted = 0;
    public List<string> inactiveQuests = new();
    public List<string> activeQuests = new();
    public List<string> completedQuests = new();
    public Vector3 position = new(-17.42f, -13.12f, 0);
    public Vector2 Direction = Vector2.down;

    public PlayerData()
    {
    }

    public PlayerData(PlayerData other)
    {
        newGame = other.newGame;
        Money = other.Money;
        treesPlanted = other.treesPlanted;
        inactiveQuests = new(other.inactiveQuests);
        activeQuests = new(other.activeQuests);
        completedQuests = new(other.completedQuests);
        position = other.position;
        Direction = other.Direction;
    }
}
