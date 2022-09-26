using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    // Variable to control de jump height 
    [SerializeField] private float jumpForce;
    // Variable to acces to the Rigidbody
    [SerializeField] private Rigidbody rb;
    // Variable of maximum jumps
    int maxJumps = 2;
    // Variable of the current jump
    int currentJump = 0;
    // Bool that checks if the player can jump
    bool canJump = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            // Jump Counter
            currentJump++;

            if (currentJump == maxJumps)
            {
                JumpCooldown();
            }

        }

    }
    // Coroutine
    private IEnumerator JumpCooldown()
    {
        currentJump = 0;
        canJump = false;
        yield return new WaitForSeconds(2);
        canJump = true;
    }
}
