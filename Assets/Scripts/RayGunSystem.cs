using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGunSystem : MonoBehaviour
{
    // Variable to control the shooting rate
    // public float shootingRate;
    // Variable to control the last shot
    float lastShoot;
    // Variable to modify the cooldown between shots
    [SerializeField] float gunCooldown;
    // Variable to assign the shot Prefab
    public GameObject shotPrefab;
    // Variable of the Raycast system, stores what we hit
    RaycastHit hit;
    // Raycast's range
    float range = 1000f;
    // Gun's Damage
    public float damage = 1f;
    // Variable that makes reference to where to shoot
    public Camera sCam;

    // Update is called once per frame
    void Update ()
    {
        // Shooting Condition if the left click is pressed
        if (Input.GetMouseButton(0))
        {
            // Condition to check if the player is allowed to shoot again
            if (Time.time > lastShoot + gunCooldown)
            {
                // Invoke the ShootingRay Method
                ShootingRay();

                // Updates the lastShoot
                lastShoot = Time.time; //+ shootingRate;
            }

        }
    }

    void ShootingRay()
    {   
        if (Physics.Raycast(sCam.transform.position, sCam.transform.forward, out hit, range))
        {
            // Message to know what we shoot
            Debug.Log(hit.transform.name);

            // Instantiate the laser GO                 prefab        position          rotation
            GameObject laser = GameObject.Instantiate(shotPrefab, transform.position, transform.rotation) as GameObject;

            // Access to TargetLife script and stores it in the target variable
            TargetLife target = hit.transform.GetComponent<TargetLife>();

            // Check if the target component is found
            if(target != null)
            {
                // Access to the method TakeDamage from the TargetLife script and sents the variable damage previously declared
                target.TakeDamage(damage);
            }

            // If the laser does not collision with anything it will be destroy
            GameObject.Destroy(laser, 2f);
        }
    }
}

