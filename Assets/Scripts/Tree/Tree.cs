using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using System;

public class Tree : MonoBehaviour
{
    static readonly int MaxHeatlh = 100;
    [SerializeField] private int health = MaxHeatlh;

    private Animator animator;
    private bool hasFruit = false;

    enum State
    {
        Chopped,
        HasFruit,
        NoFruit,
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        if (hasFruit)
        {
            animator.SetTrigger("hasFruit");
        }
    }

    void HarvestFruit()
    {
        animator.SetTrigger("dropFruit");
    }

    void ChopTree()
    {
        animator.SetTrigger("shakeTree");
        health -= 20;
        if (health <= 0)
        {
            animator.SetTrigger("fallTree");
        }
    }

    void GrowFruit()
    {
        if (hasFruit)
        {
            return;
        }

        hasFruit = true;
        animator.SetTrigger("growFruit");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
