using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove_Test : MonoBehaviour
{
    public float Velocity;
    [Space]

    private CharacterController controller;
    private bool isGrounded;
    private Vector3 desiredMoveDirection;
    private float InputX;
    private float InputZ;

    public bool blockRotationPlayer;
    public float desiredPlayerRotation = 0.1f;
    public float Speed;
    public float allowPlayerRotation = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {

        }
        
    }
}
