using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine){}

    private readonly int targetingBlendTreeStringHash = Animator.StringToHash("TargetingBlendTree");


    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.Animator.Play(targetingBlendTreeStringHash);
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        Debug.Log(stateMachine.Targeter.CurrentTarget.name);
    }



    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
    }


}
