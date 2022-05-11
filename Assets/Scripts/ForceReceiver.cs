using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{

    [SerializeField] private CharacterController controller;

    private float verticalVelocity;

    public Vector3 Movement => Vector3.up * verticalVelocity; 

    private void Update()
    {
        if (verticalVelocity <0f && controller.isGrounded)
        {
            //this is setting it to a very small number so the player is always "stuck" to ground, could be any number
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            //this is adding over time
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
}
