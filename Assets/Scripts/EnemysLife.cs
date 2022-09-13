using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysLife : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        EnemyDamage hp = collision.gameObject.GetComponent<EnemyDamage>();
        if(hp)
        {
            hp.TakeDamage(2);
        }
    }
}
