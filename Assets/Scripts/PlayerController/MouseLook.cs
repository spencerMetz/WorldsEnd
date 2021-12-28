using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f;

    [SerializeField] 
    float sensitivityX = 8f, sensitivityY = 6f;

    float mouseX, mouseY;

    float xRotation = 0f;
    
    private void Update()
    {
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;

        playerCamera.eulerAngles = targetRotation;
    }

    public void SetInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensitivityX * 2;
        mouseY = mouseInput.y * sensitivityY;
    }
}
