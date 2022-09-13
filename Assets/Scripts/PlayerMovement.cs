using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // X axis variable
    float inputX;
    // Variable to control the Speed Movement
    public float speedMovement;
    // Z axis variable
    float inputZ;
    // Variable to control the Speed rotation
    public float speedRotation;
    // Variable to control de jump height 
    public float jumpForce;
    // Variable to Y velocity
    public float yVelocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Access to the input
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        // Conditions to enter to each specific method
        if (inputX != 0)
            Rotation();
        if (inputZ != 0)
            Movex();
        /*
        if(Input.GetKey(KeyCode.Space))
        {
            yVelocity = jumpForce;
            transform.position = new Vector3(0f, jump, 0f);
        }
        */
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
