using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Animancer;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public float moveSpeed = 5.0f;
    public float runSpeedModifier = 2.0f;

    public DirectionalAnimationSet idle;
    public DirectionalAnimationSet walking;
    public DirectionalAnimationSet running;
    public DirectionalAnimationSet usingHoe;
    public DirectionalAnimationSet usingAxe;
    public DirectionalAnimationSet usingWateringCan;
}
