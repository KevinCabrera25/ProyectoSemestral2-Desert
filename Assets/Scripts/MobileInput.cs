using UnityEngine;
/*
public class MobileInput : MonoBehaviour
{
    
    // X axis variable
    // float inputX;
    // Z axis variable
    // float inputZ;
    // Public variable to assign the Joystick in the Inspector
    public Joystick joystickMove;
    // Public variable to assign the Player in the Inspector
    public Transform player;
    // Public variable to assign the Controller in the Inspector
    public Camera _camera;
    // Global variable to access the CameraMovement script
    CameraMovement _cameraMovement;

    private void Start()
    {
        // Catching
        // Get access to the CameraMovement script through a variable
        _cameraMovement = _camera.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Access to the Movex Method
        _cameraMovement.Movex();
        // Access to the Rotation Method
        _cameraMovement.Rotation();

        // Invoke the JoystickMovement method
        // JoystickMovement();
        /*
        // Access to the input
        inputX = joystickMove.Horizontal; 
        inputZ = joystickMove.Vertical;

        // Conditions to enter to each specific method
        if (inputX != 0)
        {
            // Access to the Movex Method
            _cameraMovement.Movex();
        }

        if (inputZ != 0)
        {
            // Access to the Rotation Method
            _cameraMovement.Rotation();
        }
        

    }

    /*
    void JoystickMovement()
    {
        // Conditions to enter to each specific method
        if (inputX != 0)
        {
            // Access to the Movex Method
            _cameraMovement.Movex();
        }
           
        if (inputZ != 0)
        {
            // Access to the Rotation Method
            _cameraMovement.Rotation();
        }        
    }
    
}
*/
