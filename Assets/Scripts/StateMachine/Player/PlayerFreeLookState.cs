using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int freeLookSpeedStringHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int freeLookBlendTreeStringHash = Animator.StringToHash("FreeLookBlendTree");

    private const float animatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.Animator.Play(freeLookBlendTreeStringHash);
    }
    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovment();

        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
        
        if(stateMachine.InputReader.MovementValue == Vector2.zero) 
        {
            stateMachine.Animator.SetFloat(freeLookSpeedStringHash, 0, animatorDampTime, deltaTime);
            return; 
        }
        stateMachine.Animator.SetFloat(freeLookSpeedStringHash, 1, animatorDampTime, deltaTime);
        stateMachine.CharacterController.Move(Time.deltaTime * stateMachine.FreeLookMovementSpeed * movement ) ;
        if(stateMachine.InputReader.MovementValue == Vector2.zero) { return; }
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) { return; }
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationSmoothValue );
    }


    Vector3 CalculateMovment()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;
    }



}
