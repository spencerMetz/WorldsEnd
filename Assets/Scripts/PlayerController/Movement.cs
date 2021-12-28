using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] LayerMask groundMask;

    [SerializeField] 
    float speed = 10.0f, jumpHeight = 3.5f, gravity = -30f; //-9.81

    Vector2 horizontalInput;
    Vector3 verticalVelocity = Vector3.zero;

    bool jump;
    bool isGrounded;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        if(isGrounded)
            verticalVelocity.y = 0;

        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        //Jump: V = sqrt(-2 * jumpHeight * gravity)
        if (jump && isGrounded)
        {
            verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);

            jump = false;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void SetInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    public void onJumpPressed()
    {
        jump = true;
    }
}
