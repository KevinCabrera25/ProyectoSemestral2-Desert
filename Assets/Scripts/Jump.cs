using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    // Variable to control de jump height 
    public float jumpForce;
    // Variable to acces to the Rigidbody
    public Rigidbody rb;
    // Variable of maximum jumps
    int maxJumps = 2;
    // Variable of the current jump
    int currentJump = 0;
    // Bool that checks if the player can jump
    bool canJump = true;

    /*
    public float jumpSpeed;
    public float jumpSpeedMax;
    public float jumpSpeedMin;
   // Variable to Y velocity
   public float yVelocity;
   */

    // Start is called before the first frame update
    void Start()
    {

    }

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

    private IEnumerator JumpCooldown()
    {
        currentJump = 0;
        canJump = false;
        yield return new WaitForSeconds(2);
        canJump = true;
    }
}
