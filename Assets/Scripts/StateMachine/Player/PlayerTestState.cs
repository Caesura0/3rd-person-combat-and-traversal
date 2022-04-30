using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }
    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();
        movement.x = stateMachine.InputReader.movementValue.x;
        movement.y = 0;
        movement.z = stateMachine.InputReader.movementValue.y;
        stateMachine.transform.Translate(movement * Time.deltaTime);
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

}
