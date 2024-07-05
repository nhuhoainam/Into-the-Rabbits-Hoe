using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using System;

public class Tree : MonoBehaviour
{
    static readonly int MaxHeatlh = 100;
    [SerializeField] AnimancerComponent animancer;
    // Start is called before the first frame update

    [SerializeField] private AnimationClip treeWithFruit;
    [SerializeField] private AnimationClip treeWithoutFruit;
    [SerializeField] private AnimationClip treeStump;
    [SerializeField] private AnimationClip growingFruit;
    [SerializeField] private AnimationClip shakingWithFruit;
    [SerializeField] private AnimationClip shakingWithoutFruit;
    [SerializeField] private AnimationClip droppingFruit;
    [SerializeField] private bool hasFruit = false;
    [SerializeField] private int health = MaxHeatlh;

    enum State
    {
        Chopped,
        HasFruit,
        NoFruit,
    }

    private AnimationClip CurrentShakingAnimation
    {
        get
        {
            return hasFruit ? shakingWithFruit : shakingWithoutFruit;
        }
    }
    void Start()
    {
    }

    void HarvestFruit()
    {
        if (health <= 0)
        {
            return;
        }
        if (hasFruit)
        {
            StartCoroutine(DropFruit());
        }
    }

    void ChopTree()
    {
        if (health <= 0)
        {
            return;
        }
        StartCoroutine(ShakeTree());
    }

    private IEnumerator TreeCollasping()
    {
        yield return new WaitForSeconds(1);
    }

    private IEnumerator GrowFruit()
    {
        animancer.Play(growingFruit).Time = 0;
        yield return new WaitForSeconds(growingFruit.length);
        hasFruit = true;
    }

    private IEnumerator ShakeTree()
    {
        animancer.Play(CurrentShakingAnimation).Time = 0;
        yield return new WaitForSeconds(CurrentShakingAnimation.length);
        health -= 20;
        if (health <= 0)
        {
            animancer.Play(treeStump).Time = 0;
        }
        else if (hasFruit)
        {
            animancer.Play(treeWithFruit).Time = 0;
        }
        else
        {
            animancer.Play(treeWithoutFruit).Time = 0;
        }
    }

    private IEnumerator DropFruit()
    {
        animancer.Play(droppingFruit).Time = 0;
        yield return new WaitForSeconds(droppingFruit.length);
        hasFruit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChopTree();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            HarvestFruit();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(GrowFruit());
        }
    }
}
