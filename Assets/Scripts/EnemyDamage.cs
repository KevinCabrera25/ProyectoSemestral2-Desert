using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Life variable
    public int hp;

   public void TakeDamage(int damage)
    {
        hp -= damage;

        if (damage <= 0)
        {
            Destroy(gameObject);
        }
    }
}
