using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;


    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }


    private void Update()
    {
        //the question mark is a "null conditional operator"
        //this onlu works with custom classes, not monobehavior or scriptable objects
        currentState?.Tick(Time.deltaTime);
    }
}
