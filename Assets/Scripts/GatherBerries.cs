using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GatherBerries : MonoBehaviour
{
    [SerializeField]
    Transform berriesTransform = null;
    [SerializeField]
    Transform storageTransform = null;
    Transform target = null;
    IAstarAI ai = null;

    enum State
    {
        Idle,
        MovingToResourceNode,
        GatheringResources,
        MovingToStorage,
    }
    State state = State.Idle;

    int inventory = 0;

    IEnumerator coroutine = null;

    void Awake()
    {
        ai = GetComponent<IAstarAI>();
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                target = null;
                state = State.MovingToResourceNode;
                break;
            case State.MovingToResourceNode:
                if (target == null)
                {
                    target = berriesTransform;
                    ai.destination = target.position;
                }
                if (ai.reachedDestination)
                {
                    state = State.GatheringResources;
                }
                break;
            case State.GatheringResources:
                target = null;
                if (coroutine == null)
                {
                    coroutine = WaitAndGatherBerries();
                    StartCoroutine(coroutine);
                }
                if (inventory >= 3)
                {
                    StopCoroutine(coroutine);
                    coroutine = null;
                    state = State.MovingToStorage;
                }
                break;
            case State.MovingToStorage:
                if (target == null)
                {
                    target = storageTransform;
                    ai.destination = target.position;
                }
                if (ai.reachedDestination)
                {
                    inventory = 0;
                    state = State.Idle;
                }
                break;
        }
    }

    IEnumerator WaitAndGatherBerries()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.0f);
            inventory++;
        }   
    }
}
