using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLife : MonoBehaviour
{
    // Variable of the Enemy's HP
    public float hp = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Function to inflict damage with a variable amount on how much damage is gonna take
    public void TakeDamage (float amount)
    {
        // Substracting Enemy's life
        hp -= amount;

        // Condition to Destroy the Enemy
        if (hp <= 0)
        {
            // Invoke Die Method
            Die();
        }
    }

    // Add Animation here in the future
    void Die()
    {
        // Destroy the Enemy after its life goes to 0
        Destroy(gameObject);
    }
}
