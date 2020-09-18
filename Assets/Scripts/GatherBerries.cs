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

    void Awake()
    {
        target = berriesTransform;

        ai = GetComponent<IAstarAI>();
        ai.destination = target.position;
    }

    void Update()
    {
        if (ai.reachedDestination)
        {
            if (target == berriesTransform)
            {
                target = storageTransform;
            } else
            {
                target = berriesTransform;
            }
            ai.destination = target.position;
        }
    }
}
