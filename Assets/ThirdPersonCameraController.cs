using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    float rotationSpeed = 1;
    public Transform Target;
    float mouseX, mouseY;
    
    void Start()
    {
        // focuses cursor in the game screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        // gets mouse x and y input and sets the rotation speed
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        // stops the camera moving in certain positions
        mouseY = Mathf.Clamp(mouseY, -10, 60);

        // camera looks at the target object
        transform.LookAt(Target);

        // rotates around the target object based on mouse input
        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
}
