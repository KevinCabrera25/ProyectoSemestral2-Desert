using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    // Variable to control de jump height 
    [SerializeField] private float _jumpForce;
    // Variable to acces to the Rigidbody
    [SerializeField] private Rigidbody _rb;
    // Variable of maximum jumps
    private int _maxJumps = 2;
    // Variable of the current jump
    private int _currentJump = 0;
    // Bool that checks if the player can jump
    private bool _canJump = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            _rb.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
            // Jump Counter
            _currentJump++;

            if (_currentJump == _maxJumps)
            {
                JumpCooldown();
            }
        }
    }

    // Coroutine
    private IEnumerator JumpCooldown()
    {
        _currentJump = 0;
        _canJump = false;
        yield return new WaitForSeconds(2);
        _canJump = true;
    }
}
