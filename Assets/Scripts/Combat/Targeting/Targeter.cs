using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    private List<Target> targets = new List<Target>();

    public Target CurrentTarget { get; private set; }

    [SerializeField] private CinemachineTargetGroup cineTargetGroup;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
        Debug.Log("target added");

    }

    private void RemoveTarget(Target target)
    {
        if(CurrentTarget == target)
        {
            cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }
        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        targets.Remove(target);
        Debug.Log("target removed");
    }

    public bool SelectTarget()
    {
        if(targets.Count == 0) { return false; }
        CurrentTarget = targets[0];
        cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
        return true;
    }

    public void Cancel()
    {
        cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
        
    }
}
