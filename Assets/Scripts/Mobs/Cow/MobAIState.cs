using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMobAIState
{
    // Start is called when the state is entered
    void StateEnter();

    // Update is called when the state is active, system will call this method every frame
    void StateUpdate();
}
