using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // X axis variable
    float inputX;
    // Variable to control the Speed Movement
    public float speedMovement;
    // Z axis variable
    float inputZ;
    // Variable to control the Speed rotation
    public float speedRotation;
    // Public variable to assign the Joystick in the Inspector
    public Joystick joystickMove;

    // Update is called once per frame
    void Update()
    {
        // Access to the input
        inputX = joystickMove.Horizontal + Input.GetAxis("Horizontal");
        inputZ = joystickMove.Vertical + Input.GetAxis("Vertical");

        // Conditions to enter to each specific method
        if (inputX != 0)
            Rotation();
        if (inputZ != 0)
            Movex();       
    }

    // Movement in the Z axis
    public void Movex()
    {
        // Fordward movement times the speedMovement variable to affect the speed
        transform.position += transform.forward * inputZ * Time.deltaTime * speedMovement;
    }

    // Movement in the X axis
    public void Rotation()
    {
        // Movement in X times the speedRotation variable to affect the speed
        transform.Rotate(new Vector3(0f, inputX * Time.deltaTime * speedRotation));
    }
}
