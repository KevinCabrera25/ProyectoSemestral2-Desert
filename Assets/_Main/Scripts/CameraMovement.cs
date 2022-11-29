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

    private float _timeCooldown = 1f;

    // Update is called once per frame
    void Update()
    {
        // Access to the input
        _inputX = _joystickMove.Horizontal;
        _inputZ = _joystickMove.Vertical;
       
        /*
        // If condition only implemented in the Editor
#if UNITY_EDITOR
#endif
        _inputX = Input.GetAxis("Horizontal");
        _inputZ = Input.GetAxis("Vertical");
        */

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

        if (Time.time >= _timeCooldown)
        {
            // Instantiate the Audio Manager for the Steps Sound
            AudioManager.Instance.PlaySteps();
            _timeCooldown = _timeCooldown + 0.5f;
        }
    }

    // Movement in the X axis
    public void Rotation()
    {
        // Movement in X times the speedRotation variable to affect the speed
        transform.Rotate(new Vector3(0f, _inputX * Time.deltaTime * _speedRotation));
    }
}
