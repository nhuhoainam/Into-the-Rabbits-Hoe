using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using System;

public class Tree : MonoBehaviour
{
    static readonly int MaxHeatlh = 100;
    private SpriteRenderer spriteRenderer;
    [SerializeField] AnimancerComponent animancer;
    // Start is called before the first frame update

    [SerializeField] private Sprite treeWithFruit;
    [SerializeField] private Sprite treeWithoutFruit;
    [SerializeField] private AnimationClip growingFruit;
    [SerializeField] private AnimationClip shakingWithFruit;
    [SerializeField] private AnimationClip shakingWithoutFruit;
    [SerializeField] private AnimationClip droppingFruit;
    [SerializeField] private bool hasFruit = false;
    [SerializeField] private int health = MaxHeatlh;

    private AnimationClip currentShakingAnimation {
        get {
            return hasFruit ? shakingWithFruit : shakingWithoutFruit;
        }
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void HarvestFruit()
    {
        if (hasFruit)
        {
            StartCoroutine(DropFruit());
        }
    }

    void ChopTree()
    {
        StartCoroutine(ShakeTree());
    }

    private IEnumerator TreeCollasping()
    {
        throw new NotImplementedException();
    }

    private IEnumerator GrowFruit()
    {
        animancer.Play(growingFruit).Time = 0;
        yield return new WaitForSeconds(growingFruit.length);
        hasFruit = true;
    }

    private IEnumerator ShakeTree()
    {
        animancer.Play(currentShakingAnimation).Time = 0;
        yield return new WaitForSeconds(currentShakingAnimation.length);
        health -= 10;
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
        if (hasFruit)
        {
            spriteRenderer.sprite = treeWithFruit;
        }
        else
        {
            spriteRenderer.sprite = treeWithoutFruit;
        }

        if (health <= 0)
        {
            StartCoroutine(TreeCollasping());
        }
    }
}
