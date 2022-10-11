using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLife : MonoBehaviour
{
    // Variable of the Enemy's HP
    [SerializeField] private float _hp = 20f;

    // Function to inflict damage with a variable amount on how much damage is gonna take
    public void TakeDamage(float amount)
    {
        // Substracting Enemy's life
        _hp -= amount;

        // Condition to Destroy the Enemy
        if (_hp <= 0)
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
