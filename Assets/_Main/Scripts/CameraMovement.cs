using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // X axis variable
    private float _inputX;
    // Variable to control the Speed Movement
    [SerializeField] private float _speedMovement;
    // Z axis variable
    private float _inputZ;
    // Variable to control the Speed rotation
    [SerializeField] private float _speedRotation;
    // Variable to assign the Joystick in the Inspector
    [SerializeField] private Joystick _joystickMove;

    // Update is called once per frame
    void Update()
    {
        // Access to the input
        _inputX = _joystickMove.Horizontal;
        _inputZ = _joystickMove.Vertical;

        // If condition only implemented in the Editor
#if UNITY_EDITOR
        _inputX = Input.GetAxis("Horizontal");
        _inputZ = Input.GetAxis("Vertical");
#endif

        // Conditions to enter to each specific method
        if (_inputX != 0)
            Rotation();
        if (_inputZ != 0)
            MoveX();
    }

    // Movement in the Z axis
    public void MoveX()
    {
        // Fordward movement times the speedMovement variable to affect the speed
        transform.position += transform.forward * _inputZ * Time.deltaTime * _speedMovement;
    }

    // Movement in the X axis
    public void Rotation()
    {
        // Movement in X times the speedRotation variable to affect the speed
        transform.Rotate(new Vector3(0f, _inputX * Time.deltaTime * _speedRotation));
    }
}
