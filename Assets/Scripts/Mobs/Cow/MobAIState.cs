using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobAIState
{
    public MobAIState(MobAI mobAI)
    {
        this.MobAI = mobAI;
    }

    protected MobAI MobAI { get; }
    // Start is called when the state is entered
    abstract public void StateEnter();

    // Update is called when the state is active, system will call this method every frame
    abstract public void StateUpdate();

    // Exit is called when the state is exited
    abstract public void StateExit();
}
