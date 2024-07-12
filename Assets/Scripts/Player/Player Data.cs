using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Animancer;

[System.Serializable]
public class PlayerData
{
    public int money;
    public int treesPlanted;
    public List<string> inactiveQuests;
    public List<string> activeQuests;
    public List<string> completedQuests;
    public Vector3 position;
    public Vector2 Direction = Vector2.down;
}
