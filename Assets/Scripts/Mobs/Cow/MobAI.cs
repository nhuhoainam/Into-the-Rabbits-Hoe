using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Pathfinding;

class EatingState : IMobAIState
{
    private readonly MobAI mobAI;
    private float time = 0;
    public EatingState(MobAI mobAI)
    {
        this.mobAI = mobAI;
    }
    public void StateEnter()
    {
        time = 0;
        mobAI.animator.SetBool("isEating", true);
    }

    public void StateUpdate()
    {
        time += Time.deltaTime;
        if (time >= mobAI.eatingTime)
        {
            mobAI.animator.SetBool("isEating", false);
            mobAI.animator.SetBool("isChewing", true);
        }
    }
}


class WanderingState : IMobAIState
{
    private readonly MobAI mobAI;
    public WanderingState(MobAI mobAI)
    {
        this.mobAI = mobAI;
    }
    public void StateEnter()
    {
        mobAI.animator.SetBool("isWalking", true);
    }

    public void StateUpdate()
    {
        mobAI.animator.SetBool("isWalking", true);
    }
}

class SleepingState : IMobAIState
{
    private readonly MobAI mobAI;
    public SleepingState(MobAI mobAI)
    {
        this.mobAI = mobAI;
    }
    public void StateEnter()
    {
        mobAI.animator.SetBool("isSleeping", true);
    }

    public void StateUpdate()
    {
        mobAI.animator.SetBool("isSleeping", true);
    }
}

class StandingState : IMobAIState
{
    private readonly MobAI mobAI;
    public StandingState(MobAI mobAI)
    {
        this.mobAI = mobAI;
    }
    public void StateEnter()
    {
        mobAI.animator.SetBool("isStanding", true);
    }

    public void StateUpdate()
    {
        mobAI.animator.SetBool("isStanding", true);
    }
}

class FindingCropState : IMobAIState
{
    private readonly MobAI mobAI;
    public FindingCropState(MobAI mobAI)
    {
        this.mobAI = mobAI;
    }
    public void StateEnter()
    {
        mobAI.animator.SetBool("isFindingCrop", true);
    }

    public void StateUpdate()
    {
        mobAI.animator.SetBool("isFindingCrop", true);
    }
}
public class MobAI : MonoBehaviour
{
    [SerializeField] internal float eatingTime = 5;
    internal Animator animator;
    internal Seeker seeker;
    internal AIPath aiPath;

    internal IMobAIState currentState;
    private enum State
    {
        Done,
        FindingCrop,
        StandingAndWavingTail,
        StandingAndBlinking,
        Eating,
        Wandering,
        Sleeping,
        Chewing,
    }

    private State state;
    // Start is called before the first frame update
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        seeker = GetComponent<Seeker>();
        animator = GetComponent<Animator>();
        currentState = new StandingState(this);

        // StartCoroutine(Wander());
    }
    void Update()
    {
        currentState.StateUpdate();
    }
}
