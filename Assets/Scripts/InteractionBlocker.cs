using System.Collections;
using System.Collections.Generic;
using Animancer.Examples.FineControl;
using UnityEngine;

public class InteractionBlocker : MonoBehaviour, IPlayerInteractable
{
    public void Interact(IPlayerInteractable.InteractionContext ctx)
    {
    }

    public ItemData RequiredItem(IPlayerInteractable.InteractionContext ctx)
    {
        return null;
    }

    int IPlayerInteractable.Priority => 1;
}
